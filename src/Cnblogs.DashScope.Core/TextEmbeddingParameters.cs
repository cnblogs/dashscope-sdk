namespace Cnblogs.DashScope.Core;

/// <summary>
/// The optional parameters for text embedding.
/// </summary>
public class TextEmbeddingParameters : ITextEmbeddingParameters
{
    /// <inheritdoc />
    public string? TextType { get; set; }
}
