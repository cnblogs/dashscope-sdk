namespace Cnblogs.DashScope.Core;

/// <summary>
/// The token usage data of multimodal generation request.
/// </summary>
public class MultimodalTokenUsage
{
    /// <summary>
    /// The token usage of model output.
    /// </summary>
    public int OutputTokens { get; set; }

    /// <summary>
    /// The total token usage of model input.
    /// </summary>
    public int InputTokens { get; set; }

    /// <summary>
    /// The token usage of input image.
    /// </summary>
    public int? ImageTokens { get; set; }

    /// <summary>
    /// The token usage of input audio.
    /// </summary>
    public int? AudioTokens { get; set; }

    /// <summary>
    /// The token usage of input video.
    /// </summary>
    public int? VideoTokens { get; set; }

    /// <summary>
    /// Count of cached tokens.
    /// </summary>
    public int? CachedTokens { get; set; }

    /// <summary>
    /// Count of total tokens.
    /// </summary>
    public int? TotalTokens { get; set; }

    /// <summary>
    /// The details of input token usage.
    /// </summary>
    public MultimodalInputTokenDetails? InputTokensDetails { get; set; }

    /// <summary>
    /// The details of output token usage.
    /// </summary>
    public MultimodalOutputTokenDetails? OutputTokensDetails { get; set; }

    /// <summary>
    /// The details of prompt token usage. (e.g. cached tokens)
    /// </summary>
    public MultimodalPromptTokenDetails? PromptTokensDetails { get; set; }
}
