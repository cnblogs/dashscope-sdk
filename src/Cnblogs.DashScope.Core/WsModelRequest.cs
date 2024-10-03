namespace Cnblogs.DashScope.Core;

/// <summary>
/// Model request for DashScope websocket.
/// </summary>
/// <typeparam name="TInput">Type of the input.</typeparam>
public class WsModelRequest<TInput> : ModelRequest<TInput>, IWsModelRequest
    where TInput : class
{
    /// <summary>
    /// Group name of the task.
    /// </summary>
    public required string TaskGroup { get; init; }

    /// <summary>
    /// Name of the task.
    /// </summary>
    public required string Task { get; init; }

    /// <summary>
    /// Name of the function to call.
    /// </summary>
    public required string Function { get; init; }
}

/// <summary>
/// Model request for DashScope websocket.
/// </summary>
/// <typeparam name="TInput">Type of the input.</typeparam>
/// <typeparam name="TParameter">Optional parameters type.</typeparam>
public class WsModelRequest<TInput, TParameter> : ModelRequest<TInput, TParameter>, IWsModelRequest
    where TInput : class
    where TParameter : class
{
    /// <inheritdoc />
    public required string TaskGroup { get; init; }

    /// <inheritdoc />
    public required string Task { get; init; }

    /// <inheritdoc />
    public required string Function { get; init; }
}
