using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// A single dialogue turn for LingQue CCAI Conversation Analysis AIO.
/// </summary>
public class CcaiSentence
{
    /// <summary>
    /// Optional chat id for this turn.
    /// </summary>
    [JsonPropertyName("chatId")]
    public string? ChatId { get; set; }

    /// <summary>
    /// Speaker role: user, agent, or system.
    /// </summary>
    [JsonPropertyName("role")]
    public required string Role { get; set; }

    /// <summary>
    /// Utterance text.
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}
