using System.Net.WebSockets;

namespace Cnblogs.DashScope.Core.Internals;

internal sealed class ClientWebSocketWrapper(ClientWebSocket socket) : IClientWebSocket
{
    /// <inheritdoc />
    public void Dispose()
    {
        socket.Dispose();
    }

    /// <inheritdoc />
    public ClientWebSocketOptions Options => socket.Options;

    /// <inheritdoc />
    public WebSocketCloseStatus? CloseStatus => socket.CloseStatus;

    /// <inheritdoc />
    public Task ConnectAsync(Uri uri, CancellationToken cancellation) => socket.ConnectAsync(uri, cancellation);

    /// <inheritdoc />
    public Task SendAsync(
        ArraySegment<byte> buffer,
        WebSocketMessageType messageType,
        bool endOfMessage,
        CancellationToken cancellationToken)
        => socket.SendAsync(buffer, messageType, endOfMessage, cancellationToken);

    /// <inheritdoc />
    public Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
        => socket.ReceiveAsync(buffer, cancellationToken);

    /// <inheritdoc />
    public Task CloseAsync(
        WebSocketCloseStatus closeStatus,
        string? statusDescription,
        CancellationToken cancellationToken)
        => socket.CloseAsync(closeStatus, statusDescription, cancellationToken);
}
