namespace Cnblogs.DashScope.Core;

/// <summary>
/// Token usages of code interpreter plugin.
/// </summary>
public class TextGenerationCodeInterpreterPluginUsage
{
    /// <summary>
    /// Initialize a <see cref="TextGenerationCodeInterpreterPluginUsage"/> with count.
    /// </summary>
    /// <param name="count">Usage count.</param>
    public TextGenerationCodeInterpreterPluginUsage(int count)
    {
        Count = count;
    }

    /// <summary>
    /// Token usage count.
    /// </summary>
    public int Count { get; set; }
}
