using Crockhead.Unity;
using Crockhead.Unity.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 마우스 계층 뷰.
	/// </summary>
	[AssetPath("Assets/Resources/UI/UIMouseView.prefab", AssetPathType.Resources)]
	public class UIMouseView : UIPanelView,
		IPointerEnterHandler, IPointerExitHandler,
		IPointerDownHandler, IPointerUpHandler,
		IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		#region INSPECTOR
		//[SerializeField] private Image m_LabelView;
		#endregion

		/// <summary>
		/// 진입 여부.
		/// </summary>
		private bool m_IsHovered;

		/// <summary>
		/// 눌림 여부.
		/// </summary>
		private bool m_IsPressed;

		/// <summary>
		/// 드래그 상태 여부.
		/// </summary>
		private bool m_IsDragging;

		///// <summary>
		///// 드래그 위치.
		///// </summary>
		//private Vector2 m_Offset;

		/// <summary>
		/// 이전 위치.
		/// </summary>
		private Vector2 m_LastPosition;

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();

			BackgroundColor = Color.clear;

			m_IsHovered = false;
			m_IsPressed = false;
			m_IsDragging = false;
			//m_Offset = Vector2.zero;
			m_LastPosition = Vector2.zero;
		}

		/// <summary>
		/// 초기화됨.
		/// </summary>
		protected override void OnInitialize()
		{
			base.OnInitialize();
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose()
		{
			base.OnDispose();
		}

		/// <summary>
		/// 진입됨.
		/// </summary>
		protected virtual void OnEntered()
		{
		}

		/// <summary>
		/// 탈출됨.
		/// </summary>
		protected virtual void OnExited()
		{
		}

		/// <summary>
		/// 드래그 시작됨.
		/// </summary>
		protected virtual void OnBeginDragged()
		{
		}

		/// <summary>
		/// 드래그됨.
		/// </summary>
		protected virtual void OnDragged(Vector2 previous, Vector2 next)
		{
		}

		/// <summary>
		/// 드래그 완료됨.
		/// </summary>
		protected virtual void OnEndDragged()
		{
		}

		/// <summary>
		/// 들어옴.
		/// </summary>
		void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
		{
			if (m_IsHovered)
				return;

			m_IsHovered = true;
			OnEntered();
		}

		/// <summary>
		/// 나감.
		/// </summary>
		void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
		{
			if (!m_IsHovered)
				return;

			m_IsHovered = false;
			OnExited();
		}

		/// <summary>
		/// 눌림.
		/// </summary>
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			if (m_IsPressed)
				return;

			m_IsPressed = true;
		}

		/// <summary>
		/// 뗌.
		/// </summary>
		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			if (!m_IsPressed)
				return;

			m_IsPressed = false;
		}

		/// <summary>
		/// 드래그 시작됨.
		/// </summary>
		void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
		{
			if (m_IsDragging)
				return;

			m_IsDragging = true;
			OnBeginDragged();

			RectTransformUtility.ScreenPointToLocalPointInRectangle(RectTransform, eventData.position, Window.Canvas.worldCamera, out var localPoint);
			m_LastPosition = localPoint;
		}

		/// <summary>
		/// 드래그 갱신됨.
		/// </summary>
		void IDragHandler.OnDrag(PointerEventData eventData)
		{
			if (!m_IsDragging)
				return;

			var parentRectTransform = RectTransform.parent as RectTransform;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, eventData.position, Window.Canvas.worldCamera, out var localPoint))
				return;

			////RectTransform.anchoredPosition = RectTransform.anchoredPosition + eventData.delta + offset;
			//RectTransform.anchoredPosition = localPoint;
			OnDragged(m_LastPosition, localPoint);
			m_LastPosition = localPoint;
		}

		/// <summary>
		/// 드래그 완료됨.
		/// </summary>
		void IEndDragHandler.OnEndDrag(PointerEventData eventData)
		{
			if (!m_IsDragging)
				return;

			m_IsDragging = false;
			OnEndDragged();
		}
	}
}