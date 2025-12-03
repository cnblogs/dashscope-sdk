namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Represents one uploaded file.
    /// </summary>
    /// <param name="FileId">File Id.</param>
    /// <param name="Name">File name.</param>
    public record DashScopeUploadedFileRecord(DashScopeFileId FileId, string Name);
}
