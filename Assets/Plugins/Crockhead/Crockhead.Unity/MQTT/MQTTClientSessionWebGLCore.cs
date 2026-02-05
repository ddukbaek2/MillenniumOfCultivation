#if UNITY_WEBGL
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;


namespace Crockhead.Unity.MQTT
{
	/// <summary>
	/// 웹GL용 브릿지 컴포넌트.
	/// </summary>
	public class MQTTClientSessionWebGLCore : MQTTClientSessionCore
	{
		#region JS_EXTERNAL_FUNCTION
		[DllImport("__Internal")] private static extern int MQTT_IsConnected(string coreId);
		[DllImport("__Internal")] private static extern void MQTT_Connect(string coreId, string url, string clientId);
		[DllImport("__Internal")] private static extern void MQTT_Disconnect(string coreId);
		[DllImport("__Internal")] private static extern void MQTT_Subscribe(string coreId, string topic, int qos);
		[DllImport("__Internal")] private static extern void MQTT_Unsubscribe(string coreId, string topic);
		[DllImport("__Internal")] private static extern void MQTT_Publish(string coreId, string topic, string json, int qos, int retain);
		#endregion

		/// <summary>
		/// 연결 되었는지 여부 프로퍼티.
		/// </summary>
		public override bool IsConnected => MQTT_IsConnected(CoreId) == 1;

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
		/// 브로커 접속.
		/// </summary>
		public override Task ConnectAsync(string url, string clientId)
		{
			if (IsConnected) throw new InvalidOperationException("[MQTTClientSessionWebGLCore] Aleady Connected.");

			MQTT_Connect(CoreId, url, clientId);
			return Task.CompletedTask;
		}

		/// <summary>
		/// 브로커 접속 해제.
		/// </summary>
		public override Task DisconnectAsync()
		{
			if (!IsConnected) throw new InvalidOperationException("[MQTTClientSessionWebGLCore] Not Connected.");

			MQTT_Disconnect(CoreId);
			return Task.CompletedTask;
		}

		/// <summary>
		/// 메시지 발행.
		/// </summary>
		public override Task PublishAsync(string topic, string text, int qos, bool retain)
		{
			if (!IsConnected) throw new InvalidOperationException("[MQTTClientSessionWebGLCore] Not Connected.");

			var message = MQTTMessage.Create(ClientSession.ClientId, topic, text);
			var json = message.ToJSON();
			MQTT_Publish(CoreId, topic, json, qos, retain ? 1 : 0);
			return Task.CompletedTask;
		}

		/// <summary>
		/// 토픽 구독.
		/// </summary>
		public override Task SubscribeAsync(string topic, int qos)
		{
			if (!IsConnected) throw new InvalidOperationException("[MQTTClientSessionWebGLCore] Not Connected.");

			MQTT_Subscribe(CoreId, topic, qos);
			return Task.CompletedTask;
		}

		/// <summary>
		/// 토픽 구독 해제.
		/// </summary>
		public override Task UnsubscribeAsync(string topic)
		{
			if (!IsConnected) throw new InvalidOperationException("[MQTTClientSessionWebGLCore] Not Connected.");

			MQTT_Unsubscribe(CoreId, topic);
			return Task.CompletedTask;
		}
	}
}
#endif