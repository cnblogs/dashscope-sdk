namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// Represents input of batch embedding.
/// </summary>
public class BatchGetEmbeddingsInput
{
    /// <summary>
    /// The url of text file to compute embeddings from.
    /// </summary>
    public required string Url { get; set; }
}
