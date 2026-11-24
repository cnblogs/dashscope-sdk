namespace Cnblogs.DashScope.Core;

/// <summary>
/// One embedding of a multimodal embedding request.
/// </summary>
/// <param name="Index">The position of the content in <see cref="MultimodalEmbeddingInput.Contents"/>, 0 for the vector fused from all the contents.</param>
/// <param name="Embedding">The vector, in the dimension of the model or of <see cref="MultimodalEmbeddingParameters.Dimension"/>.</param>
/// <param name="Type">What the vector was computed from: <c>text</c>, <c>image</c>, <c>video</c> or <c>multi_images</c> for a content; <c>fused</c> for the members of one content fused by the 2026-03-06 snapshots; <c>fusion</c> for all the contents fused by qwen3-vl-embedding with <see cref="MultimodalEmbeddingParameters.EnableFusion"/>; <c>vl</c> for the vectors of qwen3-vl-embedding and qwen2.5-vl-embedding, including the one qwen2.5-vl-embedding fuses from all the contents.</param>
public record MultimodalEmbeddingItem(int Index, float[] Embedding, string Type);
