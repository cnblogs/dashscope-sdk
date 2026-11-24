namespace Cnblogs.DashScope.Core;

/// <summary>
/// The token usage of a multimodal embedding request. Which members are returned depends on the model.
/// </summary>
/// <param name="InputTokens">The token count of the texts for multimodal-embedding-v1, qwen3-vl-embedding and qwen2.5-vl-embedding, of all the contents for the tongyi-embedding-vision models.</param>
/// <param name="TotalTokens">The total token count. Not returned by qwen2.5-vl-embedding and multimodal-embedding-v1.</param>
/// <param name="OutputTokens">The output token count, one per vector. Only returned by the tongyi-embedding-vision models.</param>
/// <param name="ImageTokens">The token count of the images and videos. Not returned by the tongyi-embedding-vision models, which break their <see cref="InputTokens"/> down in <see cref="InputTokensDetails"/> instead.</param>
/// <param name="ImageCount">The count of the images. Only returned by multimodal-embedding-v1.</param>
/// <param name="Duration">The duration of the videos in seconds. Only returned by multimodal-embedding-v1.</param>
/// <param name="InputTokensDetails">The breakdown of <see cref="InputTokens"/>. Only returned by the tongyi-embedding-vision models.</param>
public record MultimodalEmbeddingUsage(
    int InputTokens,
    int? TotalTokens = null,
    int? OutputTokens = null,
    int? ImageTokens = null,
    int? ImageCount = null,
    double? Duration = null,
    MultimodalEmbeddingInputTokenDetails? InputTokensDetails = null);
