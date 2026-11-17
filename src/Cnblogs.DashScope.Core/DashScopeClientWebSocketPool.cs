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
    private readonly Func<string, string?, bool, DashScopeClientWebSocket>? _socketFactory;

    /// <summary>
    /// Socket pool for DashScope API.
    /// </summary>
    /// <param name="options">Options for DashScope sdk.</param>
    public DashScopeClientWebSocketPool(DashScopeOptions options)
    {
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
        DashScopeOptions options,
        Func<string, string?, bool, DashScopeClientWebSocket>? socketFactory)
    {
        _options = options;
        _socketFactory = socketFactory;
    }

    internal DashScopeClientWebSocketPool(
        IEnumerable<DashScopeClientWebSocket> sockets,
        Func<string, string?, bool, DashScopeClientWebSocket>? socketFactory = null)
    {
        _options = new DashScopeOptions();
        _socketFactory = socketFactory;
        foreach (var socket in sockets)
        {
            _available.Add(socket);
        }
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
                socket = await InitializeNewSocketAsync(_options.SelectBaseWssAddress(), cancellationToken);
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

        var enableDataInspection = _options.DataInspection?.ToHeaderValue() is not null;
        var socket = _socketFactory?.Invoke(_options.ApiKey, _options.WorkspaceId, enableDataInspection)
            ?? new DashScopeClientWebSocket(_options.ApiKey, _options.WorkspaceId, enableDataInspection);
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
