using System;
using System.Threading.Tasks;
using UnityEngine;


namespace Crockhead.Unity.MQTT
{
	/// <summary>
	/// MQTT 클라이언트 세션.
	/// </summary>
	public class MQTTClientSession : ClientSession
	{
		/// <summary>
		/// 연결 되었는지 여부 프로퍼티.
		/// </summary>
		public override bool IsConnected => ClientSessionCore.IsConnected;

		/// <summary>
		/// 클라이언트 세션 코어 프로퍼티.
		/// </summary>
		public new MQTTClientSessionCore ClientSessionCore { private set => base.SetClientSessionCore(value); get => (MQTTClientSessionCore)base.ClientSessionCore; }

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
#if UNITY_WEBGL
			ClientSessionCore = MQTTClientSessionCore.Create<MQTTClientSessionWebGLCore>();
#else
			ClientSessionCore = MQTTClientSessionCore.Create<MQTTClientSessionStandardCore>();
#endif
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
			if (ClientSessionCore != null)
			{
				GameObject.Destroy(ClientSessionCore.gameObject);
				ClientSessionCore = null;
			}
		}

		/// <summary>
		/// 브로커 접속.
		/// </summary>
		public async Task ConnectAsync(string url, string clientId)
		{
			SetClientId(clientId);
			await ClientSessionCore.ConnectAsync(url, clientId);
		}

		/// <summary>
		/// 브로커 접속 해제.
		/// </summary>
		public async Task DisconnectAsync()
		{
			await ClientSessionCore.DisconnectAsync();
		}

		/// <summary>
		/// 메시지 발행.
		/// </summary>
		public async Task PublishAsync(string topic, string message)
		{
			await ClientSessionCore.PublishAsync(topic, message, 0, false);
		}

		/// <summary>
		/// 토픽 구독.
		/// </summary>
		public async Task SubscribeAsync(string topic)
		{
			await ClientSessionCore.SubscribeAsync(topic, 0);
		}

		/// <summary>
		/// 토픽 구독 해제.
		/// </summary>
		public async Task UnsubscribeAsync(string topic)
		{
			await ClientSessionCore.UnsubscribeAsync(topic);
		}
	}
}