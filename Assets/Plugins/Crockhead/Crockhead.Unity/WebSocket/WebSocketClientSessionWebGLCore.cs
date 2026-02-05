#if UNITY_WEBGL
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;


namespace Crockhead.Unity.WebSockets
{
	/// <summary>
	/// 클라이언트.
	/// </summary>
	public class WebSocketClientSessionWebGLCore : WebSocketClientSessionCore
	{
		#region JS_EXTERNAL_FUNCTION
		[DllImport("__Internal")] private static extern int WS_IsConnected(string coreId);
		[DllImport("__Internal")] private static extern void WS_Connect(string coreId, string url);
		[DllImport("__Internal")] private static extern void WS_Disconnect(string coreId);
		[DllImport("__Internal")] private static extern void WS_Send(string coreId, string text);
		#endregion

		/// <summary>
		/// 연결 되었는지 여부 프로퍼티.
		/// </summary>
		public override bool IsConnected => WS_IsConnected(CoreId) == 1;

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
		/// 접속.
		/// </summary>
		public override Task ConnectAsync(Uri uri, CancellationToken cancellationToken = default)
		{
			var url = uri.ToString();
			WS_Connect(url, UnityRuntime.Instance.gameObject.name);
			return Task.CompletedTask;
		}

		public override Task DisconnectAsync(CancellationToken cancellationToken = default)
		{
		}

		public override Task SendAsync(string text, CancellationToken cancellationToken = default)
		{
		}
	}
}
#endif