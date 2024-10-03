namespace Cnblogs.DashScope.Core;

/// <summary>
/// Header info for a websocket task.
/// </summary>
/// <param name="Action">Type of action.</param>
/// <param name="TaskId">Unique id of task.</param>
/// <param name="Streaming">Streaming type.</param>
public record WsHeader(string Action, string TaskId, string Streaming);
