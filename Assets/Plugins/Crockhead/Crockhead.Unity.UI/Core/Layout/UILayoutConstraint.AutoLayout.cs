using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Crockhead.Unity.UI
{
	/// <summary>
	/// UI 레이아웃 처리기.
	/// </summary>
	public abstract partial class UILayoutConstraint
	{
		/// <summary>
		/// 대상.
		/// </summary>
		public enum Target
		{
			None = 0,
			Self,
			Parent,
			Child,
		}


		/// <summary>
		/// 속성.
		/// </summary>
		public enum Attribute
		{
			Left,
			Right,
			Top,
			Bottom,
			X,
			Y,
			Width,
			Height,
		}


		public enum Relation
		{
			Equal,
			LessEqual,
			GreaterEqual,
		}


		public class LayoutConstraint
		{
			public Relation Relation;
			public RectTransform Item1;
			public Attribute Attribute1;
			public RectTransform Item2;
			public Attribute Attribute2;
			public float Multiplier;
			public float Constant;
		}


		/// <summary>
		/// 레이아웃 업데이트.
		/// </summary>
		public void UpdateAllLayouts()
		{

		}
	}
}