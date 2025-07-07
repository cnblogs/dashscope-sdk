namespace Cnblogs.DashScope.Core;

/// <summary>
/// Web search information.
/// </summary>
/// <param name="SearchResults">Web search results.</param>
public record TextGenerationWebSearchInfo(List<TextGenerationWebSearchResult> SearchResults);
