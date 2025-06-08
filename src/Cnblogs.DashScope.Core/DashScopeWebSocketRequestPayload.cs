namespace Cnblogs.DashScope.Core;

/// <summary>
/// Payload for websocket request.
/// </summary>
/// <typeparam name="TInput">Type of the input.</typeparam>
/// <typeparam name="TParameter">Type of the parameter.</typeparam>
public class DashScopeWebSocketRequestPayload<TInput, TParameter>
    where TInput : class
    where TParameter : class
{
    /// <summary>
    /// Group name of task.
    /// </summary>
    public string? TaskGroup { get; set; }

    /// <summary>
    /// Requesting task name.
    /// </summary>
    public string? Task { get; set; }

    /// <summary>
    /// Requesting function name.
    /// </summary>
    public string? Function { get; set; }

    /// <summary>
    /// Model id.
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// Optional parameters.
    /// </summary>
    public TParameter? Parameters { get; set; }

    /// <summary>
    /// The input of the request.
    /// </summary>
    public TInput Input { get; set; } = null!;
}
