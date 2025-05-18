namespace Cnblogs.DashScope.Core;

/// <summary>
/// Payload field of websocket API response.
/// </summary>
/// <param name="Output">Content of the response.</param>
/// <param name="Usage">Task usage info.</param>
/// <typeparam name="TOutput">Type of the response content.</typeparam>
public record DashScopeWebSocketResponsePayload<TOutput>(TOutput? Output, DashScopeWebSocketResponseUsage? Usage)
    where TOutput : class;
