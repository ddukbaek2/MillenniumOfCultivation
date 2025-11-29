using System;
using System.Net.Security;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Crockhead.Unity.UI
{
	/// <summary>
	/// 사용자 조작이 가능한 동적 항목.
	/// </summary>
	public abstract class UIDraggableItemView : UIItemView, 
		IPointerEnterHandler, IPointerExitHandler, 
		IPointerDownHandler, IPointerUpHandler,
		IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		#region INSPECTOR
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

		/// <summary>
		/// 드래그 위치.
		/// </summary>
		private Vector2 m_Offset;

		/// <summary>
		/// 이전 위치.
		/// </summary>
		private Vector2 m_LastPosition;

		/// <summary>
		/// 선택 여부.
		/// </summary>
		private bool m_IsSelected;

		/// <summary>
		/// 선택 이벤트.
		/// </summary>
		public event Action<UIDraggableItemView> OnSelectionEvent;

		/// <summary>
		/// 선택 해제 이벤트.
		/// </summary>
		public event Action<UIDraggableItemView> OnDeselectionEvent;

		/// <summary>
		/// 드래그 시작 이벤트.
		/// </summary>
		public event Action<UIDraggableItemView> OnBeginDraggedEvent;

		/// <summary>
		/// 드래그 갱신 이벤트.
		/// </summary>
		public event Action<UIDraggableItemView> OnDraggedEvent;

		/// <summary>
		/// 드래그 완료 이벤트.
		/// </summary>
		public event Action<UIDraggableItemView> OnEndDraggedEvent;

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();

			BackgroundColor = Color.white;

			//RectTransform.anchoredPosition3D = Vector3.zero;
			//RectTransform.anchorMin = Vector2.one * 0.5f;
			//RectTransform.anchorMax = Vector2.one * 0.5f;
			//RectTransform.sizeDelta = new Vector2(240f, 400f);

			m_IsHovered = false;
			m_IsPressed = false;
			m_IsDragging = false;
			m_IsSelected = false;

			OnSelectionEvent = null;
			OnBeginDraggedEvent = null;
			OnDraggedEvent = null;
			OnEndDraggedEvent = null;
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
		/// 선택됨.
		/// </summary>
		protected virtual void OnSelected()
		{
			OnSelectionEvent?.Invoke(this);
		}

		/// <summary>
		/// 선택 해제됨.
		/// </summary>
		protected virtual void OnDeselected()
		{
			OnBeginDraggedEvent?.Invoke(this);
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
		/// 선택.
		/// </summary>
		public void Select()
		{
			if (m_IsSelected)
				return;

			m_IsSelected = true;
			OnSelected();

		}

		/// <summary>
		/// 선택 해제.
		/// </summary>
		public void Deselect()
		{
			if (!m_IsSelected)
				return;

			m_IsSelected = false;
			OnDeselected();
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
			m_IsPressed = true;

			if (m_IsSelected)
				return;

			Select();
		}

		/// <summary>
		/// 뗌.
		/// </summary>
		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			m_IsPressed = false;

			if (!m_IsSelected)
				return;

			Deselect();
		}

		/// <summary>
		/// 드래그 시작됨.
		/// </summary>
		void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
		{
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
			var parentRectTransform = RectTransform.parent as RectTransform;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, eventData.position, Window.Canvas.worldCamera, out var localPoint))
				return;

			//RectTransform.anchoredPosition = RectTransform.anchoredPosition + eventData.delta + offset;
			RectTransform.anchoredPosition = localPoint;
			OnDragged(m_LastPosition, localPoint);
			m_LastPosition = localPoint;
		}

		/// <summary>
		/// 드래그 완료됨.
		/// </summary>
		void IEndDragHandler.OnEndDrag(PointerEventData eventData)
		{
			m_IsDragging = false;
			OnEndDragged();
		}
	}
}