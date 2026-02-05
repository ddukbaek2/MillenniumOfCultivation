using System;
using System.Threading;
using System.Threading.Tasks;


namespace Crockhead.Unity.WebSockets
{
	/// <summary>
	/// 웹소켓 클라이언트 세션 코어 기반 컴포넌트.
	/// </summary>
	public abstract class WebSocketClientSessionCore : ClientSessionCore
	{
		/// <summary>
		/// 클라이언트 세션 프로퍼티.
		/// </summary>
		public new WebSocketClientSession ClientSession { private set => base.SetClientSession(value); get => (WebSocketClientSession)base.ClientSession; }

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
		/// 연결.
		/// </summary>
		public abstract Task ConnectAsync(Uri uri, CancellationToken cancellationToken = default);

		/// <summary>
		/// 연결 해제.
		/// </summary>
		public abstract Task DisconnectAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// 송신.
		/// </summary>
		public abstract Task SendAsync(string text, CancellationToken cancellationToken = default);
	}
}