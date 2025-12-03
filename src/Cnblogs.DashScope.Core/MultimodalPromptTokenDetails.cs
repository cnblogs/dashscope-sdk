namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Prompt token details, including cache info.
    /// </summary>
    /// <param name="CachedTokens">Token count that been cached.</param>
    public record MultimodalPromptTokenDetails(int? CachedTokens = null);
}
