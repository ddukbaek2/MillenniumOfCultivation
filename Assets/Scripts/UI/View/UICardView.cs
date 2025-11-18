using Crockhead.Unity.UI;
using DG.Tweening;
using UnityEngine;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 카드 항목.
	/// </summary>
	public class UICardView : UIItemView
	{
		#region INSPECTOR
		//[SerializeField]
		#endregion

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			if (IsDestroyed())
				return;

			BackgroundColor = Color.white;

			//RectTransform.anchoredPosition3D = Vector3.zero;
			RectTransform.anchorMin = Vector2.one * 0.5f;
			RectTransform.anchorMax = Vector2.one * 0.5f;
			RectTransform.sizeDelta = new Vector2(320f, 480f);
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