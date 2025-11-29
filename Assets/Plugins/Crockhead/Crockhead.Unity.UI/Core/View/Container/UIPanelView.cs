using UnityEngine;


namespace Crockhead.Unity.UI
{
	/// <summary>
	/// 패널 화면.
	/// </summary>
	public class UIPanelView : UIView
	{
		#region INSPECTOR
		#endregion

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();

			// 색상 설정.
			BackgroundColor = Color.clear;
		}

		/// <summary>
		/// 초기화됨.
		/// </summary>
		protected override void OnInitialize()
		{
			base.OnInitialize();

			// 크기 설정.
			RectTransform.anchoredPosition3D = Vector3.zero;
			RectTransform.sizeDelta = Vector2.zero;
			RectTransform.anchorMin = Vector2.zero;
			RectTransform.anchorMax = Vector2.one;
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose()
		{
			base.OnDispose();
		}
	}
}