namespace Cnblogs.DashScope.Core;

/// <summary>
/// Metadata for websocket request.
/// </summary>
public class DashScopeWebSocketRequestHeader
{
    /// <summary>
    /// Action name.
    /// </summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// UUID for task.
    /// </summary>
    public string TaskId { get; set; } = string.Empty;

    /// <summary>
    /// Streaming type.
    /// </summary>
    public string? Streaming { get; set; } = "duplex";
}
