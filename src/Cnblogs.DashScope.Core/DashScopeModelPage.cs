namespace Cnblogs.DashScope.Core;

/// <summary>
/// The page data of a model list response.
/// </summary>
/// <param name="Total">The number of models matching the filters.</param>
/// <param name="PageNo">The page index, starting from 1.</param>
/// <param name="PageSize">The page size of the request.</param>
/// <param name="Models">The models of the page, empty when nothing matches or the page is beyond the last one.</param>
public record DashScopeModelPage(int Total, int PageNo, int PageSize, List<DashScopeModel> Models);
