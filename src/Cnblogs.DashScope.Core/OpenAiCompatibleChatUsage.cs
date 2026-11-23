namespace Cnblogs.DashScope.Core;

/// <summary>
/// The token usage of an OpenAI compatible chat completion.
/// </summary>
public class OpenAiCompatibleChatUsage
{
    /// <summary>
    /// The input token count.
    /// </summary>
    public int? PromptTokens { get; set; }

    /// <summary>
    /// The output token count.
    /// </summary>
    public int? CompletionTokens { get; set; }

    /// <summary>
    /// The total token count.
    /// </summary>
    public int? TotalTokens { get; set; }

    /// <summary>
    /// The input token details.
    /// </summary>
    public OpenAiCompatibleChatTokenDetails? PromptTokensDetails { get; set; }

    /// <summary>
    /// The output token details.
    /// </summary>
    public OpenAiCompatibleChatTokenDetails? CompletionTokensDetails { get; set; }
}
