namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// Optional parameter of batch get embeddings request.
/// </summary>
public class BatchGetEmbeddingsParameters
{
    /// <summary>
    /// Text type of input. Use <see cref="TextTypes"/> to get available options. Defaults to 'document'.
    /// </summary>
    public string? TextType { get; set; }
}
