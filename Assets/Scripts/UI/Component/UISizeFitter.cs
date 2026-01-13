using Crockhead.Unity.UI;
using UnityEngine;
using UnityEngine.EventSystems;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 사이즈 반영 처리기.
	/// </summary>
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public class UISizeFitter : UIBehaviour
	{
		#region INSPECTOR
		[SerializeField] private bool m_Horizontal;
		[SerializeField] private bool m_Vertical;
		[SerializeField] private bool m_UseChildSizeDelta;
		[SerializeField] private Vector2 m_AdditionalSizeDelta = Vector2.zero;
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

			RebuildSizeFit();
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
		protected virtual void Update()
		{
			RebuildSizeFit();
		}

		protected override void OnBeforeTransformParentChanged()
		{
			base.OnBeforeTransformParentChanged();
		}

		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
		}

		//protected override void OnDidApplyAnimationProperties()
		//{
		//	base.OnDidApplyAnimationProperties();
		//}

		//protected override void OnCanvasGroupChanged()
		//{
		//	base.OnCanvasGroupChanged();
		//}

		//protected override void OnCanvasHierarchyChanged()
		//{
		//	base.OnCanvasHierarchyChanged();
		//}

		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();

			RebuildSizeFit();
		}

		/// <summary>
		/// 갱신.
		/// </summary>
		public void RebuildSizeFit()
		{
			var rectTransform = GetComponent<RectTransform>();
			if (rectTransform == null)
				return;

			if (m_UseChildSizeDelta)
			{
				var sizeDelta = Vector2.zero;
				foreach (RectTransform childRectTransform in rectTransform)
				{
					var localCenterAndSize = UINode.GetLocalCenterAndSize(childRectTransform, rectTransform);
					if (sizeDelta.x < localCenterAndSize.Size.x)
						sizeDelta.x = localCenterAndSize.Size.x;
					if (sizeDelta.y < localCenterAndSize.Size.y)
						sizeDelta.y = localCenterAndSize.Size.y;
				}

				SetSizeDelta(rectTransform, sizeDelta);
			}
			else
			{
				var labelView = GetComponent<UILabelView>();
				if (labelView != null) UpdateLabelView(rectTransform, labelView);
			}
		}

		/// <summary>
		/// 레이블뷰의 크기를 갱신.
		/// </summary>
		private void UpdateLabelView(RectTransform rectTransform, UILabelView labelView)
		{
			if (rectTransform == null || labelView == null)
				return;

			var sizeDelta = new Vector2(labelView.preferredWidth, labelView.preferredHeight);
			SetSizeDelta(rectTransform, sizeDelta);
		}

		/// <summary>
		/// 크기 설정.
		/// </summary>
		private void SetSizeDelta(RectTransform rectTransform, Vector2 sizeDelta)
		{
			if (m_Horizontal)
			{
				rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sizeDelta.x + m_AdditionalSizeDelta.x);
			}

			if (m_Vertical)
			{
				rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sizeDelta.y + m_AdditionalSizeDelta.y);
			}
		}
	}
}