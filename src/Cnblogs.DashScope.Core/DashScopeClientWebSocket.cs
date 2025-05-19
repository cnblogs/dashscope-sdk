using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// A websocket client for DashScope websocket API.
/// </summary>
public sealed class DashScopeClientWebSocket : IDisposable
{
    private static readonly UnboundedChannelOptions UnboundedChannelOptions = new()
    {
        SingleWriter = true,
    };

    private readonly ClientWebSocket _socket = new();
    private Task? _receiveTask;
    private TaskCompletionSource<bool> _taskStartedSignal = new();

    /// <summary>
    /// The binary output.
    /// </summary>
    public Channel<byte> BinaryOutput { get; private set; } = Channel.CreateUnbounded<byte>(UnboundedChannelOptions);

    /// <summary>
    /// A task that completed when received task-started event.
    /// </summary>
    public Task<bool> TaskStarted => _taskStartedSignal.Task;

    /// <summary>
    /// Current state for this websocket.
    /// </summary>
    public DashScopeWebSocketState State { get; private set; }

    /// <summary>
    /// Initialize a configured web socket client.
    /// </summary>
    /// <param name="apiKey">The api key to use.</param>
    /// <param name="workspaceId">Optional workspace id.</param>
    public DashScopeClientWebSocket(string apiKey, string? workspaceId = null)
    {
        _socket.Options.SetRequestHeader("X-DashScope-DataInspection", "enable");
        _socket.Options.SetRequestHeader("Authorization", $"bearer {apiKey}");
        if (string.IsNullOrEmpty(workspaceId) == false)
        {
            _socket.Options.SetRequestHeader("X-DashScope-WorkspaceId", workspaceId);
        }
    }

    /// <summary>
    /// Start a websocket connection.
    /// </summary>
    /// <param name="uri">Websocket API uri.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    /// <typeparam name="TOutput">The type of the response content.</typeparam>
    /// <exception cref="OperationCanceledException">When <paramref name="cancellationToken"/> was request.</exception>
    public async Task ConnectAsync<TOutput>(Uri uri, CancellationToken cancellationToken = default)
        where TOutput : class
    {
        await _socket.ConnectAsync(uri, cancellationToken);
        _receiveTask = ReceiveMessagesAsync<TOutput>(cancellationToken);
        State = DashScopeWebSocketState.Connected;
    }

    /// <summary>
    /// Reset binary output.
    /// </summary>
    public void ResetOutput()
    {
        BinaryOutput = Channel.CreateUnbounded<byte>(UnboundedChannelOptions);
        _taskStartedSignal = new TaskCompletionSource<bool>();
    }

    /// <summary>
    /// Send message to server.
    /// </summary>
    /// <param name="request">Request to send.</param>
    /// <param name="cancellationToken">A cancellation token used to propagate notification that this operation should be canceled.</param>
    /// <returns></returns>
    /// <typeparam name="TInput">Type of the input.</typeparam>
    /// <typeparam name="TParameter">Type of the parameter.</typeparam>
    /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> is requested.</exception>
    /// <exception cref="InvalidOperationException">Websocket is not connected.</exception>
    /// <exception cref="ObjectDisposedException">The underlying websocket has already been closed.</exception>
    public Task SendMessageAsync<TInput, TParameter>(
        DashScopeWebSocketRequest<TInput, TParameter> request,
        CancellationToken cancellationToken = default)
        where TInput : class
        where TParameter : class
    {
        return _socket.SendAsync(
            new ArraySegment<byte>(
                Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request, DashScopeDefaults.SerializationOptions))),
            WebSocketMessageType.Text,
            true,
            cancellationToken);
    }

    private async Task<DashScopeWebSocketResponse<TOutput>?> ReceiveMessageAsync<TOutput>(
        CancellationToken cancellationToken = default)
        where TOutput : class
    {
        var buffer = new byte[1024 * 4];
        var segment = new ArraySegment<byte>(buffer);

        try
        {
            var result = await _socket.ReceiveAsync(segment, cancellationToken);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                await CloseAsync(cancellationToken);
                return null;
            }

            if (result.MessageType == WebSocketMessageType.Binary)
            {
                for (var i = 0; i < result.Count; i++)
                {
                    await BinaryOutput.Writer.WriteAsync(buffer[i], cancellationToken);
                }

                return null;
            }

            var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
            var jsonResponse =
                JsonSerializer.Deserialize<DashScopeWebSocketResponse<TOutput>>(
                    message,
                    DashScopeDefaults.SerializationOptions);
            return jsonResponse;
        }
        catch
        {
            // close socket when exception happens.
            await CloseAsync(cancellationToken);
        }

        return null;
    }

    /// <summary>
    /// Wait for server response.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token used to propagate notification that this operation should be canceled.</param>
    /// <typeparam name="TOutput">Type of the response content.</typeparam>
    /// <exception cref="DashScopeException">The task was failed.</exception>
    public async Task ReceiveMessagesAsync<TOutput>(CancellationToken cancellationToken = default)
        where TOutput : class
    {
        while (State != DashScopeWebSocketState.Closed && _socket.CloseStatus == null)
        {
            var json = await ReceiveMessageAsync<TOutput>(cancellationToken);
            if (json == null)
            {
                continue;
            }

            var eventStr = json.Header.Event;
            switch (eventStr)
            {
                case "task-started":
                    State = DashScopeWebSocketState.RunningTask;
                    _taskStartedSignal.TrySetResult(true);
                    break;
                case "task-finished":
                    State = DashScopeWebSocketState.Connected;
                    BinaryOutput.Writer.Complete();
                    break;
                case "task-failed":
                    await CloseAsync(cancellationToken);
                    throw new DashScopeException(
                        null,
                        400,
                        new DashScopeError()
                        {
                            Code = json.Header.ErrorCode ?? string.Empty,
                            Message = json.Header.ErrorMessage ?? string.Empty,
                            RequestId = json.Header.Attributes.RequestUuid ?? string.Empty
                        },
                        json.Header.ErrorMessage ?? "The task was failed");
                default:
                    break;
            }
        }

        await CloseAsync(cancellationToken);
    }

    /// <summary>
    /// Close the underlying websocket.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token used to propagate notification that this operation should be canceled.</param>
    /// <returns></returns>
    public async Task CloseAsync(CancellationToken cancellationToken = default)
    {
        await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", cancellationToken);
        Dispose();
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Dispose managed resources.
            _socket.Dispose();
            State = DashScopeWebSocketState.Closed;
            BinaryOutput.Writer.TryComplete();
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
    }
}
