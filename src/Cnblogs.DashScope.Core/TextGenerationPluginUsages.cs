namespace Cnblogs.DashScope.Core;

/// <summary>
/// Plugin usages.
/// </summary>
public class TextGenerationPluginUsages
{
    /// <summary>
    /// Plugin usages.
    /// </summary>
    /// <param name="search">Usage of search plugin.</param>
    /// <param name="codeInterpreter">Usage of code interpreter plugin.</param>
    public TextGenerationPluginUsages(
        TextGenerationSearchPluginUsage? search = null,
        TextGenerationCodeInterpreterPluginUsage? codeInterpreter = null)
    {
        Search = search;
        CodeInterpreter = codeInterpreter;
    }

    /// <summary>Usage of search plugin.</summary>
    public TextGenerationSearchPluginUsage? Search { get; set; }

    /// <summary>
    /// Usage of code interpreter plugin.
    /// </summary>
    public TextGenerationCodeInterpreterPluginUsage? CodeInterpreter { get; set; }
}
