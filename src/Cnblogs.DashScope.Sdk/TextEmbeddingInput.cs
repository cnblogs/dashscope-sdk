namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// The inputs for text embedding api
/// </summary>
public class TextEmbeddingInput
{
    /// <summary>
    /// The texts to be computed.
    /// </summary>
    public required IEnumerable<string> Texts { get; set; }
}
