namespace Cnblogs.DashScope.Core;

/// <summary>
/// Metadata for websocket request.
/// </summary>
public class DashScopeWebSocketRequestHeader
{
    /// <summary>
    /// Action name.
    /// </summary>
    public required string Action { get; set; }

    /// <summary>
    /// UUID for task.
    /// </summary>
    public required string TaskId { get; set; }

    /// <summary>
    /// Streaming type.
    /// </summary>
    public string Streaming { get; set; } = "duplex";
}
