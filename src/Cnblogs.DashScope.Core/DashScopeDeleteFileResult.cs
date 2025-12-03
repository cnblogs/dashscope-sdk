namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Result of a delete file action.
    /// </summary>
    /// <param name="Object">Always be "file".</param>
    /// <param name="Deleted">Deletion result.</param>
    /// <param name="Id">Deleting file's id.</param>
    public record DashScopeDeleteFileResult(string Object, bool Deleted, DashScopeFileId Id);
}
