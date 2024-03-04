namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// The optional parameters for text embedding.
/// </summary>
public class TextEmbeddingParameters
{
    /// <summary>
    /// The text type("query" or "document"). Defaults to "document".
    /// </summary>
    public string? TextType { get; set; }
}
