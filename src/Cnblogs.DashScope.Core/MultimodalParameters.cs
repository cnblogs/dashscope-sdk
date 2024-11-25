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
    public float? Temperature { get; set; }

    /// <inheritdoc />
    public ulong? Seed { get; set; }

    /// <inheritdoc />
    public bool? IncrementalOutput { get; set; }

    /// <inheritdoc />
    public bool? VlHighResolutionImages { get; set; }

    /// <inheritdoc />
    public float? RepetitionPenalty { get; set; }

    /// <inheritdoc />
    public float? PresencePenalty { get; set; }

    /// <inheritdoc />
    public int? MaxTokens { get; set; }

    /// <inheritdoc />
    public TextGenerationStop? Stop { get; set; }
}
