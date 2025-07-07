namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents a websocket request to DashScope.
/// </summary>
/// <typeparam name="TInput">Type of the input.</typeparam>
/// <typeparam name="TParameter">Type of the parameter.</typeparam>
public class DashScopeWebSocketRequest<TInput, TParameter>
    where TInput : class, new()
    where TParameter : class
{
    /// <summary>
    /// Metadata of the request.
    /// </summary>
    public DashScopeWebSocketRequestHeader Header { get; set; } = new();

    /// <summary>
    /// Payload of the request.
    /// </summary>
    public DashScopeWebSocketRequestPayload<TInput, TParameter> Payload { get; set; } = new();
}
