using System.Net.WebSockets;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents a websocket session.
/// </summary>
public abstract class WsSession(ClientWebSocket socket) : IDisposable
{
    private bool _isDisposed;

    /// <summary>
    /// Id for current session.
    /// </summary>
    protected Guid SessionId { get; } = Guid.NewGuid();

    /// <summary>
    /// Start session.
    /// </summary>
    /// <returns></returns>
    protected abstract Task StartAsync();

    /// <summary>
    /// Finish session.
    /// </summary>
    /// <returns></returns>
    protected abstract Task FinishAsync();

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Dispose socket.
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed)
        {
            return;
        }

        if (disposing)
        {
            socket.Dispose();
        }

        _isDisposed = true;
    }
}
