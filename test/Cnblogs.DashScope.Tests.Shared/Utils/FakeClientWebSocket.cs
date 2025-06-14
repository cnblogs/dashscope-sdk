using System.Net.WebSockets;
using System.Threading.Channels;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public sealed class FakeClientWebSocket : IClientWebSocket
{
    public List<ArraySegment<byte>> ReceivedMessages { get; } = new();

    public Channel<WebSocketReceiveResult> Server { get; } =
        Channel.CreateUnbounded<WebSocketReceiveResult>();

    public async Task WriteServerCloseAsync()
    {
        var close = new WebSocketReceiveResult(1, WebSocketMessageType.Close, true);
        await Server.Writer.WriteAsync(close);
        Server.Writer.Complete();
    }

    private void Dispose(bool disposing)
    {
        // nothing to release.
        if (disposing)
        {
            Server.Writer.Complete();
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
    }

    /// <inheritdoc />
    public ClientWebSocketOptions Options { get; set; } = null!;

    /// <inheritdoc />
    public WebSocketCloseStatus? CloseStatus { get; set; }

    /// <inheritdoc />
    public Task ConnectAsync(Uri uri, CancellationToken cancellation)
    {
        // do nothing.
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task SendAsync(
        ArraySegment<byte> buffer,
        WebSocketMessageType messageType,
        bool endOfMessage,
        CancellationToken cancellationToken)
    {
        ReceivedMessages.Add(buffer);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public async Task<WebSocketReceiveResult> ReceiveAsync(
        ArraySegment<byte> buffer,
        CancellationToken cancellationToken)
    {
        await Server.Reader.WaitToReadAsync(cancellationToken);
        return await Server.Reader.ReadAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task CloseAsync(
        WebSocketCloseStatus closeStatus,
        string? statusDescription,
        CancellationToken cancellationToken)
    {
        CloseStatus = WebSocketCloseStatus.NormalClosure;
        return Task.CompletedTask;
    }
}
