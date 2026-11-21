using System.Collections.Concurrent;
using System.Net.WebSockets;
using Cnblogs.DashScope.Core.Internals;

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
    private readonly Func<string, string?, bool, IClientWebSocket>? _realtimeSocketFactory;

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
        Func<string, string?, bool, DashScopeClientWebSocket>? socketFactory,
        Func<string, string?, bool, IClientWebSocket>? realtimeSocketFactory = null)
    {
        _options = options;
        _socketFactory = socketFactory;
        _realtimeSocketFactory = realtimeSocketFactory;
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

        if (socket.State != DashScopeWebSocketState.Ready
            || socket.IsDataInspectionEnabled != IsPoolInspectionEnabled)
        {
            // not returnable (closed or a temporary connection with different handshake), disposing.
            socket.Dispose();
            return;
        }

        _available.Add(socket);
    }

    /// <summary>
    /// Rent or create a socket connection from pool. Connections in the pool always follow the data inspection
    /// configuration in <see cref="DashScopeOptions"/>; when <paramref name="dataInspection"/> resolves to a different
    /// mode, a temporary connection is created for this rental only and disposed on return.
    /// </summary>
    /// <param name="dataInspection">Data inspection configuration for the handshake of the rented connection,
    /// null to follow the client level configuration. See <see cref="DataInspectionOptions"/> for available values.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<DashScopeClientWebSocketWrapper> RentSocketAsync(
        DataInspectionOptions? dataInspection = null,
        CancellationToken cancellationToken = default)
    {
        var requestInspectionEnabled = dataInspection is null
            ? IsPoolInspectionEnabled
            : dataInspection.ToHeaderValue() is not null;
        if (requestInspectionEnabled != IsPoolInspectionEnabled)
        {
            // sockets with a different handshake are not interchangeable with pooled ones, create a temporary connection.
            var temporary = await CreateAndConnectSocketAsync(requestInspectionEnabled, cancellationToken);
            return ActivateSocket(temporary);
        }

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
                if (_available.Count + _active.Count >= _options.SocketPoolSize)
                {
                    throw new InvalidOperationException("[DashScopeSDK] Socket pool is full");
                }

                socket = await CreateAndConnectSocketAsync(IsPoolInspectionEnabled, cancellationToken);
                found = true;
            }
        }

        return ActivateSocket(socket!);
    }

    private bool IsPoolInspectionEnabled => _options.DataInspection?.ToHeaderValue() is not null;

    /// <summary>
    /// Create and connect a websocket for the session-based realtime APIs (/api-ws/v1/realtime,
    /// e.g. qwen3-tts-*-realtime and qwen-*-omni-*-realtime models), the model is passed as the
    /// <c>model</c> query parameter. Unlike <see cref="RentSocketAsync"/> the connection is not
    /// pooled - the server closes it when the session ends, so every call gets a fresh connection.
    /// </summary>
    /// <param name="modelId">The model to use, passed as the model query parameter.</param>
    /// <param name="dataInspection">Data inspection configuration for the connection handshake,
    /// null to follow the client level configuration. See <see cref="DataInspectionOptions"/> for available values.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>The connected websocket.</returns>
    internal async Task<IClientWebSocket> CreateRealtimeSocketAsync(
        string modelId,
        DataInspectionOptions? dataInspection = null,
        CancellationToken cancellationToken = default)
    {
        var requestInspectionEnabled = dataInspection is null
            ? IsPoolInspectionEnabled
            : dataInspection.ToHeaderValue() is not null;
        var socket = _realtimeSocketFactory?.Invoke(_options.ApiKey, _options.WorkspaceId, requestInspectionEnabled)
            ?? CreateRealtimeSocket(_options.ApiKey, _options.WorkspaceId, requestInspectionEnabled);
        var uri = new Uri(
            $"{_options.SelectBaseWssAddress().TrimEnd('/')}/api-ws/v1/realtime?model={Uri.EscapeDataString(modelId)}");
        await socket.ConnectAsync(uri, cancellationToken);
        return socket;
    }

    private static IClientWebSocket CreateRealtimeSocket(
        string apiKey,
        string? workspaceId,
        bool enableDataInspection)
    {
        var socket = new ClientWebSocketWrapper(new ClientWebSocket());
        if (enableDataInspection)
        {
            socket.Options.SetRequestHeader("X-DashScope-DataInspection", "enable");
        }

        socket.Options.SetRequestHeader("Authorization", $"bearer {apiKey}");
        if (string.IsNullOrEmpty(workspaceId) == false)
        {
            socket.Options.SetRequestHeader("X-DashScope-WorkspaceId", workspaceId);
        }

        return socket;
    }

    private DashScopeClientWebSocketWrapper ActivateSocket(DashScopeClientWebSocket socket)
    {
        _active.TryAdd(socket.Id, socket);
        return new DashScopeClientWebSocketWrapper(socket, this);
    }

    private async Task<DashScopeClientWebSocket> CreateAndConnectSocketAsync(
        bool enableDataInspection,
        CancellationToken cancellationToken)
    {
        var socket = _socketFactory?.Invoke(_options.ApiKey, _options.WorkspaceId, enableDataInspection)
            ?? new DashScopeClientWebSocket(_options.ApiKey, _options.WorkspaceId, enableDataInspection);
        await socket.ConnectAsync(
            new Uri($"{_options.SelectBaseWssAddress().TrimEnd('/')}/api-ws/v1/inference/"),
            cancellationToken);
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
