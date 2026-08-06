namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents one element of a sparse embedding.
/// </summary>
/// <param name="Index">The index of the token in the vocabulary.</param>
/// <param name="Token">The text of the token.</param>
/// <param name="Value">The weight or importance score of the token in the input text.</param>
public record SparseEmbeddingItem(int Index, string Token, float Value);
