using Crockhead.Unity;
using Crockhead.Unity.UI;
using UnityEngine;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 카드 항목.
	/// </summary>
	[AssetPath("Assets/Resources/UI/UICardView.prefab", AssetPathType.Resources)]
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

			if (!Application.isPlaying)
				return;

			BackgroundColor = Color.white;

			//RectTransform.anchoredPosition3D = Vector3.zero;
			RectTransform.anchorMin = Vector2.one * 0.5f;
			RectTransform.anchorMax = Vector2.one * 0.5f;
			RectTransform.sizeDelta = new Vector2(240f, 400f);
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