using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Crockhead.Unity.UI
{
	/// <summary>
	/// 스크롤 뷰.
	/// <para>
	/// 중첩되어도 드래그가 동작하는 스크롤렉트.
	///  - 참고 : https://gist.github.com/Josef212/8d296acdd4f457e39e0c13fbc9ec007e
	/// </para>
	/// </summary>
	//[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public class UIScrollView : ScrollRect
	{
		/// <summary>
		/// 렉트 트랜스폼.
		/// </summary>
		private RectTransform m_RectTransform;

		/// <summary>
		/// 부모 스크롤 렉트.
		/// </summary>
		private ScrollRect m_ParentScrollRect;

		/// <summary>
		/// 현재 객체에서는 조건 상 제외되어, 상위 객체로 넘기는 이벤트인지 여부.
		/// </summary>
		private bool m_IsEventPassedToUpwards;

		/// <summary>
		/// 현재 드래그 중인지 여부.
		/// </summary>
		private bool m_IsDragging;

		/// <summary>
		/// 부모 스크롤뷰가 가진 드래그 이벤트 핸들러 목록.
		/// </summary>
		private List<IInitializePotentialDragHandler> m_ParentInitializePotentialDragHandlers;

		/// <summary>
		/// 부모 스크롤뷰가 가진 드래그 이벤트 핸들러 목록.
		/// </summary>
		private List<IBeginDragHandler> m_ParentBeginDragHandlers;

		/// <summary>
		/// 부모 스크롤뷰가 가진 드래그 이벤트 핸들러 목록.
		/// </summary>
		private List<IDragHandler> m_ParentDragHandlers;

		/// <summary>
		/// 부모 스크롤뷰가 가진 드래그 이벤트 핸들러 목록.
		/// </summary>
		private List<IEndDragHandler> m_ParentEndDragHandlers;

		/// <summary>
		/// 렉트 트랜스폼 프로퍼티.
		/// </summary>
		public RectTransform RectTransform => m_RectTransform;

		/// <summary>
		/// 현재 드래그 중인지 여부 프로퍼티.
		/// </summary>
		public bool IsDragging => m_IsDragging;

		/// <summary>
		/// 드래그 시작 이벤트 프로퍼티.
		/// </summary>
		public Action<UIScrollView, PointerEventData> OnBeginDragEvent { set; get; }

		/// <summary>
		/// 드래그 진행 이벤트 프로퍼티.
		/// </summary>
		public Action<UIScrollView, PointerEventData> OnDragEvent { set; get; }

		/// <summary>
		/// 드래그 완료 이벤트 프로퍼티.
		/// </summary>
		public Action<UIScrollView, PointerEventData> OnEndDragEvent { set; get; }

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			m_RectTransform = GetComponent<RectTransform>();
			m_ParentScrollRect = null;
			m_IsEventPassedToUpwards = false;
			m_IsDragging = false;
			m_ParentInitializePotentialDragHandlers = new List<IInitializePotentialDragHandler>();
			m_ParentBeginDragHandlers = new List<IBeginDragHandler>();
			m_ParentDragHandlers = new List<IDragHandler>();
			m_ParentEndDragHandlers = new List<IEndDragHandler>();

			RebuildUpwards();
		}

		/// <summary>
		/// 초기화됨.
		/// </summary>
		protected override void Start()
		{
			base.Start();
		}

		/// <summary>
		/// 부모 스크롤 렉트 재설정. (이미 이벤트에서 호출하고 있으므로, 기본적으로는 외부에서의 호출은 필요 없음)
		/// </summary>
		public void RebuildUpwards()
		{
			static void SetEventHandler<TEventSystemHandler, TScrollRect>(List<TEventSystemHandler> eventSystemHandlers, TScrollRect parentScrollRect)
				where TEventSystemHandler : IEventSystemHandler
				where TScrollRect : ScrollRect
			{
				eventSystemHandlers.Clear();
				if (parentScrollRect == null)
					return;

				foreach (var eventSystemHandler in parentScrollRect.GetComponents<TEventSystemHandler>())
				{
					if (eventSystemHandlers.Contains(eventSystemHandler))
						continue;

					eventSystemHandlers.Add(eventSystemHandler);
				}
			}

			if (this == null || transform == null || transform.parent == null)
				return;

			// 부모 설정.
			m_ParentScrollRect = transform.parent.GetComponentInParent<ScrollRect>();
			SetEventHandler<IInitializePotentialDragHandler, ScrollRect>(m_ParentInitializePotentialDragHandlers, m_ParentScrollRect);
			SetEventHandler<IBeginDragHandler, ScrollRect>(m_ParentBeginDragHandlers, m_ParentScrollRect);
			SetEventHandler<IDragHandler, ScrollRect>(m_ParentDragHandlers, m_ParentScrollRect);
			SetEventHandler<IEndDragHandler, ScrollRect>(m_ParentEndDragHandlers, m_ParentScrollRect);
		}

		/// <summary>
		/// 드래그 초기화됨.
		/// </summary>
		public override void OnInitializePotentialDrag(PointerEventData pointerEventData)
		{
			RebuildUpwards();

			for (var i = 0; i < m_ParentInitializePotentialDragHandlers.Count; ++i)
			{
				var handler = m_ParentInitializePotentialDragHandlers[i];
				handler.OnInitializePotentialDrag(pointerEventData);
			}

			base.OnInitializePotentialDrag(pointerEventData);
		}

		/// <summary>
		/// 드래그 시작됨.
		/// </summary>
		public override void OnBeginDrag(PointerEventData pointerEventData)
		{
			m_IsDragging = true;

			RebuildUpwards();

			// 가로나 세로 축의 스크롤이 일어났을 때, 현재 스크롤 렉트에서는 사용하지 않는다면.
			var movedHorizontallyWhileScrollUnsupported = (!horizontal && Mathf.Abs(pointerEventData.delta.x) > Mathf.Abs(pointerEventData.delta.y));
			var movedVerticallyWhileScrollUnsupported = (!vertical && Mathf.Abs(pointerEventData.delta.x) < Mathf.Abs(pointerEventData.delta.y));

			// 발생한 스크롤 이벤트를 직접 처리하지 않고 상향 전달한다.
			m_IsEventPassedToUpwards = movedHorizontallyWhileScrollUnsupported || movedVerticallyWhileScrollUnsupported;

			if (m_IsEventPassedToUpwards)
			{
				for (var i = 0; i < m_ParentBeginDragHandlers.Count; ++i)
				{
					var handler = m_ParentBeginDragHandlers[i];
					handler.OnBeginDrag(pointerEventData);
				}
			}
			else
			{
				base.OnBeginDrag(pointerEventData);

				OnBeginDragEvent?.Invoke(this, pointerEventData);
			}
		}

		/// <summary>
		/// 드래그 진행됨.
		/// </summary>
		public override void OnDrag(PointerEventData pointerEventData)
		{
			RebuildUpwards();

			if (m_IsEventPassedToUpwards)
			{
				for (int i = 0; i < m_ParentDragHandlers.Count; ++i)
				{
					var handler = m_ParentDragHandlers[i];
					handler.OnDrag(pointerEventData);
				}
			}
			else
			{
				base.OnDrag(pointerEventData);

				OnDragEvent?.Invoke(this, pointerEventData);
			}
		}

		/// <summary>
		/// 드래그 종료됨.
		/// </summary>
		public override void OnEndDrag(PointerEventData pointerEventData)
		{
			m_IsDragging = false;

			RebuildUpwards();
			
			if (m_IsEventPassedToUpwards)
			{
				for (var i = 0; i < m_ParentEndDragHandlers.Count; ++i)
				{
					var handler = m_ParentEndDragHandlers[i];
					handler.OnEndDrag(pointerEventData);
				}
			}
			else
			{
				base.OnEndDrag(pointerEventData);

				OnEndDragEvent?.Invoke(this, pointerEventData);
			}

			m_IsEventPassedToUpwards = false;
		}
	}
}