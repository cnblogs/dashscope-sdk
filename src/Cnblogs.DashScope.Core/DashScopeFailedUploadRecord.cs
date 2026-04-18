namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents one failed upload record.
/// </summary>
/// <param name="Name">File name.</param>
/// <param name="Code">Error code.</param>
/// <param name="Message">Error message.</param>
public record DashScopeFailedUploadRecord(string Name, string Code, string Message);
