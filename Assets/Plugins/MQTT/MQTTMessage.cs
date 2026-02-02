using Crockhead.Core;
using Newtonsoft.Json;
using System;
using System.Text;


namespace Crockhead.Unity.MQTT
{
	/// <summary>
	/// MQTT 송신/수신 메시지.
	/// </summary>
	/// 
	[JsonObject(MemberSerialization.OptIn)]
	public class MQTTMessage : Disposable
	{
		/// <summary>
		/// 발행 식별자.
		/// </summary>
		[JsonProperty] public string PublishId { set; get; }

		/// <summary>
		/// 사용자 식별자.
		/// </summary>
		[JsonProperty] public string ClientId { set; get; }

		/// <summary>
		/// 발행 시간.
		/// </summary>
		[JsonProperty] public string Timestamp { set; get; }

		/// <summary>
		/// 발행 채널.
		/// </summary>
		[JsonProperty] public string Topic { set; get; }

		/// <summary>
		/// 발행 내용.
		/// </summary>
		[JsonProperty] public string Content { set; get; }

		/// <summary>
		/// 생성됨.
		/// </summary>
		public MQTTMessage() : base()
		{
			PublishId = string.Empty;
			ClientId = string.Empty;
			Timestamp = string.Empty;
			Topic = string.Empty;
			Content = string.Empty;
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
		}

		/// <summary>
		/// JSON 변환.
		/// </summary>
		public string ToJSON()
		{
			var json = JsonConvert.SerializeObject(this);
			return json;
		}

		/// <summary>
		/// 바이트 배열 변환.
		/// </summary>
		public byte[] ToBytes()
		{
			var json = ToJSON();
			var bytes = Encoding.UTF8.GetBytes(json);
			return bytes;
		}

		/// <summary>
		/// 발행 메시지 생성.
		/// </summary>
		public static MQTTMessage Create(string clientId, string topic, string text)
		{
			//var timestampFormat = "yyyy-MM-dd HH:mm:ss";
			var timestampFormat = "HH:mm";
			var message = new MQTTMessage
			{
				PublishId = Guid.NewGuid().ToString(),
				ClientId = clientId,
				Timestamp = DateTime.UtcNow.ToString(timestampFormat),
				Topic = topic,
				Content = text,
			};

			return message;
		}

		/// <summary>
		/// 발행 메시지 생성.
		/// </summary>
		public static MQTTMessage FromBytes(byte[] bytes)
		{
			if (bytes == null) throw new ArgumentNullException(nameof(bytes));

			var json = Encoding.UTF8.GetString(bytes);
			var message = FromJSON(json);
			return message;
		}

		/// <summary>
		/// 발행 메시지 생성.
		/// </summary>
		public static MQTTMessage FromJSON(string json)
		{
			if (string.IsNullOrWhiteSpace(json)) throw new ArgumentNullException(nameof(json));

			var message = JsonConvert.DeserializeObject<MQTTMessage>(json);
			return message;
		}
	}
}