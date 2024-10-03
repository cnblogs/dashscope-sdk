namespace Cnblogs.DashScope.Core;

/// <summary>
/// Optional parameter of batch get embeddings request.
/// </summary>
public class BatchGetEmbeddingsParameters : IBatchGetEmbeddingsParameters
{
    /// <summary>
    /// Text type of input(ignored by v3). Use <see cref="TextTypes"/> to get available options. Defaults to 'document'.
    /// </summary>
    public string? TextType { get; set; }

    /// <inheritdoc />
    public int? Dimension { get; set; }

    /// <inheritdoc />
    public string? OutputType { get; set; }
}
