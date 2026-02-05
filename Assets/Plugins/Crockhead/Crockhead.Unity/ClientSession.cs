using Crockhead.Core;
using System;


namespace Crockhead.Unity
{
	/// <summary>
	/// 웹소켓 클라이언트.
	/// </summary>
	public abstract class ClientSession : Disposable
	{
		/// <summary>
		/// 클라이언트 세션 코어.
		/// </summary>
		private ClientSessionCore m_ClientSessionCore;

		/// <summary>
		/// 접속 식별자.
		/// </summary>
		private string m_ClientId;

		/// <summary>
		/// 연결 되었는지 여부 프로퍼티.
		/// </summary>
		public abstract bool IsConnected { get; }

		/// <summary>
		/// 접속 식별자 프로퍼티.
		/// </summary>
		public string ClientId { protected set => SetClientId(value); get => m_ClientId; }

		/// <summary>
		/// 클라이언트 세션 코어 프로퍼티.
		/// </summary>
		public ClientSessionCore ClientSessionCore { protected set => SetClientSessionCore(value); get => m_ClientSessionCore; }

		/// <summary>
		/// 접속됨 이벤트 프로퍼티.
		/// </summary>
		public Action Connected { set; get; }

		/// <summary>
		/// 접속 해제됨 이벤트 프로퍼티.
		/// </summary>
		public Action Disconnected { set; get; }

		/// <summary>
		/// 생성됨.
		/// </summary>
		public ClientSession() : base()
		{
			m_ClientId = string.Empty;
			m_ClientSessionCore = null;
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
			m_ClientSessionCore = null;
		}

		/// <summary>
		/// 클라이언트 세션 코어 설정.
		/// </summary>
		protected void SetClientSessionCore(ClientSessionCore clientSessionCore)
		{
			m_ClientSessionCore = clientSessionCore;
			m_ClientSessionCore.SetClientSession(this);
		}

		/// <summary>
		/// 클라이언트 식별자 설정.
		/// </summary>
		protected void SetClientId(string clientId)
		{
			m_ClientId = clientId;
		}
	}
}