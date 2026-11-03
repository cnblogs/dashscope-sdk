namespace Cnblogs.DashScope.Core;

/// <summary>
/// Output details for text generation api.
/// </summary>
/// <param name="ReasoningTokens">Token count of reasoning content.</param>
/// <param name="TextTokens">Token count of output text.</param>
public record TextGenerationOutputTokenDetails(
    int ReasoningTokens,
    int? TextTokens = null);
