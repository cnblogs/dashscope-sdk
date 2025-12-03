namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Usage of the search plugin.
    /// </summary>
    /// <param name="Count">Usage count.</param>
    /// <param name="Strategy">Search strategy.</param>
    public record TextGenerationSearchPluginUsage(int Count, string Strategy);
}
