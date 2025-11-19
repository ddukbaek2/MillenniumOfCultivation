using Crockhead.Unity;
using Crockhead.Unity.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MillenniumOfCultivation.UI
{
	/// <summary>
	/// 메시지 뷰.
	/// </summary>
	[AssetPath("Assets/Resources/UI/UIMessageView.prefab", AssetPathType.Resources)]
	public class UIMessageView : UIPanelView
	{
		#region INSPECTOR
		[SerializeField] private RectTransform m_ContentRectTransform;
		[SerializeField] private UIInputView m_InputField;
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

			m_CachedMessages = new Dictionary<string, List<Message>>();
			if (m_ContentRectTransform == null)
			{
				m_ContentRectTransform = GetOrAddComponent<RectTransform>("ScrollView/Viewport/Content");
			}

			if (m_InputField == null)
			{
				m_InputField = GetOrAddComponent<UIInputView>("InputField");
			}

			if (!Application.isPlaying)
				return;

			m_InputField.onSelect.AddListener(OnSelect);
			m_InputField.onDeselect.AddListener(OnDeselect);
			//m_InputField.onEndEdit
			//m_InputField.onEndTextSelection
			//m_InputField.onFocusSelectAll
			//m_InputField.onTextSelection
			//m_InputField.onTouchScreenKeyboardStatusChanged
			//m_InputField.onValidateInput
			//m_InputField.onValueChanged
			m_InputField.onSubmit.AddListener(OnSubmit);
			
			MessageManager.Instance.Connect("ddukbaek2");
			MessageManager.Instance.JoinChannel("@ddukbaek2");
		}

		/// <summary>
		/// 초기화됨.
		/// </summary>
		protected override void Start()
		{
			base.Start();

			if (!Application.isPlaying)
				return;

			// 선택.
			CreateMessageItemView("Initialize Chatting...");
			m_InputField.Select();
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		protected override void OnDestroy()
		{
			base.OnDestroy();
		}

		/// <summary>
		/// 입력 뷰 선택됨.
		/// </summary>
		protected virtual void OnSelect(string text)
		{
			Debug.Log("[UIMessageView] OnSelect()");
		}

		/// <summary>
		/// 입력 뷰 선택 해제됨.
		/// </summary>
		protected virtual void OnDeselect(string text)
		{
			Debug.Log("[UIMessageView] OnDeselect()");
		}

		/// <summary>
		/// 입력 뷰의 내용 송신.
		/// </summary>
		protected virtual void OnSubmit(string text)
		{
			Debug.Log("[UIMessageView] OnSubmit()");

			MessageManager.Instance.SendMessage("@ddukbaek2", text);
			m_InputField.text = string.Empty;
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

				// 메시지 아이템 생성.
				foreach (var message in messages)
				{
					CreateMessageItemView(message);
				}
			}
		}

		/// <summary>
		/// 아이템 생성.
		/// </summary>
		private void CreateMessageItemView(Message message)
		{
			var item = UIView.CreateNodeFromAsset<UIMessageItemView>(m_ContentRectTransform);
			item.SetMessage(message);

			CoroutineHelper.WaitForNextFrame(() =>
			{
				var scrollview = GetOrAddComponent<ScrollRect>("ScrollView");
				scrollview.verticalNormalizedPosition = 0f;
			});
		}

		/// <summary>
		/// 아이템 생성.
		/// </summary>
		private void CreateMessageItemView(string text)
		{
			var message = new Message
			{
				DateTime = DateTime.UtcNow,
				ChannelId = "@ddukbaek2",
				ClientId = "ddukbaek2",
				Text = text,
			};

			CreateMessageItemView(message);
		}
	}
}