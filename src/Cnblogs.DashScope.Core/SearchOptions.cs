namespace Cnblogs.DashScope.Core;

/// <summary>
/// Web search options
/// </summary>
public class SearchOptions
{
    /// <summary>
    /// Show search result in response. Defaults to false.
    /// </summary>
    public bool? EnableSource { get; set; }

    /// <summary>
    /// Include citation in output. Defaults to false.
    /// </summary>
    public bool? EnableCitation { get; set; }

    /// <summary>
    /// Citation format. Defaults to "[&lt;number&gt;]"
    /// </summary>
    public string? CitationFormat { get; set; }

    /// <summary>
    /// Force model to use web search. Defaults to false.
    /// </summary>
    public bool? ForcedSearch { get; set; }

    /// <summary>
    /// How many search records should be provided to model. "turbo" or "max".
    /// </summary>
    public string? SearchStrategy { get; set; }

    /// <summary>
    /// Enhanced search for specific areas.
    /// </summary>
    public bool? EnableSearchExtension { get; set; }

    /// <summary>
    /// Return the search result first when using incremental output.
    /// </summary>
    public bool? PrependSearchResult { get; set; }

    /// <summary>
    /// Only search content within given days. Available options: 7, 30, 180, 365
    /// </summary>
    public int? Freshness { get; set; }

    /// <summary>
    /// Limit websites that should search within.
    /// </summary>
    public List<string>? AssignedSiteList { get; set; }

    /// <summary>
    /// Optional prompt for the web search process.
    /// </summary>
    public IntentionOptions? IntentionOptions { get; set; }
}
