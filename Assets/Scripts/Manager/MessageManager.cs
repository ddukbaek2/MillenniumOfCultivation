using Crockhead.Core;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using UnityEngine;
using System;
using System.Collections.Generic;



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
		public class Message
		{
			/// <summary>
			/// 채널 식별자.
			/// </summary>
			public string ChannelId;

			/// <summary>
			/// 클라이언트 식별자.
			/// </summary>
			public string ClientId;

			/// <summary>
			/// 값 식별자.
			/// </summary>
			public string Text;
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
		/// 메시지 큐.
		/// </summary>
		private Queue<string> m_MessageQueue;

		/// <summary>
		/// 연결 되었는지 여부 프로퍼티.
		/// </summary>
		public bool IsConnected => m_Client?.IsConnected ?? false;

		/// <summary>
		/// 진입된 채널 식별자 목록.
		/// </summary>
		public IEnumerable<string> JoinedChannelIds => m_JoinedChannelIds;

		/// <summary>
		/// 메시지 큐 프로퍼티.
		/// </summary>
		public Queue<string> MessageQueue => m_MessageQueue;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public MessageManager() : base()
		{
			if (Instance != this)
				return;

			m_Client = new MqttClient("https://ddukbaek2.com", 1883, false, null);
			m_Client.ConnectionClosed += OnConnectionClosed;
			m_Client.MqttMsgPublishReceived += OnReceivedMessage;
			m_Client.MqttMsgSubscribed += OnSubscribed;
			m_Client.MqttMsgUnsubscribed += OnUnsubscribed;
			m_Client.MqttMsgPublished += OnSendedMessage;

			m_ClientId = string.Empty;
			m_JoinedChannelIds = new HashSet<string>();
			m_MessageQueue = new Queue<string>();
		}

		protected override void OnDispose(bool explicitDisposing)
		{
			if (m_Client.IsConnected)
			{
				m_Client.Disconnect();
			}

			base.OnDispose(explicitDisposing);
		}

		/// <summary>
		/// 접속 해제됨.
		/// </summary>
		private void OnConnectionClosed(object sender, EventArgs eventArgs)
		{
			if (IsDisposed)
				return;

			//Reconnect();
		}

		/// <summary>
		/// 채널 구독됨.
		/// </summary>
		private void OnSubscribed(object sender, MqttMsgSubscribedEventArgs eventArgs)
		{
		}

		/// <summary>
		/// 채널 구독 해제됨.
		/// </summary>
		private void OnUnsubscribed(object sender, MqttMsgUnsubscribedEventArgs eventArgs)
		{
		}

		/// <summary>
		/// 메시지 수신됨.
		/// </summary>
		private void OnReceivedMessage(object sender, MqttMsgPublishEventArgs eventArgs)
		{
			var text = Encoding.UTF8.GetString(eventArgs.Message);
			m_MessageQueue.Enqueue(text);
			Debug.Log($"[MessageManager] OnReceivedMessage(): Message: {text}");

			//eventArgs.Topic
		}

		/// <summary>
		/// 메시지 송신됨.
		/// </summary>
		private void OnSendedMessage(object sender, MqttMsgPublishedEventArgs eventArgs)
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

		///// <summary>
		///// 서버 재접속.
		///// </summary>
		//public void Reconnect()
		//{
		//	if (string.IsNullOrWhiteSpace(m_ClientId))
		//		throw new Exception("Required Try Connect.");

		//	if (IsConnected)
		//	{
		//		m_Client.Disconnect();
		//	}

		//	Connect(m_ClientId);

		//	foreach (var channelId in m_JoinedChannelIds)
		//	{
		//		JoinChannel(channelId);
		//	}
		//}

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
				var message = Encoding.UTF8.GetBytes(text);
				var messageId = m_Client.Publish(channelId, message, MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
				//m_MessageQueue.Enqueue(text);
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				throw;
			}
		}

		/// <summary>
		/// 메시지 전체 꺼내기.
		/// </summary>
		public string[] DequeueAllMessages()
		{
			var snapshot = default(string[]);
			lock (m_MessageQueue)
			{
				snapshot = m_MessageQueue.ToArray();
				m_MessageQueue.Clear();
			}

			return snapshot;
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