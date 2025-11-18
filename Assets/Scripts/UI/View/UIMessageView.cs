using Crockhead.Unity.UI;
using UnityEngine;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 메시지 뷰.
	/// </summary>
	public class UIMessageView : UIPanelView
	{
		#region INSPECTOR
		[SerializeField] private RectTransform m_ContentRectTransform;
		#endregion

		/// <summary>
		/// 콘텐트 렉트 트랜스폼 프로퍼티.
		/// </summary>
		public RectTransform ContentRectTransform => m_ContentRectTransform;

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			if (IsDestroyed())
				return;

			BackgroundColor = Color.black;

			if (m_ContentRectTransform == null)
			{
				m_ContentRectTransform = GetOrAddComponent<RectTransform>("ScrollView/Viewport/Content");
			}
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
		/// 모든 메시지 제거.
		/// </summary>
		public void RemoveAllMessages()
		{
		}

		/// <summary>
		/// 메시지 추가.
		/// </summary>
		public void AddMessage()
		{
		}
	}
}