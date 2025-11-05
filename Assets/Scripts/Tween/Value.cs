using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


namespace MillenniumOfCultivation.Tween
{
	/// <summary>
	/// 공용체 기반 값 집합.
	/// </summary>
	[StructLayout(LayoutKind.Explicit, Pack = 4)]
	public struct Value
	{
		/// <summary>
		/// 값 타입 목록 프로퍼티.
		/// </summary>
		public static Dictionary<ValueType, Type> ValueTypes { get; } = new Dictionary<ValueType, Type>()
		{
			{ ValueType.Boolean, typeof(Boolean) }, // bool.
			{ ValueType.Char, typeof(Char) }, // char.
			{ ValueType.SByte, typeof(SByte) }, // sbyte.
			{ ValueType.Short, typeof(Int16) }, // short.
			{ ValueType.Integer, typeof(Int32) }, // int.
			{ ValueType.Long, typeof(Int64) }, // long.
			{ ValueType.Byte, typeof(Byte) }, // byte.
			{ ValueType.UShort, typeof(UInt16) }, // ushort.
			{ ValueType.UInteger, typeof(UInt32) }, // uint.
			{ ValueType.ULong, typeof(UInt64) }, // ulong.
			{ ValueType.Float, typeof(Single) }, // float.
			{ ValueType.Double, typeof(Double) }, // double.
			{ ValueType.Vector2, typeof(Vector2) },
			{ ValueType.Vector3, typeof(Vector3) },
			{ ValueType.Vector4, typeof(Vector4) },
			{ ValueType.Rect, typeof(Rect) },
		};



		[FieldOffset(0)] private ValueType m_Type; // 4byte.
		[FieldOffset(4)] private int  m_Padding; // 4byte. (placeholder: alignment-padding)

		[FieldOffset(8)] private bool m_BoolValue; // 1byte.
		[FieldOffset(8)] private char m_CharValue; // 2byte. (ushort)

		[FieldOffset(8)] private sbyte m_SByteValue; // 1byte.
		[FieldOffset(8)] private short m_ShortValue; // 2byte.
		[FieldOffset(8)] private int m_IntValue; // 4byte.
		[FieldOffset(8)] private long m_LongValue; // 8byte.
		[FieldOffset(8)] private byte m_ByteValue; // 1byte.
		[FieldOffset(8)] private ushort m_UShortValue; // 2byte.
		[FieldOffset(8)] private uint m_UIntValue; // 4byte.
		[FieldOffset(8)] private ulong m_ULongValue; // 8byte.

		[FieldOffset(8)] private float m_FloatValue; // 4byte.
		[FieldOffset(8)] private double m_DoubleValue; // 8byte.

		[FieldOffset(8)] private Vector2 m_Vector2Value; // 8byte.
		[FieldOffset(8)] private Vector3 m_Vector3Value; // 12byte.
		[FieldOffset(8)] private Vector4 m_Vector4Value; // 16byte.
		[FieldOffset(8)] private Rect m_RectValue; // 16byte.

		/// <summary>
		/// 타입 프로퍼티.
		/// </summary>
		public ValueType Type => m_Type;

		/// <summary>
		/// 논리형 값 프로퍼티.
		/// </summary>
		public bool BoolValue => m_BoolValue;

		/// <summary>
		/// 문자 값 프로퍼티.
		/// </summary>
		public char CharValue => m_CharValue;

		/// <summary>
		/// 정수 값 프로퍼티.
		/// </summary>
		public short SByteValue => m_SByteValue;

		/// <summary>
		/// 정수 값 프로퍼티.
		/// </summary>
		public short ShortValue => m_ShortValue;

		/// <summary>
		/// 정수 값 프로퍼티.
		/// </summary>
		public int IntValue => m_IntValue;

		/// <summary>
		/// 정수 값 프로퍼티.
		/// </summary>
		public long LongValue => m_LongValue;

		/// <summary>
		/// 부호 없는 정수 값 프로퍼티.
		/// </summary>
		public byte ByteValue => m_ByteValue;

		/// <summary>
		/// 부호 없는 정수 값 프로퍼티.
		/// </summary>
		public ushort UShortValue => m_UShortValue;

		/// <summary>
		/// 부호 없는 정수 값 프로퍼티.
		/// </summary>
		public uint UIntValue => m_UIntValue;

		/// <summary>
		/// 부호 없는 정수 값 프로퍼티.
		/// </summary>
		public ulong ULongValue => m_ULongValue;

		/// <summary>
		/// 실수 값 프로퍼티.
		/// </summary>
		public float FloatValue => m_FloatValue;

		/// <summary>
		/// 실수 값 프로퍼티.
		/// </summary>
		public double DoubleValue => m_DoubleValue;

		/// <summary>
		/// 유니티 2차원 벡터 구조체 값 프로퍼티.
		/// </summary>
		public Vector2 Vector2Value => m_Vector2Value;

		/// <summary>
		/// 유니티 3차원 벡터 구조체 값 프로퍼티.
		/// </summary>
		public Vector3 Vector3Value => m_Vector3Value;

		/// <summary>
		/// 유니티 4차원 벡터 구조체 값 프로퍼티.
		/// </summary>
		public Vector4 Vector4Value => m_Vector4Value;

		/// <summary>
		/// 유니티 사각형 구조체 값 프로퍼티.
		/// </summary>
		public Rect RectValue => m_RectValue;

		///// <summary>
		///// 생성됨.
		///// </summary>
		//public Value(ValueType type)
		//{
		//	if (type == ValueType.Unknown)
		//		throw new ArgumentException($"{type}");

		//	m_Type = type;
		//	m_Padding = 0;

