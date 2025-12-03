namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Represent data of oss temp file upload grant.
    /// </summary>
    /// <param name="Policy">Upload policy.</param>
    /// <param name="Signature">Upload signature.</param>
    /// <param name="UploadDir">Directory that granted to upload.</param>
    /// <param name="UploadHost">Hostname that upload to.</param>
    /// <param name="ExpireInSeconds">Grant's expiration.</param>
    /// <param name="MaxFileSizeMb">Maximum size of file.</param>
    /// <param name="CapacityLimitMb">Total upload limit of account.</param>
    /// <param name="OssAccessKeyId">Key used to upload.</param>
    /// <param name="XOssObjectAcl">Access of the uploaded file.</param>
    /// <param name="XOssForbidOverwrite">Can file be overwritten by another file with same name.</param>
    public record DashScopeTemporaryUploadPolicyData(
        string Policy,
        string Signature,
        string UploadDir,
        string UploadHost,
        int ExpireInSeconds,
        int MaxFileSizeMb,
        int CapacityLimitMb,
        string OssAccessKeyId,
        string XOssObjectAcl,
        string XOssForbidOverwrite);
}
