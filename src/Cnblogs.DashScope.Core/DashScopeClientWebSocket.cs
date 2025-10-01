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
    private static readonly UnboundedChannelOptions UnboundedChannelOptions =
        new()
        {
            SingleWriter = true,
            SingleReader = true,
            AllowSynchronousContinuations = true
        };

    private readonly IClientWebSocket _socket;
    // ReSharper disable once NotAccessedField.Local
    private Task? _receiveTask;
    private TaskCompletionSource<bool> _taskStartedSignal = new();
    private Channel<byte>? _binaryOutput;
    private Channel<DashScopeWebSocketResponse<JsonElement>>? _jsonOutput;

    /// <summary>
    /// Unique id of this socket.
    /// </summary>
    internal Guid Id { get; } = Guid.NewGuid();

    /// <summary>
    /// The binary output.
    /// </summary>
    public ChannelReader<byte> BinaryOutput
        => _binaryOutput?.Reader
           ?? throw new InvalidOperationException("Please call ResetOutput() before accessing output");

    /// <summary>
    /// The json output.
    /// </summary>
    /// <exception cref="InvalidOperationException">Throws when ResetOutput is not called.</exception>
    public ChannelReader<DashScopeWebSocketResponse<JsonElement>> JsonOutput
        => _jsonOutput?.Reader
           ?? throw new InvalidOperationException("Please call ResetOutput() before accessing output");

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
        _socket = new ClientWebSocketWrapper(new ClientWebSocket());
        _socket.Options.SetRequestHeader("X-DashScope-DataInspection", "enable");
        _socket.Options.SetRequestHeader("Authorization", $"bearer {apiKey}");
        if (string.IsNullOrEmpty(workspaceId) == false)
        {
            _socket.Options.SetRequestHeader("X-DashScope-WorkspaceId", workspaceId);
        }
    }

    /// <summary>
    /// Initiate a <see cref="DashScopeClientWebSocket"/> with a pre-configured <see cref="ClientWebSocket"/>.
    /// </summary>
    /// <param name="socket">Pre-configured <see cref="ClientWebSocket"/>.</param>
    internal DashScopeClientWebSocket(IClientWebSocket socket)
    {
        _socket = socket;
    }

    /// <summary>
    /// Start a websocket connection.
    /// </summary>
    /// <param name="uri">Websocket API uri.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    /// <exception cref="OperationCanceledException">When <paramref name="cancellationToken"/> was request.</exception>
    public async Task ConnectAsync(Uri uri, CancellationToken cancellationToken = default)
    {
        await _socket.ConnectAsync(uri, cancellationToken);
        _receiveTask = ReceiveMessagesAsync(cancellationToken);
        State = DashScopeWebSocketState.Ready;
    }

    /// <summary>
    /// Reset binary output.
    /// </summary>
    public void ResetOutput()
    {
        _binaryOutput?.Writer.TryComplete();
        _binaryOutput = Channel.CreateUnbounded<byte>(UnboundedChannelOptions);
        _jsonOutput?.Writer.TryComplete();
        _jsonOutput = Channel.CreateUnbounded<DashScopeWebSocketResponse<JsonElement>>(UnboundedChannelOptions);
        _taskStartedSignal.TrySetResult(false);
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
    /// <exception cref="InvalidOperationException">Websocket is not connected or already closed.</exception>
    /// <exception cref="ObjectDisposedException">The underlying websocket has already been closed.</exception>
    public Task SendMessageAsync<TInput, TParameter>(
        DashScopeWebSocketRequest<TInput, TParameter> request,
        CancellationToken cancellationToken = default)
        where TInput : class, new()
        where TParameter : class
    {
        if (State == DashScopeWebSocketState.Closed)
        {
            throw new InvalidOperationException("Socket is already closed.");
        }

        var json = JsonSerializer.Serialize(request, DashScopeDefaults.SerializationOptions);
        return _socket.SendAsync(
            new ArraySegment<byte>(Encoding.UTF8.GetBytes(json)),
            WebSocketMessageType.Text,
            true,
            cancellationToken);
    }

    private async Task<DashScopeWebSocketResponse<TOutput>?> ReceiveMessageAsync<TOutput>(
        CancellationToken cancellationToken = default)
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
                    await _binaryOutput!.Writer.WriteAsync(buffer[i], cancellationToken);
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
    /// <exception cref="DashScopeException">The task was failed.</exception>
    private async Task ReceiveMessagesAsync(CancellationToken cancellationToken = default)
    {
        while (State != DashScopeWebSocketState.Closed && _socket.CloseStatus == null)
        {
            var json = await ReceiveMessageAsync<JsonElement>(cancellationToken);
            if (json == null)
            {
                continue;
            }

            if (_jsonOutput is not null)
            {
                await _jsonOutput.Writer.WriteAsync(json, cancellationToken);
            }

            var eventStr = json.Header.Event;
            switch (eventStr)
            {
                case "task-started":
                    State = DashScopeWebSocketState.RunningTask;
                    _taskStartedSignal.TrySetResult(true);
                    break;
                case "task-finished":
                    State = DashScopeWebSocketState.Ready;
                    _binaryOutput?.Writer.Complete();
                    _jsonOutput?.Writer.Complete();
                    break;
                case "task-failed":
                    await CloseAsync(cancellationToken);
                    _binaryOutput?.Writer.Complete();
                    _jsonOutput?.Writer.Complete();
                    throw new DashScopeException(
                        null,
                        400,
                        new DashScopeError
                        {
                            Code = json.Header.ErrorCode ?? string.Empty,
                            Message = json.Header.ErrorMessage ?? string.Empty,
                            RequestId = json.Header.Attributes.RequestUuid ?? string.Empty
                        },
                        json.Header.ErrorMessage ?? "The task was failed");
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
        State = DashScopeWebSocketState.Closed;
        _binaryOutput?.Writer.TryComplete();
        _jsonOutput?.Writer.TryComplete();
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Dispose managed resources.
            _socket.Dispose();
            _binaryOutput?.Writer.TryComplete();
            _jsonOutput?.Writer.TryComplete();
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
    }
}
