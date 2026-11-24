namespace Cnblogs.DashScope.Core;

/// <summary>
/// The input of the multimodal embedding api, shared by the models on this endpoint
/// (tongyi-embedding-vision-plus, tongyi-embedding-vision-flash and their 2026-03-06 snapshots,
/// multimodal-embedding-v1, qwen3-vl-embedding and qwen2.5-vl-embedding).
/// </summary>
public class MultimodalEmbeddingInput
{
    /// <summary>
    /// The contents to compute the embeddings from, in the order given: the
    /// <see cref="MultimodalEmbeddingItem.Index"/> of an embedding is the position in this list.
    /// Whether a vector comes out of every content or the contents are fused into one vector
    /// depends on the model:
    /// <list type="bullet">
    /// <item>The 2026-03-06 snapshots fuse the members of one content (e.g. its text and image) into one vector: give a content one member for a vector of its own, several members for a fused vector.</item>
    /// <item>qwen3-vl-embedding computes one vector per content, or fuses all the contents into one vector with <see cref="MultimodalEmbeddingParameters.EnableFusion"/>.</item>
    /// <item>qwen2.5-vl-embedding always fuses all the contents into one vector.</item>
    /// <item>The other models compute one vector per content and expect one member in each: with several members the vector is the one of a single modality.</item>
    /// </list>
    /// </summary>
    public List<MultimodalEmbeddingContent> Contents { get; set; } = new();
}
