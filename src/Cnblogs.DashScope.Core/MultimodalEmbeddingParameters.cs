namespace Cnblogs.DashScope.Core;

/// <summary>
/// The optional parameters for multimodal embedding. Which members take effect depends on the model.
/// </summary>
public class MultimodalEmbeddingParameters
{
    /// <summary>
    /// The format of the vectors, only <c>dense</c> is supported at present.
    /// </summary>
    public string? OutputType { get; set; }

    /// <summary>
    /// The dimension of the vectors, the default of the model when unset: 2560 (or 2048, 1536,
    /// 1024, 768, 512, 256) for qwen3-vl-embedding, 1024 (or 2048, 768, 512) for
    /// qwen2.5-vl-embedding, 1152 (or 1024, 512, 256, 128, 64) for tongyi-embedding-vision-plus
    /// and its 2026-03-06 snapshot, 768 (or 512, 256, 128, 64) for tongyi-embedding-vision-flash
    /// and its 2026-03-06 snapshot. multimodal-embedding-v1 always returns 1024 dimensions.
    /// </summary>
    public int? Dimension { get; set; }

    /// <summary>
    /// The ratio of the video frames to read, in [0, 1], defaults to 1. No effect was observed on
    /// the current models: the vectors and the usage stay the same whatever the value.
    /// </summary>
    public float? Fps { get; set; }

    /// <summary>
    /// The task instruction that guides the model to understand the query intent, e.g.
    /// <c>Given a user query, retrieve the most relevant image</c>. English is recommended.
    /// qwen3-vl-embedding and qwen2.5-vl-embedding only, it is counted into
    /// <see cref="MultimodalEmbeddingUsage.InputTokens"/>.
    /// </summary>
    public string? Instruct { get; set; }

    /// <summary>
    /// Whether to fuse all the contents into one vector instead of one vector per content,
    /// defaults to false. qwen3-vl-embedding only, ignored by the other models.
    /// </summary>
    public bool? EnableFusion { get; set; }

    /// <summary>
    /// The resolution level of the images, 0, 1, 2 or 3 for 127, 402, 578 or 1026 tokens per
    /// image, defaults to 1. The 2026-03-06 snapshots only, ignored by the other models.
    /// </summary>
    public int? ResLevel { get; set; }

    /// <summary>
    /// The maximum number of the frames sampled from a video, at most 64, defaults to 8. The
    /// 2026-03-06 snapshots only, ignored by the other models.
    /// </summary>
    public int? MaxVideoFrames { get; set; }
}
