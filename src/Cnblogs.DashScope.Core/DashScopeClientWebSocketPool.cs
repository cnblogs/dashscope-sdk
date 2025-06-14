using System.Collections.Concurrent;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Socket pool for DashScope API.
/// </summary>
public sealed class DashScopeClientWebSocketPool : IDisposable
{
    private readonly ConcurrentBag<DashScopeClientWebSocket> _available = new();
    private readonly ConcurrentBag<DashScopeClientWebSocket> _active = new();
    private readonly DashScopeOptions _options;

    /// <summary>
    /// Socket pool for DashScope API.
    /// </summary>
    /// <param name="options">Options for DashScope sdk.</param>
    public DashScopeClientWebSocketPool(DashScopeOptions options)
    {
        _options = options;
    }

    internal DashScopeClientWebSocketPool(IEnumerable<DashScopeClientWebSocket> sockets)
    {
        _options = new DashScopeOptions();
        foreach (var socket in sockets)
        {
            _available.Add(socket);
        }
    }

    internal void ReturnSocketAsync(DashScopeClientWebSocket socket)
    {
        if (socket.State != DashScopeWebSocketState.Ready)
        {
            // not returnable, disposing.
            socket.Dispose();
            return;
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
                if (socket?.State != DashScopeWebSocketState.Ready)
                {
                    // expired
                    found = false;
                    socket?.Dispose();
                }
            }
            else
            {
                socket = await InitializeNewSocketAsync<TOutput>(_options.BaseWebsocketAddress, cancellationToken);
                found = true;
            }
        }

        return ActivateSocket(socket!);
    }

    private DashScopeClientWebSocketWrapper ActivateSocket(DashScopeClientWebSocket socket)
    {
        _active.Add(socket);
        return new DashScopeClientWebSocketWrapper(socket, this);
    }

    private async Task<DashScopeClientWebSocket> InitializeNewSocketAsync<TOutput>(
        string url,
        CancellationToken cancellationToken = default)
        where TOutput : class
    {
        if (_available.Count + _active.Count >= _options.SocketPoolSize)
        {
            throw new InvalidOperationException("[DashScopeSDK] Socket pool is full");
        }

        var socket = new DashScopeClientWebSocket(_options.ApiKey, _options.WorkspaceId);
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
