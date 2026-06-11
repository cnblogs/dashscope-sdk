using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Generation task metric.
/// </summary>
/// <param name="Failed">Failed count.</param>
/// <param name="Succeeded">Succeed count.</param>
/// <param name="Total">Total count.</param>
public record MultimodalTaskMetric(
    [property: JsonPropertyName("FAILED")] int Failed,
    [property: JsonPropertyName("SUCCEEDED")]
    int Succeeded,
    [property: JsonPropertyName("TOTAL")] int Total);
