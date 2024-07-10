namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents a DashScope file.
/// </summary>
/// <param name="Id">Id of the file.</param>
/// <param name="Object">Always be "file".</param>
/// <param name="Bytes">Total bytes of the file.</param>
/// <param name="CreatedAt">Unix timestamp(in seconds) of file create time.</param>
/// <param name="Filename">Name of the file.</param>
/// <param name="Purpose">Purpose of the file.</param>
public record DashScopeFile(
    DashScopeFileId Id,
    string Object,
    int Bytes,
    int CreatedAt,
    string Filename,
    string? Purpose);
