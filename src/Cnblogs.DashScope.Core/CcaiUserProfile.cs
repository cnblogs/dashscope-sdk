using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// User profile field definition.
/// </summary>
public class CcaiUserProfile
{
    /// <summary>
    /// Profile field name.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Profile field description / value instruction.
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; set; }
}
