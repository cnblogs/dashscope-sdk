namespace Cnblogs.DashScope.Core;

/// <summary>
/// Token usages of code interpreter plugin.
/// </summary>
public class DashScopeCodeInterpreterPluginUsage
{
    /// <summary>
    /// Initialize a <see cref="DashScopeCodeInterpreterPluginUsage"/> with count.
    /// </summary>
    /// <param name="count">Usage count.</param>
    public DashScopeCodeInterpreterPluginUsage(int count)
    {
        Count = count;
    }

    /// <summary>
    /// Token usage count.
    /// </summary>
    public int Count { get; set; }
}
