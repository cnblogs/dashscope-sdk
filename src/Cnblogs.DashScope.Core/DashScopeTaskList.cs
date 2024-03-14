namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents one page of DashScope task list.
/// </summary>
/// <param name="RequestId">The unique id of this request.</param>
/// <param name="Data">The query result data.</param>
/// <param name="TotalPage">Total number of pages.</param>
/// <param name="Total">Total number of items.</param>
/// <param name="PageNo">Page index of this request.</param>
/// <param name="PageSize">Page size of this request.</param>
public record DashScopeTaskList(
    string RequestId,
    DashScopeTaskListItem[] Data,
    int TotalPage,
    int Total,
    int PageNo,
    int PageSize);
