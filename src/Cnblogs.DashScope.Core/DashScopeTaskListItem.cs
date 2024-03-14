namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents one list item of dash scope task list.
/// </summary>
/// <param name="ApiKeyId">The id of api key.</param>
/// <param name="CallerParentId">The aliyun parent uid of task creator.</param>
/// <param name="CallerUid">The aliyun uid of task creator.</param>
/// <param name="GmtCreate">The timestamp of task creation.</param>
/// <param name="StartTime">The timestamp of task start.</param>
/// <param name="EndTime">The timestamp of task end.</param>
/// <param name="Region">The region of this task belongs.</param>
/// <param name="RequestId">The request id this task created by</param>
/// <param name="Status">Status of this task.</param>
/// <param name="TaskId">Id of this task.</param>
/// <param name="UserApiUniqueKey">Unique key of model api information.</param>
/// <param name="ModelName">The model name of this task using.</param>
public record DashScopeTaskListItem(
    string ApiKeyId,
    string CallerParentId,
    string CallerUid,
    long GmtCreate,
    long? StartTime,
    long? EndTime,
    string Region,
    string RequestId,
    DashScopeTaskStatus Status,
    string TaskId,
    string UserApiUniqueKey,
    string ModelName);
