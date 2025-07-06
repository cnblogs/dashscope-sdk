using System.Collections.Concurrent;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Socket pool for DashScope API.
/// </summary>
public sealed class DashScopeClientWebSocketPool : IDisposable
{
    private readonly ConcurrentBag<DashScopeClientWebSocket> _available = new();
    private readonly ConcurrentDictionary<Guid, DashScopeClientWebSocket> _active = new();
    private readonly DashScopeOptions _options;
    private readonly IDashScopeClientWebSocketFactory _dashScopeClientWebSocketFactory;

    /// <summary>
    /// Socket pool for DashScope API.
    /// </summary>
    /// <param name="dashScopeClientWebSocketFactory"></param>
    /// <param name="options">Options for DashScope sdk.</param>
    public DashScopeClientWebSocketPool(
        IDashScopeClientWebSocketFactory dashScopeClientWebSocketFactory,
        DashScopeOptions options)
    {
        _dashScopeClientWebSocketFactory = dashScopeClientWebSocketFactory;
        _options = options;
    }

    /// <summary>
    /// Get available connection count.
    /// </summary>
    internal int AvailableSocketCount => _available.Count;

    /// <summary>
    /// Get active connection count.
    /// </summary>
    internal int ActiveSocketCount => _active.Count;

    internal DashScopeClientWebSocketPool(
        IEnumerable<DashScopeClientWebSocket> sockets,
        IDashScopeClientWebSocketFactory dashScopeClientWebSocketFactory)
    {
        _options = new DashScopeOptions();
        foreach (var socket in sockets)
        {
            _available.Add(socket);
        }

        _dashScopeClientWebSocketFactory = dashScopeClientWebSocketFactory;
    }

    internal void ReturnSocket(DashScopeClientWebSocket socket)
    {
        _active.Remove(socket.Id, out _);

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
    /// <returns></returns>
    public async Task<DashScopeClientWebSocketWrapper> RentSocketAsync(CancellationToken cancellationToken = default)
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
                socket = await InitializeNewSocketAsync(_options.WebsocketBaseAddress, cancellationToken);
                found = true;
            }
        }

        return ActivateSocket(socket!);
    }

    private DashScopeClientWebSocketWrapper ActivateSocket(DashScopeClientWebSocket socket)
    {
        _active.TryAdd(socket.Id, socket);
        return new DashScopeClientWebSocketWrapper(socket, this);
    }

    private async Task<DashScopeClientWebSocket> InitializeNewSocketAsync(
        string url,
        CancellationToken cancellationToken = default)
    {
        if (_available.Count + _active.Count >= _options.SocketPoolSize)
        {
            throw new InvalidOperationException("[DashScopeSDK] Socket pool is full");
        }

        var socket = _dashScopeClientWebSocketFactory.GetClientWebSocket(_options.ApiKey, _options.WorkspaceId);
        await socket.ConnectAsync(new Uri(url), cancellationToken);
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

            var activeSockets = _active.Values;
            foreach (var activeSocket in activeSockets)
            {
                activeSocket.Dispose();
            }
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
    }
}
