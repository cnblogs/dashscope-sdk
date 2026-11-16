namespace Cnblogs.DashScope.Core;

/// <summary>
/// Available values for <see cref="SearchOptions.SearchStrategy"/>.
/// </summary>
/// <remarks>
/// Legacy values "standard"/"pro" (billed as <see cref="Turbo"/>) and "pro_max"/"pro_ultra" (billed as <see cref="Max"/>) are no longer recommended.
/// </remarks>
public static class SearchStrategies
{
    /// <summary>
    /// Balance search quality and response speed, this is the default value.
    /// </summary>
    public const string Turbo = "turbo";

    /// <summary>
    /// Search with multiple search engines for more comprehensive results, at the cost of longer response time.
    /// </summary>
    public const string Max = "max";

    /// <summary>
    /// Perform multiple rounds of search and integrate results with the model, extra charge applies.
    /// Only available on part of the Qwen3.5-3.8 series models. When enabled, only <see cref="SearchOptions.EnableSource"/>
    /// is supported (and must be set to true), and streaming is required when using the text generation endpoint.
    /// </summary>
    public const string Agent = "agent";
}
