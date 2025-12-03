namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// The website reference that deep research model learned from search.
    /// </summary>
    /// <param name="Title">The title of the website page.</param>
    /// <param name="Description">The description of the website ref.</param>
    /// <param name="Url">The url of the website.</param>
    /// <param name="Favicon">The favicon of the website.</param>
    public record DashScopeDeepResearchWebsiteRef(string Title, string Description, string Url, string Favicon);
}
