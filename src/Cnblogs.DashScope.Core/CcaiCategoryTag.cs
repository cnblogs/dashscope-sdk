using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Category tag definition.
/// </summary>
public class CcaiCategoryTag
{
    /// <summary>
    /// Tag name.
    /// </summary>
    [JsonPropertyName("tagName")]
    public string? TagName { get; set; }

    /// <summary>
    /// Tag description.
    /// </summary>
    [JsonPropertyName("tagDesc")]
    public string? TagDesc { get; set; }
}
