using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// The metrics of one DashScope task.
/// </summary>
/// <param name="Total">The total number of subtasks.</param>
/// <param name="Succeeded">The number of succeeded subtasks.</param>
/// <param name="Failed">The number of failed subtasks.</param>
public record DashScopeTaskMetrics(
    [property: JsonPropertyName("TOTAL")] int Total,
    [property: JsonPropertyName("SUCCEEDED")]
    int Succeeded,
    [property: JsonPropertyName("FAILED")]
    int Failed);
