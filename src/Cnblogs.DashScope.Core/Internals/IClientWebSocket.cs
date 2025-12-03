using System.Net.WebSockets;

namespace Cnblogs.DashScope.Core.Internals
{
    /// <summary>
    /// Extract <see cref="ClientWebSocket"/> for testing purpose.
    /// </summary>
    internal interface IClientWebSocket : IDisposable
    {
        public ClientWebSocketOptions Options { get; }

        public WebSocketCloseStatus? CloseStatus { get; }

        public Task ConnectAsync(Uri uri, CancellationToken cancellation);

        public Task SendAsync(
            ArraySegment<byte> buffer,
            WebSocketMessageType messageType,
            bool endOfMessage,
            CancellationToken cancellationToken);

        Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken);

        Task CloseAsync(WebSocketCloseStatus closeStatus, string? statusDescription, CancellationToken cancellationToken);
    }
}
