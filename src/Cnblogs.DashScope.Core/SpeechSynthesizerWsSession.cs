using System.Net.WebSockets;

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

    }

    /// <inheritdoc />
    protected override async Task FinishAsync()
    {
        throw new NotImplementedException();
    }
}
