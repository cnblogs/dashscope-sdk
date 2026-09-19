using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Field definition for information extraction.
/// </summary>
public class CcaiField
{
    /// <summary>
    /// Optional field code.
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    /// <summary>
    /// Field name.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Field description.
    /// </summary>
    [JsonPropertyName("desc")]
    public required string Desc { get; set; }

    /// <summary>
    /// Optional enum values.
    /// </summary>
    [JsonPropertyName("enumValues")]
    public List<CcaiFieldEnumValue>? EnumValues { get; set; }
}
