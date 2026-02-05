using UnityEngine;


namespace Crockhead.Unity
{
	/// <summary>
	/// 네트워크 클라이언트 코어 컴포넌트.
	/// </summary>
	public abstract class ClientSessionCore : NetworkBehaviour
	{
		/// <summary>
		/// 클라이언트 세션.
		/// </summary>
		private ClientSession m_ClientSession;

		/// <summary>
		/// 고유 식별자 프로퍼티.
		/// </summary>
		public string CoreId => ObjectId;

		/// <summary>
		/// 연결 되었는지 여부 프로퍼티.
		/// </summary>
		public override bool IsConnected => false;

		/// <summary>
		/// 클라이언트 세션 프로퍼티.
		/// </summary>
		public ClientSession ClientSession { private set => SetClientSession(value); get => m_ClientSession; }

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			m_ClientSession = null;
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		protected override void OnDestroy()
		{
			m_ClientSession = null;

			base.OnDestroy();
		}

		/// <summary>
		/// 클라이언트 세션 설정.
		/// </summary>
		internal void SetClientSession(ClientSession clientSession)
		{
			m_ClientSession = clientSession;
		}

		/// <summary>
		/// 생성.
		/// </summary>
		public static TClientSessionCore Create<TClientSessionCore>() where TClientSessionCore : ClientSessionCore
		{
			var gameObject = new GameObject("ClientSessionCore");
			var instance = gameObject.AddComponent<TClientSessionCore>();
			return instance;
		}

		/// <summary>
		/// 해제.
		/// </summary>
		public static void SafeDestroy<TClientSessionCore>(ref TClientSessionCore clientSessionCore) where TClientSessionCore : ClientSessionCore
		{
			if (clientSessionCore == null)
				return;

			GameObject.Destroy(clientSessionCore.gameObject);
			clientSessionCore = null;
		}
	}
}