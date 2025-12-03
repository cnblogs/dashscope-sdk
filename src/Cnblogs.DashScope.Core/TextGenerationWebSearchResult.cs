namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Represents one web search record.
    /// </summary>
    /// <param name="SiteName">Source site name.</param>
    /// <param name="Icon">Source site favicon url.</param>
    /// <param name="Index">Serial number of search records.</param>
    /// <param name="Title">Page title.</param>
    /// <param name="Url">Page url.</param>
    public record TextGenerationWebSearchResult(string SiteName, string Icon, int Index, string Title, string Url);
}
