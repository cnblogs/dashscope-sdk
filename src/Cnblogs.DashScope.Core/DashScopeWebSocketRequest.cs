namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents a websocket request to DashScope.
/// </summary>
/// <typeparam name="TInput">Type of the input.</typeparam>
/// <typeparam name="TParameter">Type of the parameter.</typeparam>
public class DashScopeWebSocketRequest<TInput, TParameter>
    where TInput : class
    where TParameter : class
{
    /// <summary>
    /// Metadata of the request.
    /// </summary>
    public required DashScopeWebSocketRequestHeader Header { get; set; }

    /// <summary>
    /// Payload of the request.
    /// </summary>
    public required DashScopeWebSocketRequestPayload<TInput, TParameter> Payload { get; set; }
}
