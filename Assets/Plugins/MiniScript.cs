using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;


namespace MiniScript
{
	public class ScriptEngine
	{
		private readonly Dictionary<string, Action<object[]>> _nativeFunctions = new Dictionary<string, Action<object[]>>();

		public ScriptEngine()
		{
			// 기본 바인딩 함수 등록
			_nativeFunctions["print"] = args =>
			{
				if (args.Length > 0)

				//Console.WriteLine(args[0]);
				Debug.Log(args[0]);
			};
		}

		public void RegisterFunction(string name, Action<object[]> func)
		{
			_nativeFunctions[name] = func;
		}

		public void Execute(string script)
		{
			var parser = new Parser(script);
			var func = parser.ParseFunction();
			var interpreter = new Interpreter(_nativeFunctions);
			interpreter.ExecuteFunction(func);
		}
	}

	#region Lexer
	public enum TokenType { Identifier, Number, Symbol, Keyword, String, EOF }

	public class Token
	{
		public TokenType Type;
		public string Value;
	}

	public class Lexer
	{
		private readonly string _src;
		private int _pos;

		public Lexer(string src) => _src = src;

		public Token Next()
		{
			SkipWhite();

			if (_pos >= _src.Length)
				return new Token { Type = TokenType.EOF };

			char c = _src[_pos];

			if (char.IsLetter(c))
				return ReadIdentifier();
			if (char.IsDigit(c))
				return ReadNumber();
			if (c == '"' || c == '\'')
				return ReadString();

			_pos++;
			return new Token { Type = TokenType.Symbol, Value = c.ToString() };
		}

		private void SkipWhite()
		{
			while (_pos < _src.Length && char.IsWhiteSpace(_src[_pos]))
				_pos++;
		}

		private Token ReadIdentifier()
		{
			int start = _pos;
			while (_pos < _src.Length && (char.IsLetterOrDigit(_src[_pos]) || _src[_pos] == '_'))
				_pos++;
			string value = _src.Substring(start, _pos - start);
			return new Token { Type = (value == "function" || value == "var") ? TokenType.Keyword : TokenType.Identifier, Value = value };
		}

		private Token ReadNumber()
		{
			int start = _pos;
			while (_pos < _src.Length && (char.IsDigit(_src[_pos]) || _src[_pos] == '.'))
				_pos++;
			string value = _src.Substring(start, _pos - start);
			return new Token { Type = TokenType.Number, Value = value };
		}

		private Token ReadString()
		{
			char quote = _src[_pos++];
			int start = _pos;
			while (_pos < _src.Length && _src[_pos] != quote)
				_pos++;
			string value = _src.Substring(start, _pos - start);
			_pos++;
			return new Token { Type = TokenType.String, Value = value };
		}
	}
	#endregion

	#region Parser
	public class FunctionDef
	{
		public List<IStatement> Body = new List<IStatement>();
	}

	public interface IStatement { }

	public class VarAssign : IStatement
	{
		public string Name;
		public IExpression Expr;
	}

	public class CallStatement : IStatement
	{
		public string FuncName;
		public List<IExpression> Args = new List<IExpression>();
	}

	public interface IExpression
	{
		object Evaluate(Dictionary<string, object> vars);
	}

	public class NumberExpr : IExpression
	{
		public float Value;
		public object Evaluate(Dictionary<string, object> vars) => Value;
	}

	public class VarExpr : IExpression
	{
		public string Name;
		public object Evaluate(Dictionary<string, object> vars) => vars[Name];
	}

	public class BinaryExpr : IExpression
	{
		public IExpression Left;
		public string Op;
		public IExpression Right;
		public object Evaluate(Dictionary<string, object> vars)
		{
			var l = Convert.ToSingle(Left.Evaluate(vars));
			var r = Convert.ToSingle(Right.Evaluate(vars));
			return Op switch
			{
				"+" => l + r,
				"-" => l - r,
				"*" => l * r,
				"/" => l / r,
				"%" => l % r,
				_ => 0
			};
		}
	}

	public class Parser
	{
		private readonly Lexer _lexer;
		private Token _cur;

