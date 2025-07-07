using System.Net.WebSockets;

namespace Cnblogs.DashScope.Core.Internals;

internal sealed class ClientWebSocketWrapper : IClientWebSocket
{
    private readonly ClientWebSocket _socket;

    public ClientWebSocketWrapper(ClientWebSocket socket)
    {
        _socket = socket;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _socket.Dispose();
    }

    /// <inheritdoc />
    public ClientWebSocketOptions Options => _socket.Options;

    /// <inheritdoc />
    public WebSocketCloseStatus? CloseStatus => _socket.CloseStatus;

    /// <inheritdoc />
    public Task ConnectAsync(Uri uri, CancellationToken cancellation) => _socket.ConnectAsync(uri, cancellation);

    /// <inheritdoc />
    public Task SendAsync(
        ArraySegment<byte> buffer,
        WebSocketMessageType messageType,
        bool endOfMessage,
        CancellationToken cancellationToken)
        => _socket.SendAsync(buffer, messageType, endOfMessage, cancellationToken);

    /// <inheritdoc />
    public Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
        => _socket.ReceiveAsync(buffer, cancellationToken);

    /// <inheritdoc />
    public Task CloseAsync(
        WebSocketCloseStatus closeStatus,
        string? statusDescription,
        CancellationToken cancellationToken)
        => _socket.CloseAsync(closeStatus, statusDescription, cancellationToken);
}
