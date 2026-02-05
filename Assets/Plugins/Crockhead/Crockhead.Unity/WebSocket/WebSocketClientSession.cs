using Crockhead.Unity.MQTT;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace Crockhead.Unity.WebSockets
{
	/// <summary>
	/// 웹소켓 클라이언트.
	/// </summary>
	public class WebSocketClientSession : ClientSession
	{
		/// <summary>
		/// 연결 되었는지 여부 프로퍼티.
		/// </summary>
		public override bool IsConnected { get; }

		/// <summary>
		/// 클라이언트 세션 코어 프로퍼티.
		/// </summary>
		public new WebSocketClientSessionCore ClientSessionCore { private set => base.SetClientSessionCore(value); get => (WebSocketClientSessionCore)base.ClientSessionCore; }

		/// <summary>
		/// 수신됨 이벤트 프로퍼티.
		/// </summary>
		public Action<byte[]> Received { set; get; }

		/// <summary>
		/// 생성됨. (자동으로 코어 선택)
		/// </summary>
		public WebSocketClientSession() : base()
		{
#if UNITY_WEBGL
			ClientSessionCore = WebSocketClientSessionCore.Create<WebSocketClientSessionWebGLCore>();
#else
			ClientSessionCore = WebSocketClientSessionCore.Create<WebSocketClientSessionStandardCore>();
#endif
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
		}

		/// <summary>
		/// 연결.
		/// </summary>
		public async Task ConnectAsync(Uri uri)
		{
			await ClientSessionCore.ConnectAsync(uri);
		}

		/// <summary>
		/// 연결 해제.
		/// </summary>
		public async Task DisconnectAsync()
		{
			await ClientSessionCore.DisconnectAsync();
		}

		/// <summary>
		/// 송신.
		/// </summary>
		public async Task SendAsync(string text)
		{
			await ClientSessionCore.SendAsync(text);
		}
	}
}