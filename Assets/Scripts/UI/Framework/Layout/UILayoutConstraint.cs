using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Crockhead.Unity.UI
{
	/// <summary>
	/// UI 레이아웃 처리기.
	/// </summary>
	[RequireComponent(typeof(RectTransform))]
	[ExecuteAlways]
	public abstract partial class UILayoutConstraint : UIBehaviour
	{
		#region INSPECTOR
		[SerializeField] private RectTransform m_RectTransform;
		[SerializeField] private Target m_HorizontalTarget;
		[SerializeField] private Target m_VerticalTarget;
		[SerializeField] private Vector2 m_Size;
		[SerializeField] private Rect m_LayoutBound;

		#endregion

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
		}

		/// <summary>
		/// 비활성화됨.
		/// </summary>
		protected override void OnDisable()
		{
			base.OnDisable();
		}

		/// <summary>
		/// 갱신됨.
		/// </summary>
		protected virtual void LateUpdate()
		{
			UpdateAllLayouts();
		}

		/// <summary>
		/// 크기 변경됨.
		/// </summary>
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
		}

		/// <summary>
		/// 캔버스 계층 변경됨.
		/// </summary>
		protected override void OnCanvasHierarchyChanged()
		{
			base.OnCanvasHierarchyChanged();
		}

		/// <summary>
		/// 캔버스 그룹 변경됨.
		/// </summary>
		protected override void OnCanvasGroupChanged()
		{
			base.OnCanvasGroupChanged();
		}

		/// <summary>
		/// 부모 변경 직전 호출됨.
		/// </summary>
		protected override void OnBeforeTransformParentChanged()
		{
			base.OnBeforeTransformParentChanged();
		}

		/// <summary>
		/// 부모 변경됨.
		/// </summary>
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
		}

		/// <summary>
		/// 유효성 검사시 호출됨.
		/// </summary>
		protected override void OnValidate()
		{
			base.OnValidate();
		}

		/// <summary>
		/// 애니메이션 프로퍼티 적용됨.
		/// </summary>
		protected override void OnDidApplyAnimationProperties()
		{
			base.OnDidApplyAnimationProperties();
		}
	}
}
