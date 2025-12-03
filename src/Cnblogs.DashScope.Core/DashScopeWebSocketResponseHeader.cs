namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Metadata of the websocket response.
    /// </summary>
    /// <param name="TaskId">TaskId of the task.</param>
    /// <param name="Event">Event name.</param>
    /// <param name="ErrorCode">Error code when <paramref name="Event"/> is task-failed.</param>
    /// <param name="ErrorMessage">Error message when <paramref name="Event"/> is task-failed.</param>
    /// <param name="Attributes">Optional attributes</param>
    public record DashScopeWebSocketResponseHeader(
        string TaskId,
        string Event,
        string? ErrorCode,
        string? ErrorMessage,
        DashScopeWebSocketResponseHeaderAttributes Attributes);
}
