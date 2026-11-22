namespace Cnblogs.DashScope.Core;

/// <summary>
/// The usage of a speech synthesis request of qwen-audio-tts models.
/// </summary>
public class SpeechSynthesisUsage
{
    /// <summary>
    /// The accumulated count of billed characters.
    /// </summary>
    public int Characters { get; set; }

    /// <summary>
    /// The count of input tokens. qwen-audio-3.1-tts models only, which are billed by
    /// tokens instead of characters.
    /// </summary>
    public int? InputTokens { get; set; }

    /// <summary>
    /// The count of output tokens. qwen-audio-3.1-tts models only.
    /// </summary>
    public int? OutputTokens { get; set; }

    /// <summary>
    /// The count of total tokens. qwen-audio-3.1-tts models only.
    /// </summary>
    public int? TotalTokens { get; set; }
}
