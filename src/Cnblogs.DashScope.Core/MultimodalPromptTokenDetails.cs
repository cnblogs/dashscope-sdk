namespace Cnblogs.DashScope.Core;

/// <summary>
/// Prompt token details, including cache info.
/// </summary>
/// <param name="CachedTokens">Token count that been cached.</param>
/// <param name="CacheCreation">Explicit cache creation details, only present when explicit cache is used.</param>
/// <param name="CacheCreationInputTokens">Token count used to create the explicit cache, only present when explicit cache is used.</param>
/// <param name="CacheType">The cache type, <c>ephemeral</c> when explicit cache is used, otherwise not present.</param>
public record MultimodalPromptTokenDetails(
    int? CachedTokens = null,
    CacheCreationDetails? CacheCreation = null,
    int? CacheCreationInputTokens = null,
    string? CacheType = null);
