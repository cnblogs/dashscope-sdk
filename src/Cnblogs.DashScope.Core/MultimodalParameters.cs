namespace Cnblogs.DashScope.Core;

/// <summary>
/// Optional parameters for multi-model generation request.
/// </summary>
public class MultimodalParameters : IMultimodalParameters
{
    /// <inheritdoc />
    public float? TopP { get; set; }

    /// <inheritdoc />
    public int? TopK { get; set; }

    /// <inheritdoc />
    public ulong? Seed { get; set; }

    /// <inheritdoc />
    public bool? IncrementalOutput { get; set; }
}
