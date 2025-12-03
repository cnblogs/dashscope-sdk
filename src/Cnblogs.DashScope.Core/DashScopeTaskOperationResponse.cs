namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// The operation result of operation to DashScope task.
    /// </summary>
    /// <param name="RequestId">The id of this request.</param>
    /// <param name="Code">The error code, not null if operation failed.</param>
    /// <param name="Message">The error message, not null if operation failed.</param>
    public record DashScopeTaskOperationResponse(string RequestId, string? Code, string? Message);
}
