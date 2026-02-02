using Crockhead.Core;
using System;
using System.Threading.Tasks;


namespace Crockhead.Unity.MQTT
{
	/// <summary>
	/// MQTT 클라이언트 세션.
	/// </summary>
	public class MQTTClientSession : Disposable
	{
		/// <summary>
		/// 코어.
		/// </summary>
		private MQTTClientSessionCore m_ClientSessionCore;

		/// <summary>
		/// 접속 식별자.
		/// </summary>
		private string m_ClientId;

		/// <summary>
		/// 연결 되었는지 여부 프로퍼티.
		/// </summary>
		public bool IsConnected => m_ClientSessionCore.IsConnected;

		/// <summary>
		/// 접속 식별자 프로퍼티.
		/// </summary>
		public string ClientId => m_ClientId;

		/// <summary>
		/// 접속됨 이벤트 프로퍼티.
		/// </summary>
		public Action Connected { set; get; }

		/// <summary>
		/// 접속 해제됨 이벤트 프로퍼티.
		/// </summary>
		public Action Disconnected { set; get; }

		/// <summary>
		/// 메시지 발행됨 이벤트 프로퍼티. (토픽, 메시지)
		/// </summary>
		public Action<MQTTMessage> Published { set; get; }

		/// <summary>
		/// 메시지 수신됨 이벤트 프로퍼티. (토픽, 메시지)
		/// </summary>
		public Action<MQTTMessage> Received { set; get; }

		/// <summary>
		/// 토픽 구독됨 이벤트 프로퍼티. (토픽)
		/// </summary>
		public Action<string> Subscribed { set; get; }

		/// <summary>
		/// 토픽 구독취소됨 이벤트 프로퍼티. (토픽)
		/// </summary>
		public Action<string> Unsubscribed { set; get; }

		/// <summary>
		/// 생성됨. (자동으로 코어 선택)
		/// </summary>
		public MQTTClientSession() : base()
		{
			m_ClientId = string.Empty;
#if UNITY_WEBGL
			m_ClientSessionCore = MQTTClientSessionCore.Create<MQTTClientSessionWebGLCore>();
#else
			m_ClientSessionCore = MQTTClientSessionCore.Create<MQTTClientSessionStandardCore>();
#endif
			m_ClientSessionCore.SetClientSession(this);
		}

		/// <summary>
		/// 생성됨.
		/// </summary>
		public MQTTClientSession(MQTTClientSessionCore mqttClientCore) : base()
		{
			m_ClientId = string.Empty;
			m_ClientSessionCore = mqttClientCore;
			m_ClientSessionCore.SetClientSession(this);
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
			MQTTClientSessionCore.SafeDestroy(ref m_ClientSessionCore);
		}

		/// <summary>
		/// 브로커 접속.
		/// </summary>
		public async Task ConnectAsync(string url, string clientId)
		{
			m_ClientId = clientId;
			await m_ClientSessionCore.ConnectAsync(url, clientId);
		}

		/// <summary>
		/// 브로커 접속 해제.
		/// </summary>
		public async Task DisconnectAsync()
		{
			await m_ClientSessionCore.DisconnectAsync();
		}

		/// <summary>
		/// 메시지 발행.
		/// </summary>
		public async Task PublishAsync(string topic, string message)
		{
			await m_ClientSessionCore.PublishAsync(topic, message, 0, false);
		}

		/// <summary>
		/// 토픽 구독.
		/// </summary>
		public async Task SubscribeAsync(string topic)
		{
			await m_ClientSessionCore.SubscribeAsync(topic, 0);
		}

		/// <summary>
		/// 토픽 구독 해제.
		/// </summary>
		public async Task UnsubscribeAsync(string topic)
		{
			await m_ClientSessionCore.UnsubscribeAsync(topic);
		}
	}
}