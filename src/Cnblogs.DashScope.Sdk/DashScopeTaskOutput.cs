using System.Text.Json.Serialization;
using Cnblogs.DashScope.Sdk.Internals;

namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// The common properties of DashScope task.
/// </summary>
public abstract record DashScopeTaskOutput
{
    /// <summary>
    /// The unique id of this task.
    /// </summary>
    public required string TaskId { get; set; }

    /// <summary>
    /// The status of this task.
    /// </summary>
    public DashScopeTaskStatus TaskStatus { get; set; }

    /// <summary>
    /// Submit time of this task.
    /// </summary>
    [JsonConverter(typeof(DashScopeDateTimeConvertor))]
    public DateTime? SubmitTime { get; set; }

    /// <summary>
    /// Scheduled(Started) time of this task.
    /// </summary>
    [JsonConverter(typeof(DashScopeDateTimeConvertor))]
    public DateTime? ScheduledTime { get; set; }

    /// <summary>
    /// End time of this task.
    /// </summary>
    [JsonConverter(typeof(DashScopeDateTimeConvertor))]
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// The metrics of subtasks.
    /// </summary>
    public DashScopeTaskMetrics? TaskMetrics { get; set; }

    /// <summary>
    /// Error code, not null when <see cref="TaskStatus"/> is Failed.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Error message, not null when <see cref="TaskStatus"/> is Failed.
    /// </summary>
    public string? Message { get; set; }
}
