namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Response of application call.
    /// </summary>
    /// <param name="RequestId">Unique id of this request.</param>
    /// <param name="Output">The output of application call.</param>
    /// <param name="Usage">Token usage of this application call.</param>
    public record ApplicationResponse(
        string RequestId,
        ApplicationOutput Output,
        ApplicationUsage Usage);
}
