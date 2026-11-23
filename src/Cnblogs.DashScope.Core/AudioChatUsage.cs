namespace Cnblogs.DashScope.Core;

/// <summary>
/// The token usage of an audio chat response, returned by response.done events with a
/// completed status.
/// </summary>
public class AudioChatUsage
{
    /// <summary>
    /// The total token count.
    /// </summary>
    public int? TotalTokens { get; set; }

    /// <summary>
    /// The input token count.
    /// </summary>
    public int? InputTokens { get; set; }

    /// <summary>
    /// The output token count.
    /// </summary>
    public int? OutputTokens { get; set; }

    /// <summary>
    /// The input token details.
    /// </summary>
    public AudioChatTokenDetails? InputTokensDetails { get; set; }

    /// <summary>
    /// The output token details.
    /// </summary>
    public AudioChatTokenDetails? OutputTokensDetails { get; set; }

    /// <summary>
    /// The plugin usage, present when the web search is enabled.
    /// </summary>
    public AudioChatPluginUsage? Plugins { get; set; }
}
