namespace Cnblogs.DashScope.Core;

/// <summary>
/// The output of a multimodal embedding request.
/// </summary>
/// <param name="Embeddings">The embeddings, one per content in the order of the contents, or the single vector fused from all the contents.</param>
public record MultimodalEmbeddingOutput(List<MultimodalEmbeddingItem> Embeddings);
