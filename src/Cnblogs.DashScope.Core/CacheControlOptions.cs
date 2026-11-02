namespace Cnblogs.DashScope.Core;

/// <summary>
/// Explicit context cache marker on a content block, the model caches the prefix
/// from the start of the messages array to the content block that carries it.
/// </summary>
public class CacheControlOptions
{
    /// <summary>
    /// The cache type, no need to change, defaults to "ephemeral".
    /// </summary>
    public string Type { get; set; } = "ephemeral";
}
