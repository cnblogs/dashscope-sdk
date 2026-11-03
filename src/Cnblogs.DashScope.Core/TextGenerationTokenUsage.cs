namespace Cnblogs.DashScope.Core;

/// <summary>
/// The token usage per request.
/// </summary>
public class TextGenerationTokenUsage
{
    /// <summary>
    /// The number of input token.
    /// </summary>
    /// <remarks>This number may larger than input if internet search is enabled.</remarks>
    public int InputTokens { get; set; }

    /// <summary>
    /// Prompt token details, including cache details.
    /// </summary>
    public TextGenerationPromptTokenDetails? PromptTokensDetails { get; set; }

    /// <summary>
    /// Output token details.
    /// </summary>
    public TextGenerationOutputTokenDetails? OutputTokensDetails { get; set; }

    /// <summary>
    /// Usages of plugins.
    /// </summary>
    public DashScopePluginUsages? Plugins { get; set; }

    /// <summary>
    /// The number of output token.
    /// </summary>
    public int OutputTokens { get; set; }

    /// <summary>
    /// The total number of token.
    /// </summary>
    public int TotalTokens { get; set; }

    /// <summary>
    /// Cached token count.
    /// </summary>
    /// <remarks>
    /// The documented position of this field is <see cref="PromptTokensDetails"/>.CachedTokens;
    /// the flat form is an undocumented variant still returned by some responses (e.g. qwen-doc-turbo
    /// with doc urls) and kept for compatibility. Prefer <see cref="PromptTokensDetails"/> when
    /// reading cached token counts.
    /// </remarks>
    public int? CachedTokens { get; set; }
}
