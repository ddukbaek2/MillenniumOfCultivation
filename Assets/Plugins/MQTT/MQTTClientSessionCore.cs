using System;
using System.Threading.Tasks;
using UnityEngine;


namespace Crockhead.Unity.MQTT
{
	/// <summary>
	/// MQTT 클라이언트 세션 코어 기반 컴포넌트.
	/// </summary>
	public abstract class MQTTClientSessionCore : MonoBehaviour
	{
		/// <summary>
		/// 고유 식별자.
		/// </summary>
		private string m_CoreId;

		/// <summary>
		/// 클라이언트 세션.
		/// </summary>
		private MQTTClientSession m_ClientSession;

		/// <summary>
		/// 고유 식별자 프로퍼티.
		/// </summary>
		public string CoreId => m_CoreId;

		/// <summary>
		/// 연결 되었는지 여부 프로퍼티.
		/// </summary>
		public abstract bool IsConnected { get; }

		/// <summary>
		/// 클라이언트 세션 프로퍼티.
		/// </summary>
		public MQTTClientSession ClientSession { private set => SetClientSession(value); get => m_ClientSession; }

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected virtual void Awake()
		{
			m_ClientSession = null;
			m_CoreId = Guid.NewGuid().ToString();
			gameObject.name = m_CoreId;
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			GameObject.DontDestroyOnLoad(gameObject);
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		protected virtual void OnDestroy()
		{
			m_ClientSession = null;
		}

		/// <summary>
		/// 클라이언트 세션 설정.
		/// </summary>
		internal void SetClientSession(MQTTClientSession mqttClientSession)
		{
			m_ClientSession = mqttClientSession;
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

		/// <summary>
		/// 생성.
		/// </summary>
		public static T Create<T>() where T : MQTTClientSessionCore
		{
			var gameObject = new GameObject("MQTTClientSessionCore");
			var instance = gameObject.AddComponent<T>();
			return instance;
		}

		/// <summary>
		/// 해제.
		/// </summary>
		public static void SafeDestroy(ref MQTTClientSessionCore core)
		{
			if (core == null)
				return;

			GameObject.Destroy(core.gameObject);
			core = null;
		}
	}
}