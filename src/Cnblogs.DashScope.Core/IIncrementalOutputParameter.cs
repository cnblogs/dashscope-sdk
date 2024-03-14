namespace Cnblogs.DashScope.Core;

/// <summary>
/// Marks parameter accepts incremental output.
/// </summary>
public interface IIncrementalOutputParameter
{
    /// <summary>
    /// Enable stream output. Defaults to false.
    /// </summary>
    public bool? IncrementalOutput { get; }
}
