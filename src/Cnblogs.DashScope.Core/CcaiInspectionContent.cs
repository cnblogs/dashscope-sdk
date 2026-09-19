using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// A single service inspection dimension.
/// </summary>
public class CcaiInspectionContent
{
    /// <summary>
    /// Dimension title.
    /// </summary>
    [JsonPropertyName("title")]
    public required string Title { get; set; }

    /// <summary>
    /// Dimension description.
    /// </summary>
    [JsonPropertyName("content")]
    public required string Content { get; set; }
}
