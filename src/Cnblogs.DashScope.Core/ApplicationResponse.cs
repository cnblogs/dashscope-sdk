namespace Cnblogs.DashScope.Core;

/// <summary>
/// Response of application call.
/// </summary>
/// <param name="RequestId">Unique id of this request.</param>
/// <param name="Code">Error code. Null when request is successful.</param>
/// <param name="Message">Error message. Null when request is successful.</param>
/// <param name="Output">The output of application call.</param>
/// <param name="Usage">Token usage of this application call.</param>
public record ApplicationResponse(
    string RequestId,
    string? Code,
    string? Message,
    ApplicationOutput Output,
    ApplicationUsage Usage);
