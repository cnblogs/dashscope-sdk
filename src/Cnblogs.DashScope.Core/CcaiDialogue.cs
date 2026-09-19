using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Dialogue payload for LingQue CCAI Conversation Analysis AIO.
/// </summary>
public class CcaiDialogue
{
    /// <summary>
    /// Optional session id.
    /// </summary>
    [JsonPropertyName("sessionId")]
    public string? SessionId { get; set; }

    /// <summary>
    /// Ordered dialogue turns.
    /// </summary>
    [JsonPropertyName("sentences")]
    public required List<CcaiSentence> Sentences { get; set; }
}
