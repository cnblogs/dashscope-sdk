using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Explicit cache creation details.
/// </summary>
/// <param name="Ephemeral5MInputTokens">Token count used to create an explicit cache with 5 minutes TTL.</param>
public record CacheCreationDetails(
    [property: JsonPropertyName("ephemeral_5m_input_tokens")] int? Ephemeral5MInputTokens = null);
