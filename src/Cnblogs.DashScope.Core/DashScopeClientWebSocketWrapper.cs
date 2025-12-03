using System.Text.Json;

namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Represents a transient wrapper for rented websocket, should be transient.
    /// </summary>
    /// <param name="Socket">The rented websocket</param>
    /// <param name="Pool">The pool to return the socket to.</param>
    public sealed record DashScopeClientWebSocketWrapper(DashScopeClientWebSocket Socket, DashScopeClientWebSocketPool Pool)
        : IDisposable
    {
    /// <summary>
    /// The binary output.
    /// </summary>
    public IAsyncEnumerable<byte> BinaryOutput => Socket.BinaryOutput.ReadAllAsync();

    /// <summary>
    /// The json message output.
    /// </summary>
    public IAsyncEnumerable<DashScopeWebSocketResponse<JsonElement>> JsonOutput => Socket.JsonOutput.ReadAllAsync();

    /// <summary>
    /// Reset task signal and output cannel.
    /// </summary>
    public void ResetTask() => Socket.ResetOutput();

    /// <summary>
    /// The task that completes when received task-started event from server.
    /// </summary>
    public Task TaskStarted => Socket.TaskStarted;

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
        where TInput : class, new()
        where TParameter : class
        => Socket.SendMessageAsync(request, cancellationToken);

    /// <inheritdoc />
    public void Dispose()
    {
        Pool.ReturnSocket(Socket);
    }
    }
}
