namespace Cnblogs.DashScope.Core;

/// <summary>
/// Token usage details.
/// </summary>
/// <param name="CachedTokens">Token count of cached input.</param>
/// <param name="CacheCreation">Explicit cache creation details, only present when explicit cache is used.</param>
/// <param name="CacheCreationInputTokens">Token count used to create the explicit cache, only present when explicit cache is used.</param>
/// <param name="CacheType">The cache type, <c>ephemeral</c> when explicit cache is used, otherwise not present.</param>
public record TextGenerationPromptTokenDetails(
    int CachedTokens,
    CacheCreationDetails? CacheCreation = null,
    int? CacheCreationInputTokens = null,
    string? CacheType = null);
