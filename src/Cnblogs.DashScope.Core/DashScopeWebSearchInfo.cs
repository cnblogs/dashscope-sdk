namespace Cnblogs.DashScope.Core;

/// <summary>
/// Web search information.
/// </summary>
/// <param name="SearchResults">Web search results.</param>
/// <param name="ExtraToolInfo">Extra tool infos when <see cref="SearchOptions.EnableSearchExtension"/> is true.</param>
public record DashScopeWebSearchInfo(
    List<DashScopeWebSearchResult> SearchResults,
    List<DashScopeWebSearchExtra>? ExtraToolInfo);