		public Parser(string src)
		{
			_lexer = new Lexer(src);
			_cur = _lexer.Next();
		}

		private void Next() => _cur = _lexer.Next();
		private bool Match(string v)
		{
			if (_cur.Value == v)
			{
				Next();
				return true;
			}
			return false;
		}

		public FunctionDef ParseFunction()
		{
			ExpectKeyword("function");
			string funcName = ExpectIdentifier();
			Expect("(");
			Expect(")");
			Expect("{");

			var func = new FunctionDef();
			while (_cur.Type != TokenType.Symbol || _cur.Value != "}")
				func.Body.Add(ParseStatement());

			Expect("}");
			return func;
		}

		private IStatement ParseStatement()
		{
			if (_cur.Type == TokenType.Keyword && _cur.Value == "var")
			{
				Next();
				string name = ExpectIdentifier();
				Expect("=");
				var expr = ParseExpression();
				Expect(";");
				return new VarAssign { Name = name, Expr = expr };
			}
			else if (_cur.Type == TokenType.Identifier)
			{
				string name = _cur.Value;
				Next();
				Expect("(");
				var args = new List<IExpression>();
				if (_cur.Value != ")")
				{
					args.Add(ParseExpression());
					while (Match(","))
						args.Add(ParseExpression());
				}
				Expect(")");
				Expect(";");
				return new CallStatement { FuncName = name, Args = args };
			}
			throw new Exception($"Unexpected token: {_cur.Value}");
		}

		private IExpression ParseExpression()
		{
			var expr = ParseTerm();
			while (_cur.Value == "+" || _cur.Value == "-")
			{
				string op = _cur.Value;
				Next();
				var right = ParseTerm();
				expr = new BinaryExpr { Left = expr, Op = op, Right = right };
			}
			return expr;
		}

		private IExpression ParseTerm()
		{
			var expr = ParseFactor();
			while (_cur.Value == "*" || _cur.Value == "/" || _cur.Value == "%")
			{
				string op = _cur.Value;
				Next();
				var right = ParseFactor();
				expr = new BinaryExpr { Left = expr, Op = op, Right = right };
			}
			return expr;
		}

		private IExpression ParseFactor()
		{
			if (_cur.Type == TokenType.Number)
			{
				float val = float.Parse(_cur.Value, CultureInfo.InvariantCulture);
				Next();
				return new NumberExpr { Value = val };
			}
			if (_cur.Type == TokenType.Identifier)
			{
				string name = _cur.Value;
				Next();
				return new VarExpr { Name = name };
			}
			if (Match("("))
			{
				var expr = ParseExpression();
				Expect(")");
				return expr;
			}
			throw new Exception($"Unexpected factor: {_cur.Value}");
		}

		private void Expect(string v)
		{
			if (_cur.Value != v)
				throw new Exception($"Expected '{v}' but got '{_cur.Value}'");
			Next();
		}

		private void ExpectKeyword(string v)
		{
			if (_cur.Type != TokenType.Keyword || _cur.Value != v)
				throw new Exception($"Expected keyword '{v}'");
			Next();
		}

		private string ExpectIdentifier()
		{
			if (_cur.Type != TokenType.Identifier)
				throw new Exception("Expected identifier");
			string val = _cur.Value;
			Next();
			return val;
		}
	}
	#endregion

	#region Interpreter
	public class Interpreter
	{
		private readonly Dictionary<string, Action<object[]>> _native;
		private readonly Dictionary<string, object> _vars = new Dictionary<string, object>();

		public Interpreter(Dictionary<string, Action<object[]>> native)
		{
			_native = native;
		}

		public void ExecuteFunction(FunctionDef func)
		{
			foreach (var stmt in func.Body)
			{
				switch (stmt)
				{
					case VarAssign v:
						_vars[v.Name] = v.Expr.Evaluate(_vars);
						break;
					case CallStatement c:
						var args = new List<object>();
						foreach (var a in c.Args)
							args.Add(a.Evaluate(_vars));
						if (_native.TryGetValue(c.FuncName, out var native))
							native(args.ToArray());
						else
							throw new Exception($"Unknown function: {c.FuncName}");
						break;
				}
			}
		}
	}
	#endregion
}