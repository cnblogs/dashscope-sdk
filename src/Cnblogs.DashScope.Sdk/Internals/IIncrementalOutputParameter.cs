namespace Cnblogs.DashScope.Sdk.Internals;

internal interface IIncrementalOutputParameter
{
    /// <summary>
    /// Enable stream output. Defaults to false.
    /// </summary>
    public bool? IncrementalOutput { get; }
}
