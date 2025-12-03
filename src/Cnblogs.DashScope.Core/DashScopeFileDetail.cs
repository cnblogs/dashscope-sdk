namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Detail info of a DashScope file.
    /// </summary>
    /// <param name="FileId">ID of the file, can be used in model request.</param>
    /// <param name="Name">Name of the file.</param>
    /// <param name="Description">Description of the file.</param>
    /// <param name="Size">File size in byte.</param>
    /// <param name="Md5">File's MD5.</param>
    /// <param name="GmtCreate">Upload time.</param>
    /// <param name="Url">Download link of the file.</param>
    /// <param name="UserId">Uploader's ID.</param>
    /// <param name="Region">Region of the file.</param>
    /// <param name="ApiKeyId">ID of the api key which uploaded this file.</param>
    /// <param name="Id">Internal ID of the file.</param>
    public record DashScopeFileDetail(
        DashScopeFileId FileId,
        string Name,
        string Description,
        int Size,
        string Md5,
        string GmtCreate,
        string Url,
        string? UserId,
        string? Region,
        string? ApiKeyId,
        int? Id);
}
