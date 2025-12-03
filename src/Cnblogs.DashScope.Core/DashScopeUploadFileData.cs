namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Represents upload file result data.
    /// </summary>
    /// <param name="UploadedFiles">Successful uploads.</param>
    /// <param name="FailedUploads">Failed uploads.</param>
    public record DashScopeUploadFileData(
        List<DashScopeUploadedFileRecord> UploadedFiles,
        List<DashScopeFailedUploadRecord> FailedUploads);
}
