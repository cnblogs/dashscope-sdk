namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// Represents one result of text embedding request.
/// </summary>
/// <param name="TextIndex">The correspond text's index in input array.</param>
/// <param name="Embedding">The resulting embedding.</param>
public record TextEmbeddingItem(int TextIndex, float[] Embedding);
