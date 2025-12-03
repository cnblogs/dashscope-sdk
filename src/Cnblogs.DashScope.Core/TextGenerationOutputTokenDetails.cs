namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Output details for text generation api.
    /// </summary>
    /// <param name="ReasoningTokens">Token count of reasoning content.</param>
    public record TextGenerationOutputTokenDetails(int ReasoningTokens);
}
