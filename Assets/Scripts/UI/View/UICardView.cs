using Crockhead.Unity;
using Crockhead.Unity.UI;
using MillenniumOfCultivation.Battle;
using UnityEngine;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 카드 항목.
	/// </summary>
	[AssetPath("Assets/Resources/UI/UICardView.prefab", AssetPathType.Resources)]
	public class UICardView : UIDraggableItemView
	{
		#region INSPECTOR
		[SerializeField] private UILabelView m_NameLabel;
		[SerializeField] private UIImageView m_PortraitImage;
		[SerializeField] private UILabelView m_TypeLabel;
		[SerializeField] private UILabelView m_CostLabel;
		[SerializeField] private UILabelView m_ExplanationLabel;
		[SerializeField] private UIImageView m_SelectImage;
		#endregion

		/// <summary>
		/// 카드 데이터.
		/// </summary>
		private Card m_Card;

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

			if (m_NameLabel == null) m_NameLabel = GetOrAddComponent<UILabelView>("Portrait/Name/Background/LabelView");
			if (m_TypeLabel == null) m_TypeLabel = GetOrAddComponent<UILabelView>("Portrait/Type/Background/LabelView");
			if (m_CostLabel == null) m_CostLabel = GetOrAddComponent<UILabelView>("Cost/LabelView");
			if (m_ExplanationLabel == null) m_ExplanationLabel = GetOrAddComponent<UILabelView>("Explanation/Background/LabelView");
			if (m_SelectImage == null) m_SelectImage = GetOrAddComponent<UIImageView>("Select");

			m_Card = null;

			SetFocus(false);
		}

		/// <summary>
		/// 초기화됨.
		/// </summary>
		protected override void OnInitialize()
		{
			base.OnInitialize();
		}

		/// <summary>
		/// 카드 설정.
		/// </summary>
		public void SetData(Card card)
		{
			m_Card = card;
		}

		protected override void OnEntered()
		{
			base.OnEntered();

			//RectTransform.localScale = Vector3.one * 1.5f;
			SetFocus(true);
			RectTransform.SetAsLastSibling();
		}

		protected override void OnExited()
		{
			SetFocus(false);
			//RectTransform.localScale = Vector3.one * 1f;

			base.OnExited();
		}

		//protected override void OnSelected()
		//{
		//	base.OnSelected();

		//	SetFocus(true);
		//}

		//protected override void OnDeselected()
		//{
		//	SetFocus(false);

		//	base.OnDeselected();
		//}

		private void SetFocus(bool focused)
		{
			m_SelectImage.gameObject.SetActive(focused);
		}
	}
}