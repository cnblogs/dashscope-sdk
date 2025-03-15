namespace Cnblogs.DashScope.Core;

/// <summary>
/// Request body for an application all.
/// </summary>
public class ApplicationRequest
{
    /// <summary>
    /// Content of this call.
    /// </summary>
    public required ApplicationInput Input { get; init; }

    /// <summary>
    /// Optional configurations.
    /// </summary>
    public required ApplicationParameters? Parameters { get; init; }
}
