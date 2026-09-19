using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Enum value definition for field extraction.
/// </summary>
public class CcaiFieldEnumValue
{
    /// <summary>
    /// Enum value.
    /// </summary>
    [JsonPropertyName("enumValue")]
    public required string EnumValue { get; set; }

    /// <summary>
    /// Enum description.
    /// </summary>
    [JsonPropertyName("desc")]
    public required string Desc { get; set; }
}
