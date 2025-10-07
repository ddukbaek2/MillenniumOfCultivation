using Crockhead.Core;
using UnityEngine;
using Crockhead.Scripting;


/// <summary>
/// 게임 진입점.
/// </summary>
public static class RuntimeInitializer
{
	/// <summary>
	/// 시작.
	/// </summary>
	[RuntimeInitializeOnLoadMethod]
	public static void Run()
	{
		Debug.Log("[RuntimeInitializer] Run()");
		SharedInstances.Clear();

		static Variable Print(Variable[] parameters)
		{
			if (parameters.Length == 0)
				return Variable.Null();

			var message = parameters[0].ToString();
			Debug.Log($"[Crockhead.Scripting] {message}");
			return Variable.Null();
		}

		var textAsset = Resources.Load<TextAsset>("script");
		var scriptEngine = new ScriptEngine();
		scriptEngine.Load(textAsset.text);
		scriptEngine.AddFunction("Print", Print);
		scriptEngine.Execute("doSomething");
	}
}