namespace Cnblogs.DashScope.Core;

/// <summary>
/// The token usage of a live translation response.
/// </summary>
public class LiveTranslateUsage
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
    public LiveTranslateTokenDetails? InputTokensDetails { get; set; }

    /// <summary>
    /// The output token details.
    /// </summary>
    public LiveTranslateTokenDetails? OutputTokensDetails { get; set; }
}
