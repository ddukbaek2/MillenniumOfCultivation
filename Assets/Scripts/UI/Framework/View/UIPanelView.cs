using UnityEngine;


namespace MillenniumOfCultivation.UI
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
		protected override void Awake()
		{
			base.Awake();

			if (IsDestroyed())
				return;

			// 색상 설정.
			BackgroundColor = Color.clear;

			// 크기 설정.
			RectTransform.anchoredPosition3D = Vector3.zero;
			RectTransform.sizeDelta = Vector2.zero;
			RectTransform.anchorMin = Vector2.zero;
			RectTransform.anchorMax = Vector2.one;
		}

		/// <summary>
		/// 초기화됨.
		/// </summary>
		protected override void Start()
		{
			base.Start();
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		protected override void OnDestroy()
		{
			base.OnDestroy();
		}
	}
}