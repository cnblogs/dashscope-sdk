namespace Cnblogs.DashScope.Core;

/// <summary>
/// The token details of an OpenAI compatible chat completion usage. Which members are
/// present depends on the direction: the prompt details carry audio_tokens / video_tokens /
/// cached_tokens (text_tokens is 0), the completion details carry audio_tokens /
/// text_tokens.
/// </summary>
public class OpenAiCompatibleChatTokenDetails
{
    /// <summary>
    /// The token count of the audio.
    /// </summary>
    public int? AudioTokens { get; set; }

    /// <summary>
    /// The token count of the text.
    /// </summary>
    public int? TextTokens { get; set; }

    /// <summary>
    /// The token count of the video. Prompt details only.
    /// </summary>
    public int? VideoTokens { get; set; }

    /// <summary>
    /// The token count served from the cache. Prompt details only.
    /// </summary>
    public int? CachedTokens { get; set; }

    /// <summary>
    /// The token count of the reasoning. Completion details only, null so far.
    /// </summary>
    public int? ReasoningTokens { get; set; }
}
