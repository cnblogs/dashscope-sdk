namespace Cnblogs.DashScope.Core;

/// <summary>
/// Parameters for web search.
/// </summary>
public interface IWebSearchParameter
{
    /// <summary>
    /// Enable internet search when generation. Defaults to false.
    /// </summary>
    bool? EnableSearch { get; }

    /// <summary>
    /// Include &lt;img&gt; tags in the response.
    /// </summary>
    bool? EnableTextImageMixed { get; }

    /// <summary>
    /// Search options. <see cref="EnableSearch"/> should set to true.
    /// </summary>
    SearchOptions? SearchOptions { get; }
}
