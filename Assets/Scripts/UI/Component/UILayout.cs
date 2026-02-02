using Crockhead.Unity.UI;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 레이아웃 처리기.
	/// </summary>
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public class UILayout : UIBehaviour, ILayoutGroup
	{
		public enum ApplyMode
		{
			/// <summary>
			/// 없음.
			/// </summary>
			None,

			/// <summary>
			/// 자신.
			/// </summary>
			Self,

			/// <summary>
			/// 계층적으로 하향식.
			/// </summary>
			Hierarchy,

			/// <summary>
			/// 계층적으로 상향식.
			/// </summary>
			HierarchyReverse,

			/// <summary>
			/// 지정 대상에 기반한.
			/// </summary>
			Specific,

			/// <summary>
			/// 수동.
			/// </summary>
			Menual,
		}


		#region INSPECTOR
		[NonSerialized] private RectTransform m_RectTransform;
		[SerializeField] private RectOffset m_Padding = new RectOffset();
		#endregion

		/// <summary>
		/// 렉트 트랜스폼 프로퍼티.
		/// </summary>
		protected RectTransform RectTransform
		{
			get
			{
				if (m_RectTransform == null)
					m_RectTransform = GetComponent<RectTransform>();
				return m_RectTransform;
			}
		}

		/// <summary>
		/// 패딩 프로퍼티.
		/// </summary>
		public RectOffset Padding { set => SetProperty(ref m_Padding, value); get => m_Padding; }

		/// <summary>
		/// 루트 여부 프로퍼티.
		/// </summary>
		public bool IsRootLayout
		{
			get
			{
				var parent = transform.parent;
				if (parent == null)
					return true;

				return transform.parent.GetComponent(typeof(ILayoutGroup)) == null;
			}
		}

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected UILayout()
		{
			if (m_Padding == null)
				m_Padding = new RectOffset();
		}

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();
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
		/// 활성화됨.
		/// </summary>
		protected override void OnEnable()
		{
			base.OnEnable();

			RectTransform.sendChildDimensionsChange = true;
			SetDirty();
			//UpdateLayout();
		}

		/// <summary>
		/// 비활성화됨.
		/// </summary>
		protected override void OnDisable()
		{
			LayoutRebuilder.MarkLayoutForRebuild(RectTransform);
			RectTransform.sendChildDimensionsChange = false;

			base.OnDisable();
		}

		/// <summary>
		/// Callback invoked by the auto layout system which handles horizontal aspects of the layout.
		/// </summary>
		void ILayoutController.SetLayoutHorizontal()
		{
			//SetDirty();
			UpdateLayout();
		}

		/// <summary>
		/// Callback invoked by the auto layout system which handles vertical aspects of the layout.
		/// </summary>
		void ILayoutController.SetLayoutVertical()
		{
			//SetDirty();
			UpdateLayout();
		}

		///// <summary>
		///// 부모 트랜스폼 갱신 직전.
		///// </summary>
		//protected override void OnBeforeTransformParentChanged()
		//{
		//	base.OnBeforeTransformParentChanged();
		//}

		///// <summary>
		///// 부모 트랜스폼 갱신 직후.
		///// </summary>
		//protected override void OnTransformParentChanged()
		//{
		//	base.OnTransformParentChanged();

		//	if (transform.parent == null)
		//	{
		//	}
		//	else
		//	{
		//	}
		//}

		/// <summary>
		/// 애니메이션으로 프로퍼티 변경됨.
		/// </summary>
		protected override void OnDidApplyAnimationProperties()
		{
			base.OnDidApplyAnimationProperties();

			SetDirty();
		}

		//protected override void OnCanvasGroupChanged()
		//{
		//	base.OnCanvasGroupChanged();
		//}

		//protected override void OnCanvasHierarchyChanged()
		//{
		//	base.OnCanvasHierarchyChanged();
		//}

		/// <summary>
		/// 크기가 변경된 직후.
		/// </summary>
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();

			//UpdateLayout();
			SetDirty();
		}

		private void OnTransformChildrenChanged()
		{
			SetDirty();
		}

		private void OnChildRectTransformDimensionsChange()
		{
			if (!CanvasUpdateRegistry.IsRebuildingLayout())
				SetDirty();
		}

		/// <summary>
		/// 레이아웃 갱신.
		/// </summary>
		public void UpdateLayout()
		{
			if (RectTransform == null)
				return;

			var labelView = GetComponent<UILabelView>();
			if (labelView != null)
				UpdateLabelView(RectTransform, labelView);
		}

		/// <summary>
		/// 레이블뷰의 크기를 갱신.
		/// </summary>
		private void UpdateLabelView(RectTransform rectTransform, UILabelView labelView)
		{
			if (rectTransform == null || labelView == null)
				return;

			var sizeDelta = new Vector2(labelView.preferredWidth, labelView.preferredHeight);
			SetSizeDelta(rectTransform, sizeDelta, true, true);
		}

		/// <summary>
		/// 크기 설정.
		/// </summary>
		private void SetSizeDelta(RectTransform rectTransform, Vector2 sizeDelta, bool horizontal, bool vertical)
		{
			if (horizontal)
			{
				rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sizeDelta.x);
			}

			if (vertical)
			{
				rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sizeDelta.y);
			}
		}

		/// <summary>
		/// 갱신 요청.
		/// </summary>
		protected void SetDirty()
		{
			static IEnumerator DelayedSetDirty(RectTransform rectTransform)
			{
				yield return null;
				LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
			}

			if (!IsActive())
				return;

			if (CanvasUpdateRegistry.IsRebuildingLayout())
			{
				StartCoroutine(DelayedSetDirty(RectTransform));
			}
			else
			{
				LayoutRebuilder.MarkLayoutForRebuild(RectTransform);
			}
		}

		/// <summary>
		/// 프로퍼티 설정.
		/// </summary>
		protected bool SetProperty<T>(ref T currentValue, T newValue)
		{
			if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
				return false;

			currentValue = newValue;
			SetDirty();
			return true;
		}

#if UNITY_EDITOR
		/// <summary>
		/// 검증.
		/// </summary>
		protected override void OnValidate()
		{
			SetDirty();
		}
#endif
	}
}