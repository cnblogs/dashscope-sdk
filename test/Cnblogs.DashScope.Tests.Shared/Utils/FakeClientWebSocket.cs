using System.Net.WebSockets;
using System.Text;
using System.Threading.Channels;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public sealed class FakeClientWebSocket : IClientWebSocket
{
    public List<ArraySegment<byte>> ReceivedMessages { get; } = new();

    public Channel<WebSocketReceiveResult> Server { get; } =
        Channel.CreateUnbounded<WebSocketReceiveResult>();

    public Channel<byte[]> ServerBuffer { get; } = Channel.CreateUnbounded<byte[]>();

    public bool DisposeCalled { get; private set; }

    public async Task WriteServerCloseAsync()
    {
        var close = new WebSocketReceiveResult(1, WebSocketMessageType.Close, true);
        await Server.Writer.WriteAsync(close);
        await ServerBuffer.Writer.WriteAsync(new byte[] { 1 });
        await Server.Reader.WaitToReadAsync();
        await ServerBuffer.Reader.WaitToReadAsync();
        await Task.Delay(50);
    }

    public async Task WriteServerMessageAsync(string json)
    {
        var binary = Encoding.UTF8.GetBytes(json);
        await Server.Writer.WriteAsync(new WebSocketReceiveResult(binary.Length, WebSocketMessageType.Text, true));

        await ServerBuffer.Writer.WriteAsync(binary);
        await Server.Reader.WaitToReadAsync();
        await ServerBuffer.Reader.WaitToReadAsync();
        await Task.Delay(50);
    }

    public async Task WriteServerMessageAsync(byte[] binary)
    {
        await Server.Writer.WriteAsync(new WebSocketReceiveResult(binary.Length, WebSocketMessageType.Binary, true));

        await ServerBuffer.Writer.WriteAsync(binary);
        await Server.Reader.WaitToReadAsync();
        await ServerBuffer.Reader.WaitToReadAsync();
        await Task.Delay(50);
    }

    private void Dispose(bool disposing)
    {
        // nothing to release.
        if (disposing)
        {
            DisposeCalled = true;
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
        await ServerBuffer.Reader.WaitToReadAsync(cancellationToken);
        var binary = await ServerBuffer.Reader.ReadAsync(cancellationToken);
        for (var i = 0; i < binary.Length; i++)
        {
            buffer[i] = binary[i];
        }

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
