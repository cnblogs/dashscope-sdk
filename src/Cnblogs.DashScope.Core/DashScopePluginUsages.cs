namespace Cnblogs.DashScope.Core;

/// <summary>
/// Plugin usages.
/// </summary>
public class DashScopePluginUsages
{
    /// <summary>
    /// Plugin usages.
    /// </summary>
    /// <param name="search">Usage of search plugin.</param>
    /// <param name="imageSearch">Usage of image search plugin.</param>
    /// <param name="codeInterpreter">Usage of code interpreter plugin.</param>
    public DashScopePluginUsages(
        DashScopeSearchPluginUsage? search = null,
        DashScopeImageSearchPluginUsage? imageSearch = null,
        DashScopeCodeInterpreterPluginUsage? codeInterpreter = null)
    {
        Search = search;
        ImageSearch = imageSearch;
        CodeInterpreter = codeInterpreter;
    }

    /// <summary>Usage of search plugin.</summary>
    public DashScopeSearchPluginUsage? Search { get; set; }

    /// <summary>
    /// Image search plugin usage.
    /// </summary>
    public DashScopeImageSearchPluginUsage? ImageSearch { get; set; }

    /// <summary>
    /// Usage of code interpreter plugin.
    /// </summary>
    public DashScopeCodeInterpreterPluginUsage? CodeInterpreter { get; set; }
}
