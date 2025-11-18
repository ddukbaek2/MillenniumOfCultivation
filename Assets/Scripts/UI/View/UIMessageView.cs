using Crockhead.Unity.UI;
using System.Collections.Generic;
using TMPro;
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
		[SerializeField] private TMP_InputField m_InputField;
		#endregion

		private Dictionary<string, List<Message>> m_CachedMessages;

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

			if (m_InputField == null)
			{
				m_InputField = GetOrAddComponent<TMP_InputField>("InputField");
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
		/// 갱신됨.
		/// </summary>
		protected virtual void Update()
		{
			PullingAllMessages();
		}

		/// <summary>
		/// 모든 메시지를 꺼내온다.
		/// </summary>
		public void PullingAllMessages()
		{
			if (!MessageManager.Instance.IsConnected)
				return;
			if (MessageManager.Instance.JoinedChannelCount == 0)
				return;

			foreach (var channelId in MessageManager.Instance.JoinedChannelIds)
			{
				var messages = MessageManager.Instance.DispatchAllMessages(channelId);
				if (messages.Count == 0)
					continue;

				if (!m_CachedMessages.TryGetValue(channelId, out var cachedMessages))
				{
					cachedMessages = new List<Message>();
					m_CachedMessages.Add(channelId, cachedMessages);
				}

				cachedMessages.AddRange(messages);
			}
		}
	}
}