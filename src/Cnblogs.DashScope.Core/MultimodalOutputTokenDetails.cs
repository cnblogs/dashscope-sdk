namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Token details of multimodal outputs.
    /// </summary>
    /// <param name="ReasoningTokens">Token count of reasoning output.</param>
    /// <param name="TextTokens">Token count of text output.</param>
    public record MultimodalOutputTokenDetails(int? ReasoningTokens = null, int? TextTokens = null);
}
