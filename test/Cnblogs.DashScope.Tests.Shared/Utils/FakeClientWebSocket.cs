using System.Net.WebSockets;
using System.Text;
using System.Threading.Channels;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public sealed class FakeClientWebSocket : IClientWebSocket
{
    // A message is (result, payload) pair so that the two can never be read out of order,
    // which used to happen when they traveled through two separate channels.
    private readonly Channel<(WebSocketReceiveResult Result, byte[]? Payload)> _inboundMessages
        = Channel.CreateUnbounded<(WebSocketReceiveResult, byte[]?)>();

    private readonly object _consumedLock = new();
    private int _pendingMessages;
    private TaskCompletionSource _allMessagesConsumed =
        new(TaskCreationOptions.RunContinuationsAsynchronously);

    public List<ArraySegment<byte>> ServerReceivedMessages { get; } = new();

    public Queue<Func<FakeClientWebSocket, Task>> Playlist { get; } = new();

    public bool DisposeCalled { get; private set; }

    public async Task WriteServerCloseAsync()
    {
        await WriteServerMessageCoreAsync(new WebSocketReceiveResult(1, WebSocketMessageType.Close, true), null);
    }

    public Task WriteServerMessageAsync(string json)
    {
        var binary = Encoding.UTF8.GetBytes(json);
        return WriteServerMessageCoreAsync(
            new WebSocketReceiveResult(binary.Length, WebSocketMessageType.Text, true),
            binary);
    }

    public Task WriteServerMessageAsync(byte[] binary)
    {
        return WriteServerMessageCoreAsync(
            new WebSocketReceiveResult(binary.Length, WebSocketMessageType.Binary, true),
            binary);
    }

    private async Task WriteServerMessageCoreAsync(WebSocketReceiveResult result, byte[]? payload)
    {
        lock (_consumedLock)
        {
            _pendingMessages++;
        }

        await _inboundMessages.Writer.WriteAsync((result, payload));

        // Wait until the client picks the message up. Waiting on "channel has data"
        // (WaitToReadAsync) deadlocks when the receive loop already consumed the message
        // between the write above and the wait, so consumption is tracked explicitly.
        // The timeout keeps the helper from hanging when the receive loop is already
        // gone (e.g. terminated by a task-failed event) and never consumes anything.
        Task consumed;
        lock (_consumedLock)
        {
            consumed = _pendingMessages == 0 ? Task.CompletedTask : _allMessagesConsumed.Task;
        }

        await Task.WhenAny(consumed, Task.Delay(500));
        await Task.Delay(50);
    }

    private void MarkConsumed()
    {
        lock (_consumedLock)
        {
            _pendingMessages--;
            if (_pendingMessages != 0)
            {
                return;
            }

            _allMessagesConsumed.TrySetResult();
            _allMessagesConsumed = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        }
    }

    private void Dispose(bool disposing)
    {
        // nothing to release.
        if (disposing)
        {
            DisposeCalled = true;
            _inboundMessages.Writer.TryComplete();
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
    public async Task SendAsync(
        ArraySegment<byte> buffer,
        WebSocketMessageType messageType,
        bool endOfMessage,
        CancellationToken cancellationToken)
    {
        ServerReceivedMessages.Add(buffer);
        if (Playlist.Count > 0)
        {
            await Playlist.Dequeue().Invoke(this);
        }
    }

    /// <inheritdoc />
    public async Task<WebSocketReceiveResult> ReceiveAsync(
        ArraySegment<byte> buffer,
        CancellationToken cancellationToken)
    {
        var timeout = Task.Delay(1000, cancellationToken);
        var messageTask = _inboundMessages.Reader.ReadAsync(cancellationToken).AsTask();
        var finishedTask = await Task.WhenAny(messageTask, timeout);
        if (finishedTask == timeout)
        {
            throw new TimeoutException("waiting for next socket message timeouts");
        }

        var (result, payload) = await messageTask;
        if (payload is not null)
        {
            for (var i = 0; i < payload.Length; i++)
            {
                buffer[i] = payload[i];
            }
        }

        MarkConsumed();
        return result;
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
