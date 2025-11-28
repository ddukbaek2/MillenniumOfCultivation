using Crockhead.Unity;
using Crockhead.Unity.UI;
using MillenniumOfCultivation.Battle;
using System;
using UnityEngine;
using UnityEngine.EventSystems;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 카드 항목.
	/// </summary>
	[AssetPath("Assets/Resources/UI/UICardView.prefab", AssetPathType.Resources)]
	public class UICardView : UIItemView, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
	{
		#region INSPECTOR
		[SerializeField] private UILabelView m_NameLabel;
		[SerializeField] private UIImageView m_PortraitImage;
		[SerializeField] private UILabelView m_TypeLabel;
		[SerializeField] private UILabelView m_CostLabel;
		[SerializeField] private UILabelView m_ExplanationLabel;
		[SerializeField] private UIImageView m_FocusImage;
		#endregion

		private Action OnPressed { set; get; }

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();

			BackgroundColor = Color.white;

			//RectTransform.anchoredPosition3D = Vector3.zero;
			RectTransform.anchorMin = Vector2.one * 0.5f;
			RectTransform.anchorMax = Vector2.one * 0.5f;
			RectTransform.sizeDelta = new Vector2(240f, 400f);

			if (m_NameLabel == null) m_NameLabel = GetOrAddComponent<UILabelView>("Portrait/Name/Background/Label");
			if (m_TypeLabel == null) m_TypeLabel = GetOrAddComponent<UILabelView>("Portrait/Type/Background/Label");
			if (m_CostLabel == null) m_CostLabel = GetOrAddComponent<UILabelView>("Cost/Label");
			if (m_ExplanationLabel == null) m_ExplanationLabel = GetOrAddComponent<UILabelView>("Explanation/Background/Label");
			if (m_FocusImage == null) m_FocusImage = GetOrAddComponent<UIImageView>("Focus");
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

		/// <summary>
		/// 카드 설정.
		/// </summary>
		public void SetCard(Card card)
		{
		}

		void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
		{
		}

		void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
		{
		}

		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			m_FocusImage.gameObject.SetActive(true);
		}

		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			m_FocusImage.gameObject.SetActive(false);
		}

		void IPointerMoveHandler.OnPointerMove(PointerEventData eventData)
		{
		}
	}
}