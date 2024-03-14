namespace Cnblogs.DashScope.Core;

/// <summary>
/// The optional parameters for text embedding.
/// </summary>
public interface ITextEmbeddingParameters
{
    /// <summary>
    /// The text type("query" or "document"). Defaults to "document".
    /// </summary>
    public string? TextType { get; }
}
