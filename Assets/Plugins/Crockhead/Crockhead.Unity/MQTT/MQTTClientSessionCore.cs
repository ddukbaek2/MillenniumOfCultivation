using System.Threading.Tasks;


namespace Crockhead.Unity.MQTT
{
	/// <summary>
	/// MQTT 클라이언트 세션 코어 기반 컴포넌트.
	/// </summary>
	public abstract class MQTTClientSessionCore : ClientSessionCore
	{
		/// <summary>
		/// 연결 되었는지 여부 프로퍼티.
		/// </summary>
		public override bool IsConnected { get; }

		/// <summary>
		/// 클라이언트 세션 프로퍼티.
		/// </summary>
		public new MQTTClientSession ClientSession { private set => base.SetClientSession(value); get => (MQTTClientSession)base.ClientSession; }

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
			base.OnDestroy();
		}

		/// <summary>
		/// 접속됨.
		/// </summary>
		public virtual void MQTT_Connected(string args)
		{
			if (ClientSession != null)
				ClientSession.Connected?.Invoke();
		}

		/// <summary>
		/// 접속 해제됨.
		/// </summary>
		public virtual void MQTT_Disconnected(string args)
		{
			if (ClientSession != null)
				ClientSession.Disconnected?.Invoke();
		}

		/// <summary>
		/// 메시지 발행됨.
		/// </summary>
		public virtual void MQTT_Published(string json)
		{
			if (ClientSession != null)
			{
				try
				{
					var message = MQTTMessage.FromJSON(json);
					ClientSession.Published?.Invoke(message);
				}
				catch
				{
				}
			}
		}

		/// <summary>
		/// 메시지 수신됨.
		/// </summary>
		public virtual void MQTT_Received(string json)
		{
			if (ClientSession != null)
			{
				try
				{
					var message = MQTTMessage.FromJSON(json);
					ClientSession.Received?.Invoke(message);
				}
				catch
				{
				}
			}
		}

		/// <summary>
		/// 토픽 구독됨.
		/// </summary>
		public virtual void MQTT_Subscribed(string topic)
		{
			if (ClientSession != null)
				ClientSession.Subscribed?.Invoke(topic);
		}

		/// <summary>
		/// 토픽 구독 해제됨.
		/// </summary>
		public virtual void MQTT_Unsubscribed(string topic)
		{
			if (ClientSession != null)
				ClientSession.Unsubscribed?.Invoke(topic);
		}

		/// <summary>
		/// 브로커 접속.
		/// </summary>
		public abstract Task ConnectAsync(string url, string clientId);

		/// <summary>
		/// 브로커 접속 해제.
		/// </summary>
		public abstract Task DisconnectAsync();

		/// <summary>
		/// 메시지 발행.
		/// </summary>
		public abstract Task PublishAsync(string topic, string message, int qos, bool retain);

		/// <summary>
		/// 토픽 구독.
		/// </summary>
		public abstract Task SubscribeAsync(string topic, int qos);

		/// <summary>
		/// 토픽 구독 해제.
		/// </summary>
		public abstract Task UnsubscribeAsync(string topic);
	}
}