using Newtonsoft.Json;
using System;


namespace MillenniumOfCultivation
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
}