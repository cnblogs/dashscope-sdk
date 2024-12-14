using System.Net.WebSockets;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Ws session for SpeechSynthesizer
/// </summary>
/// <param name="socket">The underlying socket.</param>
public class SpeechSynthesizerWsSession(ClientWebSocket socket) : WsSession(socket)
{
    /// <inheritdoc />
    protected override async Task StartAsync()
    {
        await socket.ConnectAsync(new Uri(ApiLinks.WsLink), CancellationToken.None);
    }

    /// <inheritdoc />
    protected override async Task FinishAsync()
    {
        throw new NotImplementedException();
    }
}
