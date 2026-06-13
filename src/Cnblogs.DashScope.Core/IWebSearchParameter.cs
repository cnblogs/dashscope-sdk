namespace Cnblogs.DashScope.Core;

/// <summary>
/// Parameters for web search.
/// </summary>
public interface IWebSearchParameter
{
    /// <summary>
    /// Enable internet search when generation. Defaults to false.
    /// </summary>
    bool? EnableSearch { get; set; }

    /// <summary>
    /// Include &lt;img&gt; tags in the response.
    /// </summary>
    bool? EnableTextImageMixed { get; set; }

    /// <summary>
    /// Search options. <see cref="EnableSearch"/> should set to true.
    /// </summary>
    SearchOptions? SearchOptions { get; set; }
}
