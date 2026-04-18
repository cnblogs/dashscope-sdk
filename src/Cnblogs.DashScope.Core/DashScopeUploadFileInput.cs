namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents one file input.
/// </summary>
/// <param name="FileStream">File data.</param>
/// <param name="FileName">Name of the file.</param>
/// <param name="Description">Description of the file.</param>
public record DashScopeUploadFileInput(Stream FileStream, string FileName, string? Description = null);
