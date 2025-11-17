using Crockhead.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;



namespace MillenniumOfCultivation
{
	/// <summary>
	/// 메시지 매니저.
	/// </summary>
	public class MessageManager : SharedClass<MessageManager>
	{
		/// <summary>
		/// 메시지 정보.
		/// </summary>
		[JsonObject(MemberSerialization.OptIn)]
		public class Message
		{
			/// <summary>
			/// 채널 식별자 프로퍼티.
			/// </summary>
			[JsonProperty]
			public string ChannelId { set; get; }

			/// <summary>
			/// 클라이언트 식별자 프로퍼티.
			/// </summary>
			[JsonProperty]
			public string ClientId { set; get; }

			/// <summary>
			/// 송신 시간 프로퍼티.
			/// </summary>
			[JsonProperty]
			public DateTime DateTime { set; get; }

			/// <summary>
			/// 텍스트 프로퍼티.
			/// </summary>
			[JsonProperty]
			public string Text { set; get; }
		}


		/// <summary>
		/// MQTT 프로토콜 클라이언트.
		/// </summary>
		private MqttClient m_Client;

		/// <summary>
		/// 클라이언트 식별자.
		/// </summary>
		private string m_ClientId;

		/// <summary>
		/// 구독 중인 채널 목록.
		/// </summary>
		private HashSet<string> m_JoinedChannelIds;

		/// <summary>
		/// 수신 된 메시지 목록.
		/// </summary>
		private List<Message> m_ReceviedMessages;

		/// <summary>
		/// 연결 되었는지 여부 프로퍼티.
		/// </summary>
		public bool IsConnected => m_Client?.IsConnected ?? false;

		/// <summary>
		/// 진입된 채널 식별자 목록.
		/// </summary>
		public IEnumerable<string> JoinedChannelIds => m_JoinedChannelIds;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public MessageManager() : base()
		{
			if (Instance != this)
				return;

			m_Client = new MqttClient("ddukbaek2.com", 1883, false, null);
			m_Client.ConnectionClosed += OnDisconnected;
			m_Client.MqttMsgPublishReceived += OnReceived;
			//m_Client.MqttMsgSubscribed += OnSubscribed;
			//m_Client.MqttMsgUnsubscribed += OnUnsubscribed;
			m_Client.MqttMsgPublished += OnSended;

			m_ClientId = string.Empty;
			m_JoinedChannelIds = new HashSet<string>();
			m_ReceviedMessages = new List<Message>();
		}

		protected override void OnDispose(bool explicitDisposing)
		{
			if (IsConnected)
			{
				m_Client.Disconnect();
			}

			base.OnDispose(explicitDisposing);
		}

		/// <summary>
		/// 접속 해제됨.
		/// </summary>
		private void OnDisconnected(object sender, EventArgs eventArgs)
		{
			if (IsDisposed)
				return;

			Debug.Log($"[MessageManager] OnDisconnected()");
			//Reconnect();
		}

		///// <summary>
		///// 채널 구독됨.
		///// </summary>
		//private void OnSubscribed(object sender, MqttMsgSubscribedEventArgs eventArgs)
		//{
		//	Debug.Log($"[MessageManager] OnSubscribed()");
		//}

		///// <summary>
		///// 채널 구독 해제됨.
		///// </summary>
		//private void OnUnsubscribed(object sender, MqttMsgUnsubscribedEventArgs eventArgs)
		//{
		//	Debug.Log($"[MessageManager] OnUnsubscribed()");
		//}

		/// <summary>
		/// 메시지 수신됨.
		/// </summary>
		private void OnReceived(object sender, MqttMsgPublishEventArgs eventArgs)
		{
			var json = Encoding.UTF8.GetString(eventArgs.Message);
			Debug.Log($"[MessageManager] OnReceived(): Message: {json}");

			try
			{
				var message = JsonConvert.DeserializeObject<Message>(json);
				if (message == null)
					return;

				lock (m_ReceviedMessages)
				{
					m_ReceviedMessages.Add(message);
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				//throw;
			}
		}

		/// <summary>
		/// 메시지 송신됨.
		/// </summary>
		private void OnSended(object sender, MqttMsgPublishedEventArgs eventArgs)
		{
			// 성공.
			if (eventArgs.IsPublished)
			{
			}
			// 실패.
			else
			{
				Debug.Log("[MessageManager] SendMessage() Failure.");
			}
		}

		/// <summary>
		/// 서버 접속.
		/// </summary>
		public void Connect(string clientId)
		{
			if (IsConnected)
				return;

			m_ClientId = clientId;

			try
			{
				var result = m_Client.Connect(m_ClientId);
				switch (result)
				{
					case 1: throw new Exception("Protocol Mismatch");
					case 2: throw new Exception("Reject ClientId.");
					case 3: throw new Exception("Broker Failure.");
					case 4: throw new Exception("Id/Password Incorrect.");
					case 5: throw new Exception("Authorize Failure.");
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				throw;
			}
		}

		/// <summary>
		/// 서버 재접속.
		/// </summary>
		public void Reconnect()
		{
			try
			{
				if (string.IsNullOrWhiteSpace(m_ClientId))
					throw new Exception("Required Try Connect.");

				if (IsConnected)
				{
					m_Client.Disconnect();
				}

				Connect(m_ClientId);

				if (IsConnected)
				{
					foreach (var channelId in m_JoinedChannelIds)
					{
						JoinChannel(channelId);
					}
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				throw;
			}
		}

		/// <summary>
		/// 채널 진입.
		/// </summary>
		public void JoinChannel(string channelId)
		{
			if (!IsConnected)
				return;
			if (IsJoinedChannel(channelId))
				return;

			try
			{
				var messageId = m_Client.Subscribe(new string[] { channelId }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
				m_JoinedChannelIds.Add(channelId);
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				throw;
			}
		}

		/// <summary>
		/// 채널 나가기.
		/// </summary>
		public void LeaveChannel(string channelId)
		{
			if (!IsConnected)
				return;
			if (!IsJoinedChannel(channelId))
				return;

			try
			{
				var messageId = m_Client.Unsubscribe(new string[] { channelId });
				m_JoinedChannelIds.Remove(channelId);
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				throw;
			}
		}

		/// <summary>
		/// 송신.
		/// </summary>
		public void SendMessage(string channelId, string text)
		{
			if (!IsConnected)
				return;

			try
			{
				var json = JsonConvert.SerializeObject(new Message
				{
					ChannelId = channelId,
					ClientId = m_ClientId,
					DateTime = DateTime.UtcNow,
					Text = text
				});

				var message = Encoding.UTF8.GetBytes(json);
				var messageId = m_Client.Publish(channelId, message, MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				throw;
			}
		}

		/// <summary>
		/// 지정 채널의 모든 메시지 꺼내기.
		/// </summary>
		public List<Message> DispatchAllMessages(string channelId)
		{
			var messages = new List<Message>();
			lock (m_ReceviedMessages)
			{
				for (var i = 0; i < m_ReceviedMessages.Count; ++i)
				{
					var message = m_ReceviedMessages[i];
					if (message.ChannelId != channelId)
						continue;

					messages.Add(message);
					m_ReceviedMessages.RemoveAt(i);
					--i;
				}
			}

			return messages;
		}

		/// <summary>
		/// 채널이 진입 되어있는지 여부.
		/// </summary>
		public bool IsJoinedChannel(string channelId)
		{
			var contains = m_JoinedChannelIds.Contains(channelId);
			return contains;
		}
	}
}