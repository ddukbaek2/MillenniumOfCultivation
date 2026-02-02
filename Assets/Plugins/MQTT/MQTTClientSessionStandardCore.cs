#if !UNITY_WEBGL
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


namespace Crockhead.Unity.MQTT
{
	/// <summary>
	/// MQTT 클라이언트 세션 코어. (범용 코어 - MQTTnet 라이브러리 의존)
	/// </summary>
	public class MQTTClientSessionStandardCore : MQTTClientSessionCore
	{
		/// <summary>
		/// 내부에서 사용되는 MQTTnet 라이브러리의 MQTT 클라이언트.
		/// </summary>
		private IMqttClient m_Client;

		/// <summary>
		/// 연결 되었는지 여부 프로퍼티.
		/// </summary>
		public override bool IsConnected => m_Client != null && m_Client.IsConnected;

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		protected override void OnDestroy()
		{
			if (m_Client != null)
			{
				m_Client.Dispose();
				m_Client = null;
			}

			base.OnDestroy();
		}

		/// <summary>
		/// 접속됨.
		/// </summary>
		protected virtual Task OnConnectedAsync(MqttClientConnectedEventArgs eventArgs)
		{
			MQTT_Connected(string.Empty);

			return Task.CompletedTask;
		}

		/// <summary>
		/// 접속 해제됨.
		/// </summary>
		protected virtual Task OnDisconnectedAsync(MqttClientDisconnectedEventArgs eventArgs)
		{
			MQTT_Disconnected(string.Empty);

			return Task.CompletedTask;
		}

		/// <summary>
		/// 메시지 수신됨.
		/// </summary>
		protected virtual Task OnApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
		{
			var topic = eventArgs.ApplicationMessage.Topic ?? string.Empty;
			var payloadSegment = eventArgs.ApplicationMessage.PayloadSegment;
			var bytes = payloadSegment.Array == null ? Array.Empty<byte>() : payloadSegment.ToArray();

			if (ClientSession != null)
			{
				var json = Encoding.UTF8.GetString(bytes);
				MQTT_Received(json);
			}

			return Task.CompletedTask;
		}

		/// <summary>
		/// 브로커 접속.
		/// </summary>
		public override async Task ConnectAsync(string url, string clientId)
		{
			if (IsConnected) throw new InvalidOperationException("[MQTTClientSessionStandardCore] Aleady Connected.");

			var factory = new MqttFactory();
			m_Client = factory.CreateMqttClient();
			m_Client.ConnectedAsync += OnConnectedAsync;
			m_Client.ApplicationMessageReceivedAsync += OnApplicationMessageReceivedAsync;
			m_Client.DisconnectedAsync += OnDisconnectedAsync;

			var builder = new MqttClientOptionsBuilder();
			builder.WithClientId(clientId);
			//builder.WithCredentials(username, password);
			builder.WithCleanSession();
			builder.WithWebSocketServer((builder) => builder.WithUri(url));
			var mqttClientOptions = builder.Build();

			var mqttClientConnectResult = await m_Client.ConnectAsync(mqttClientOptions, CancellationToken.None);
			//mqttClientConnectResult
		}

		/// <summary>
		/// 브로커 접속 해제.
		/// </summary>
		public override async Task DisconnectAsync()
		{
			if (!IsConnected) throw new InvalidOperationException("[MQTTClientSessionStandardCore] Not Connected.");

			var mqttClientDisconnectOptions = new MqttClientDisconnectOptions();
			mqttClientDisconnectOptions.Reason = MqttClientDisconnectOptionsReason.NormalDisconnection;
			await m_Client.DisconnectAsync(mqttClientDisconnectOptions, CancellationToken.None);

			m_Client.Dispose();
			m_Client = null;
		}

		/// <summary>
		/// 메시지 발행.
		/// </summary>
		public override async Task PublishAsync(string topic, string text, int qos, bool retain)
		{
			if (!IsConnected) throw new InvalidOperationException("[MQTTClientSessionStandardCore] Not Connected.");

			var builder = new MqttApplicationMessageBuilder();
			builder.WithTopic(topic);
			builder.WithPayload(text);
			builder.WithQualityOfServiceLevel((MqttQualityOfServiceLevel)qos);
			builder.WithRetainFlag(retain);
			var mqttApplicationMessage = builder.Build();

			try
			{
				var mqttClientPublishResult = await m_Client.PublishAsync(mqttApplicationMessage, CancellationToken.None);
				//mqttClientPublishResult

				var message = MQTTMessage.Create(ClientSession.ClientId, topic, text);
				var json = message.ToJSON();
				MQTT_Published(json);
			}
			catch
			{
				// 실패.
			}
		}

		/// <summary>
		/// 토픽 구독.
		/// </summary>
		public override async Task SubscribeAsync(string topic, int qos)
		{
			if (!IsConnected) throw new InvalidOperationException("[MQTTClientSessionStandardCore] Not Connected.");

			var builder = new MqttTopicFilterBuilder();
			builder.WithTopic(topic);
			builder.WithQualityOfServiceLevel((MqttQualityOfServiceLevel)qos);
			var mqttTopicFilter = builder.Build();

			try
			{
				var mqttClientSubscribeResult = await m_Client.SubscribeAsync(mqttTopicFilter);
				//mqttClientSubscribeResult
				MQTT_Subscribed(topic);
			}
			catch
			{
				// 실패.
			}
		}

		/// <summary>
		/// 토픽 구독 해제.
		/// </summary>
		public override async Task UnsubscribeAsync(string topic)
		{
			if (!IsConnected) throw new InvalidOperationException("[MQTTClientSessionStandardCore] Not Connected.");

			try
			{
				var mqttClientUnsubscribeResult = await m_Client.UnsubscribeAsync(topic);
				//mqttClientUnsubscribeResult
				MQTT_Unsubscribed(topic);
			}
			catch
			{
				// 실패.
			}
		}
	}
}
#endif