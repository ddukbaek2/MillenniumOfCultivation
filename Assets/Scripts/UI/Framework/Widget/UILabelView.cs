using TMPro;
using UnityEngine;


namespace Crockhead.Unity.UI
{
	/// <summary>
	/// 레이블 뷰.
	/// </summary>
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(CanvasRenderer))]
	public sealed class UILabelView : TextMeshProUGUI, IUIWidget, IUIFrameable
	{
		#region INSPECTOR
		[SerializeField] private string m_LocalizeKey;
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
		public RectTransform RectTransform => rectTransform;

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			m_Frame = new UIFrame();
		}

		/// <summary>
		/// 활성화됨.
		/// </summary>
		protected override void OnEnable()
		{
			base.OnEnable();
#if UNITY_EDITOR
			HideChildrenInHierarchy();
#endif
		}

		/// <summary>
		/// 자식 트랜스폼이 변경됨.
		/// </summary>
		private void OnTransformChildrenChanged()
		{
#if UNITY_EDITOR
			HideChildrenInHierarchy();
#endif
		}

#if UNITY_EDITOR
		/// <summary>
		/// 하이어라키 상에서 서브메쉬를 감춘다.
		/// <para>에디터 전용.</para>
		/// </summary>
		public void HideChildrenInHierarchy()
		{
			foreach (Transform child in transform)
				child.gameObject.hideFlags = HideFlags.HideInHierarchy | HideFlags.DontSave;
		}
#endif

		/// <summary>
		/// 컴포넌트 프로퍼티 정합성 검사.
		/// </summary>
		protected override void OnValidate()
		{
			base.OnValidate();

			//if (!string.IsNullOrWhiteSpace(m_DefaultTag) && !long.TryParse(m_DefaultTag, out var defaultTag))
			//{
			//	Debug.LogWarning($"[UIKitForUnity] Wrong Default Tag: {m_DefaultTag}");
			//}
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