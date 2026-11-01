namespace Cnblogs.DashScope.Sdk.TextEmbedding;

/// <summary>
/// Supported models for embedding.
/// </summary>
public enum TextEmbeddingModel
{
    /// <summary>
    /// text-embedding-v1
    /// </summary>
    TextEmbeddingV1 = 1,

    /// <summary>
    /// text-embedding-v2
    /// </summary>
    TextEmbeddingV2 = 2,

    /// <summary>
    /// text-embedding-v3
    /// </summary>
    TextEmbeddingV3 = 3,

    /// <summary>
    /// text-embedding-v4
    /// </summary>
    TextEmbeddingV4 = 4,

    /// <summary>
    /// qwen3.7-text-embedding, supports 2560/2048/1536/1024(default)/768/512/256 dimensions,
    /// up to 20 texts per request and 128,000 tokens per text.
    /// </summary>
    QwenTextEmbedding = 5,

    /// <summary>
    /// qwen3.7-text-embedding-flash, supports 1024(default)/768/512/256 dimensions.
    /// </summary>
    QwenTextEmbeddingFlash = 6
}
