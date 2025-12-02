using TMPro;
using UnityEngine;


namespace Crockhead.Unity.UI
{
	/// <summary>
	/// 입력 뷰.
	/// </summary>
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(CanvasRenderer))]
	public sealed class UIInputView : TMP_InputField, IUIView, IUIConstraintable
	{
		#region INSPECTOR
		//[SerializeField] private RectTransform m_RectTransform;
		#endregion

		/// <summary>
		/// 프레임.
		/// </summary>
		private UIFrame m_Frame;

		/// <summary>
		/// 프레임 프로퍼티.
		/// </summary>
		public UIFrame Frame => m_Frame;

		/// <summary>
		/// 렉트 트랜스폼 프로퍼티.
		/// </summary>
		public RectTransform RectTransform => m_RectTransform;

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			m_Frame = new UIFrame();

			if (m_RectTransform == null)
			{
				m_RectTransform = GetComponent<RectTransform>();
			}
		}

		/// <summary>
		/// 프레임 갱신.
		/// </summary>
		public void FrameUpdate()
		{
		}

		/// <summary>
		/// 레이아웃 갱신.
		/// </summary>
		public void LayoutUpdate()
		{
		}
	}
}