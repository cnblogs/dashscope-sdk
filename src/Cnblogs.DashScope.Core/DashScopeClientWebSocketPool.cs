using System.Collections.Concurrent;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Socket pool for DashScope API.
/// </summary>
/// <param name="options"></param>
public sealed class DashScopeClientWebSocketPool(DashScopeOptions options) : IDisposable
{
    private readonly ConcurrentBag<DashScopeClientWebSocket> _available = new();
    private readonly ConcurrentBag<DashScopeClientWebSocket> _active = new();

    internal void ReturnSocketAsync(DashScopeClientWebSocket socket)
    {
        if (socket.State != DashScopeWebSocketState.Connected)
        {
            // not returnable, disposing.
            socket.Dispose();
        }

        _available.Add(socket);
    }

    /// <summary>
    /// Rent or create a socket connection from pool.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TOutput">The output type.</typeparam>
    /// <returns></returns>
    public async Task<DashScopeClientWebSocketWrapper> RentSocketAsync<TOutput>(
        CancellationToken cancellationToken = default)
        where TOutput : class
    {
        var found = false;
        DashScopeClientWebSocket? socket = null;
        while (found == false)
        {
            if (_available.IsEmpty == false)
            {
                found = _available.TryTake(out socket);
                if (socket?.State != DashScopeWebSocketState.Connected)
                {
                    // expired
                    found = false;
                }
            }
            else
            {
                socket = await InitializeNewSocketAsync<TOutput>(options.BaseWebsocketAddress, cancellationToken);
                found = true;
            }
        }

        return ActiveSocket(socket!);
    }

    private DashScopeClientWebSocketWrapper ActiveSocket(DashScopeClientWebSocket socket)
    {
        socket.ResetOutput();
        _active.Add(socket);
        return new DashScopeClientWebSocketWrapper(socket, this);
    }

    private async Task<DashScopeClientWebSocket> InitializeNewSocketAsync<TOutput>(
        string url,
        CancellationToken cancellationToken = default)
        where TOutput : class
    {
        if (_available.Count + _active.Count >= options.SocketPoolSize)
        {
            throw new InvalidOperationException("[DashScopeSDK] Socket pool is full");
        }

        var socket = new DashScopeClientWebSocket(options.ApiKey, options.WorkspaceId);
        await socket.ConnectAsync<TOutput>(new Uri(url), cancellationToken);
        return socket;
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Dispose managed resources.
            while (_available.IsEmpty == false)
            {
                _available.TryTake(out var socket);
                socket?.Dispose();
            }

            while (_active.IsEmpty == false)
            {
                _active.TryTake(out var socket);
                socket?.Dispose();
            }
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
    }
}
