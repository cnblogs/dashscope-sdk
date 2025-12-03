namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Represents a reference for deep search answer.
    /// </summary>
    /// <param name="Icon">The icon of the reference.</param>
    /// <param name="Description">The description of the reference.</param>
    /// <param name="IndexNumber">Index number of the reference.</param>
    /// <param name="Title">Title of the reference.</param>
    /// <param name="Url">The url of the reference.</param>
    public record DashScopeDeepResearchReference(string? Icon, string? Description, int IndexNumber, string Title, string Url);
}
