using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Few-shot example for custom instructions.
/// </summary>
public class CcaiExample
{
    /// <summary>
    /// Example dialogue turns.
    /// </summary>
    [JsonPropertyName("sentences")]
    public required List<CcaiSentence> Sentences { get; set; }

    /// <summary>
    /// Expected output for the example.
    /// </summary>
    [JsonPropertyName("output")]
    public required string Output { get; set; }
}
