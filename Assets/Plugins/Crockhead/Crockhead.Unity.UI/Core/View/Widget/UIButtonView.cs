using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Crockhead.Unity.UI
{
	/// <summary>
	/// 버튼 뷰.
	/// </summary>
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public sealed partial class UIButtonView : Button, IUIWidget, IUIFrameable
	{
		#region INSPECTOR
		[SerializeField] private RectTransform m_RectTransform;
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

		///// <summary>
		///// 포커스 표시 객체.
		///// </summary>
		//private Outline m_Outline;

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			if (m_RectTransform == null)
			{
				m_RectTransform = GetComponent<RectTransform>();
			}

			m_Frame = new UIFrame();

			//if (Application.isPlaying)
			//{
			//	m_Outline = gameObject.GetComponent<Outline>();
			//	if (m_Outline == null)
			//		m_Outline = gameObject.AddComponent<Outline>();
			//	m_Outline.effectColor = Color.red;
			//	m_Outline.effectDistance = new Vector3(4f, 4f);
			//	m_Outline.enabled = false;
			//}
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		protected override void OnDestroy()
		{
			base.OnDestroy();
		}

		/// <summary>
		/// 선택됨.
		/// <para>ISelectHandler 인터페이스 구현.</para>
		/// </summary>
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);

			if (Application.isPlaying)
			{
				Debug.Log($"[UIButtonView] OnSelect(): {name}");
				//m_Outline.enabled = true;
			}
		}

		/// <summary>
		/// 선택해제됨.
		/// <para>IDeselectHandler 인터페이스 구현.</para>
		/// </summary>
		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);

			if (Application.isPlaying)
			{
				Debug.Log($"[UIButtonView] OnDeselect(): {name}");
				//m_Outline.enabled = false;
			}
		}

#if UNITY_EDITOR
		/// <summary>
		/// 유효성 검사시 호출됨.
		/// </summary>
		protected override void OnValidate()
		{
			base.OnValidate();

			//if (!string.IsNullOrWhiteSpace(m_DefaultTag) && !long.TryParse(m_DefaultTag, out var defaultTag))
			//{
			//	Debug.LogWarning($"[UIKitForUnity] Wrong Default Tag: {m_DefaultTag}");
			//}
		}
#endif

		///// <summary>
		///// 버튼 설정.
		///// </summary>
		//public void SetTitle(string title)
		//{
		//}

		///// <summary>
		///// 액션 추가.
		///// </summary>
		//public void AddAction(UIAction action)
		//{
		//}

		/// <summary>
		/// 프레임 갱신.
		/// </summary>
		public void FrameUpdate()
		{
			// m_Frame
		}

		/// <summary>
		/// 레이아웃 갱신.
		/// </summary>
		public void LayoutUpdate()
		{
			// m_Frame
		}
	}
}