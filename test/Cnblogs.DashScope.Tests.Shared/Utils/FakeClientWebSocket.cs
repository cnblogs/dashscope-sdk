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

    // The undelivered remainder of a message that did not fit into the receiver's buffer,
    // delivered on the next ReceiveAsync call as a continuation frame.
    private (WebSocketMessageType Type, byte[] Payload)? _pendingFragment;

    public List<ArraySegment<byte>> ServerReceivedMessages { get; } = new();

    /// <summary>
    /// The uri passed to the last ConnectAsync call, null when the socket was never connected.
    /// </summary>
    public Uri? ConnectedUri { get; private set; }

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

    public string? CloseStatusDescription { get; set; }

    /// <inheritdoc />
    public Task ConnectAsync(Uri uri, CancellationToken cancellation)
    {
        ConnectedUri = uri;
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public async Task SendAsync(
        ArraySegment<byte> buffer,
        WebSocketMessageType messageType,
        bool endOfMessage,
        CancellationToken cancellationToken)
    {
        // Snapshot the payload: callers may reuse the underlying buffer (e.g. chunked audio),
        // while a real websocket serializes the frame at send time.
        ServerReceivedMessages.Add(new ArraySegment<byte>(buffer.ToArray()));
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
        // Deliver the remainder of an oversized message first, a real websocket delivers
        // such messages as multiple frames with EndOfMessage on the last one only.
        if (_pendingFragment is not null)
        {
            var fragment = _pendingFragment.Value;
            _pendingFragment = null;
            return DeliverFragment(fragment.Type, fragment.Payload, buffer);
        }

        var timeout = Task.Delay(1000, cancellationToken);
        var messageTask = _inboundMessages.Reader.ReadAsync(cancellationToken).AsTask();
        var finishedTask = await Task.WhenAny(messageTask, timeout);
        if (finishedTask == timeout)
        {
            throw new TimeoutException("waiting for next socket message timeouts");
        }

        var (result, payload) = await messageTask;
        if (payload is null)
        {
            MarkConsumed();
            return result;
        }

        return DeliverFragment(result.MessageType, payload, buffer);
    }

    private WebSocketReceiveResult DeliverFragment(
        WebSocketMessageType messageType,
        byte[] payload,
        ArraySegment<byte> buffer)
    {
        var count = Math.Min(payload.Length, buffer.Count);
        for (var i = 0; i < count; i++)
        {
            buffer[i] = payload[i];
        }

        if (count < payload.Length)
        {
            _pendingFragment = (messageType, payload[count..]);
            return new WebSocketReceiveResult(count, messageType, false);
        }

        MarkConsumed();
        return new WebSocketReceiveResult(count, messageType, true);
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
