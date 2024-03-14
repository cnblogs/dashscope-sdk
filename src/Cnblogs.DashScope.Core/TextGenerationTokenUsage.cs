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
    /// The number of output token.
    /// </summary>
    public int OutputTokens { get; set; }

    /// <summary>
    /// The total number of token.
    /// </summary>
    public int TotalTokens { get; set; }
}