		//	switch (m_Type)
		//	{
		//		case ValueType.Boolean: m_BoolValue = default; break;
		//		case ValueType.Char: m_CharValue = default; break;
		//		case ValueType.Short: m_ShortValue = default; break;
		//		case ValueType.SByte: m_SByteValue = default; break;
		//		case ValueType.Integer: m_IntValue = default; break;
		//		case ValueType.Long: m_LongValue = default; break;
		//		case ValueType.Byte: m_ByteValue = default; break;
		//		case ValueType.UShort: m_UShortValue = default; break;
		//		case ValueType.UInteger: m_UIntValue = default; break;
		//		case ValueType.ULong: m_ULongValue = default; break;
		//		case ValueType.Float: m_FloatValue = default; break;
		//		case ValueType.Double: m_DoubleValue = default; break;
		//		case ValueType.Vector2: m_Vector2Value = Vector2.zero; break;
		//		case ValueType.Vector3: m_Vector3Value = Vector3.zero; break;
		//		case ValueType.Vector4: m_Vector4Value = Vector4.zero; break;
		//		case ValueType.Rect: m_RectValue = Rect.zero; break;
		//	}
		//}

		/// <summary>
		/// 문자열 반환.
		/// </summary>
		public override string ToString()
		{
			//return base.ToString();
			var value = GetValue();
			return value.ToString();
		}

		/// <summary>
		/// 값 설정.
		/// </summary>
		public void SetValue(object value)
		{
			try
			{
				switch (m_Type)
				{
					case ValueType.Boolean: m_BoolValue = (bool)value; break;
					case ValueType.Char: m_CharValue = (char)value; break;
					case ValueType.SByte: m_SByteValue = (sbyte)value; break;
					case ValueType.Short: m_ShortValue = (short)value; break;
					case ValueType.Integer: m_IntValue = (int)value; break;
					case ValueType.Long: m_LongValue = (long)value; break;
					case ValueType.Byte: m_ByteValue = (byte)value; break;
					case ValueType.UShort: m_UShortValue = (ushort)value; break;
					case ValueType.UInteger: m_UIntValue = (uint)value; break;
					case ValueType.ULong: m_ULongValue = (ulong)value; break;
					case ValueType.Float: m_FloatValue = (float)value; break;
					case ValueType.Double: m_DoubleValue = (double)value; break;
					case ValueType.Vector2: m_Vector2Value = (Vector2)value; break;
					case ValueType.Vector3: m_Vector3Value = (Vector3)value; break;
					case ValueType.Vector4: m_Vector4Value = (Vector4)value; break;
					case ValueType.Rect: m_RectValue = (Rect)value; break;
					default:
						throw new NotImplementedException($"{m_Type}");
				}
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// 값 반환.
		/// </summary>
		public object GetValue()
		{
			switch (m_Type)
			{
				case ValueType.Boolean: return m_BoolValue;
				case ValueType.Char: return m_CharValue;
				case ValueType.SByte: return m_SByteValue;
				case ValueType.Short: return m_ShortValue;
				case ValueType.Integer: return m_IntValue;
				case ValueType.Long: return m_LongValue;
				case ValueType.Byte: return m_ByteValue;
				case ValueType.UShort: return m_UShortValue;
				case ValueType.UInteger: return m_UIntValue;
				case ValueType.ULong: return m_ULongValue;
				case ValueType.Float: return m_FloatValue;
				case ValueType.Double: return m_DoubleValue;
				case ValueType.Vector2: return m_Vector2Value;
				case ValueType.Vector3: return m_Vector3Value;
				case ValueType.Vector4: return m_Vector4Value;
				case ValueType.Rect: return m_RectValue;
				default:
					throw new NotImplementedException($"{m_Type}");
			}
		}

		/// <summary>
		/// 생성.
		/// </summary>
		public static Value Create(ValueType type)
		{
			if (type == ValueType.Unknown)
				throw new ArgumentException($"{type}");

			var value = new Value();
			value.m_Type = type;

			switch (value.m_Type)
			{
				case ValueType.Boolean: value.m_BoolValue = default; break;
				case ValueType.Char: value.m_CharValue = default; break;
				case ValueType.Short: value.m_ShortValue = default; break;
				case ValueType.SByte: value.m_SByteValue = default; break;
				case ValueType.Integer: value.m_IntValue = default; break;
				case ValueType.Long: value.m_LongValue = default; break;
				case ValueType.Byte: value.m_ByteValue = default; break;
				case ValueType.UShort: value.m_UShortValue = default; break;
				case ValueType.UInteger: value.m_UIntValue = default; break;
				case ValueType.ULong: value.m_ULongValue = default; break;
				case ValueType.Float: value.m_FloatValue = default; break;
				case ValueType.Double: value.m_DoubleValue = default; break;
				case ValueType.Vector2: value.m_Vector2Value = Vector2.zero; break;
				case ValueType.Vector3: value.m_Vector3Value = Vector3.zero; break;
				case ValueType.Vector4: value.m_Vector4Value = Vector4.zero; break;
				case ValueType.Rect: value.m_RectValue = Rect.zero; break;
			}

			return value;
		}

		/// <summary>
		/// 값 타입 추정 반환.
		/// </summary>
		public static ValueType GetValueType(object unknownValue)
		{
			if (unknownValue == null)
				throw new ArgumentNullException(nameof(unknownValue));

			if (unknownValue is Value value)
			{
				return value.m_Type;
			}
			else
			{
				var valueType = unknownValue.GetType();
				foreach (var pair in ValueTypes)
				{
					if (pair.Value == valueType)
						return pair.Key;
				}

				//throw new InvalidOperationException();
				return ValueType.Unknown;
			}
		}
	}
}