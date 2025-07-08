namespace Cnblogs.DashScope.Core;

/// <summary>
/// Web search options
/// </summary>
public class TextGenerationSearchOptions
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
    /// How many search records should be provided to model. "standard" - 5 records. "pro" - 10 records.
    /// </summary>
    public string? SearchStrategy { get; set; }
}
