namespace Cnblogs.DashScope.Core;

/// <summary>
/// Response from websocket API.
/// </summary>
/// <param name="Header">Response metadatas.</param>
/// <param name="Payload">Response body.</param>
/// <typeparam name="TOutput">Output type of the response.</typeparam>
public record DashScopeWebSocketResponse<TOutput>(
    DashScopeWebSocketResponseHeader Header,
    DashScopeWebSocketResponsePayload<TOutput> Payload)
    where TOutput : class;
