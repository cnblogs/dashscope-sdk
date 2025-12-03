namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Represents the fetch result of an asynchronous task.
    /// </summary>
    /// <param name="RequestId">The unique id of this query request.</param>
    /// <param name="Output">The task output.</param>
    /// <param name="Usage">The task usage.</param>
    /// <typeparam name="TOutput">The type of task output.</typeparam>
    /// <typeparam name="TUsage">The type of task usage.</typeparam>
    public record DashScopeTask<TOutput, TUsage>(string RequestId, TOutput Output, TUsage? Usage = null)
        where TUsage : class;
}
