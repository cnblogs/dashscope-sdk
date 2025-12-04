namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represent one page of file list.
/// </summary>
/// <param name="Total">Total count of files.</param>
/// <param name="PageNo">Page index.</param>
/// <param name="PageSize">Page size.</param>
/// <param name="Files">File item list.</param>
public record DashScopeListFilesData(int Total, int PageNo, int PageSize, List<DashScopeFileDetail> Files);
