using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents status of DashScope task.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<DashScopeTaskStatus>))]
public enum DashScopeTaskStatus
{
    /// <summary>
    /// Task not running yet.
    /// </summary>
    Pending = 1,

    /// <summary>
    /// Task is running.
    /// </summary>
    Running = 2,

    /// <summary>
    /// Task has succeeded.
    /// </summary>
    /// <remarks>If task contains multiple subtask, then this means at lease one subtask has been succeeded.</remarks>
    Succeeded = 3,

    /// <summary>
    /// Task has failed.
    /// </summary>
    Failed = 4,

    /// <summary>
    /// Task status unknown, usually because the task is not exits.
    /// </summary>
    Unknown = 5
}
