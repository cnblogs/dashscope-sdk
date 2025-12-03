namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Web search information.
    /// </summary>
    /// <param name="SearchResults">Web search results.</param>
    /// <param name="ExtraToolInfo">Extra tool infos when <see cref="TextGenerationSearchOptions.EnableSearchExtension"/> is true.</param>
    public record TextGenerationWebSearchInfo(
        List<TextGenerationWebSearchResult> SearchResults,
        List<TextGenerationWebSearchExtra>? ExtraToolInfo);
}
