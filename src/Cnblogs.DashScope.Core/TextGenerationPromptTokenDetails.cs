namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Token usage details.
    /// </summary>
    /// <param name="CachedTokens">Token count of cached input.</param>
    public record TextGenerationPromptTokenDetails(int CachedTokens);
}
