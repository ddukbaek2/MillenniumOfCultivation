#if !UNITY_WEBGL
using System;
using System.Buffers;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Crockhead.Unity.WebSockets
{
	/// <summary>
	/// 클라이언트.
	/// </summary>
	public class WebSocketClientSessionStandardCore : WebSocketClientSessionCore
	{
		/// <summary>
		/// 닷넷 웹소켓 클라이언트.
		/// </summary>
		private ClientWebSocket m_Client;

		/// <summary>
		/// 송신 태스크 취소 처리기.
		/// </summary>
		private CancellationTokenSource m_ReceiveCancellationTokenSource;

		/// <summary>
		/// 송신용 태스크.
		/// </summary>
		private Task m_ReceiveTask;

		/// <summary>
		/// 연결 되었는지 여부 프로퍼티.
		/// </summary>
		public override bool IsConnected => m_Client != null && m_Client.State == WebSocketState.Open;

		/// <summary>
		/// 생성됨.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			m_Client = new ClientWebSocket();
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		protected override void OnDestroy()
		{
			if (m_Client != null)
			{
				m_Client.Dispose();
				m_Client = null;
			}

			base.OnDestroy();
		}

		/// <summary>
		/// 연결.
		/// </summary>
		public override async Task ConnectAsync(Uri uri, CancellationToken cancellationToken = default)
		{
			if (IsConnected)
				throw new InvalidOperationException();

			try
			{
				await m_Client.ConnectAsync(uri, cancellationToken).ConfigureAwait(false);
			}
			catch (Exception)
			{
				throw;
			}

			m_ReceiveCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
			m_ReceiveTask = Task.Run(() => OnReceiveAsync(m_ReceiveCancellationTokenSource.Token));
			ClientSession.Connected?.Invoke();
		}

		/// <summary>
		/// 연결 해제.
		/// </summary>
		public override async Task DisconnectAsync(CancellationToken cancellationToken = default)
		{
			if (!IsConnected)
				return;

			try
			{
				m_ReceiveCancellationTokenSource.Cancel();

				if (m_Client.State == WebSocketState.Open ||
					m_Client.State == WebSocketState.CloseReceived)
					await m_Client.CloseAsync(WebSocketCloseStatus.NormalClosure, "bye", cancellationToken).ConfigureAwait(false);
			}
			catch (Exception)
			{
				throw;
			}

			ClientSession.Disconnected?.Invoke();
		}

		/// <summary>
		/// 송신.
		/// </summary>
		public override async Task SendAsync(string text, CancellationToken cancellationToken = default)
		{
			if (!IsConnected)
				throw new InvalidOperationException("WebSocket is not connected.");

			var bytes = Encoding.UTF8.GetBytes(text);
			await m_Client.SendAsync(bytes, WebSocketMessageType.Text, true, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// 수신.
		/// </summary>
		protected async Task OnReceiveAsync(CancellationToken cancellationToken = default)
		{
			var buffer = ArrayPool<byte>.Shared.Rent(64 * 1024);

			try
			{
				while (!cancellationToken.IsCancellationRequested && m_Client.State == WebSocketState.Open)
				{
					using var memoryStream = new MemoryStream();
					var finished = false;
					var value = string.Empty;

					do
					{
						var result = await m_Client.ReceiveAsync(buffer, cancellationToken).ConfigureAwait(false);

						if (result.MessageType == WebSocketMessageType.Close)
							return;

						if (result.Count > 0)
							memoryStream.Write(buffer, 0, result.Count);
						if (memoryStream.Length > 256 * 1024)
							throw new InvalidOperationException("Incoming message too large.");

						if (result.EndOfMessage)
						{
							finished = true;

							if (result.MessageType == WebSocketMessageType.Text)
							{
								value = Encoding.UTF8.GetString(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
							}
						}
					}
					while (!finished);
				}
			}
			catch (OperationCanceledException)
			{

			}
			catch (Exception)
			{
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(buffer);

				try
				{
					await DisconnectAsync(CancellationToken.None).ConfigureAwait(false);
				}
				catch
				{
				}
			}
		}
	}
}
#endif