namespace Cnblogs.DashScope.Core;

/// <summary>
/// The token usage of text embedding request.
/// </summary>
/// <param name="TotalTokens">The token number of input texts.</param>
public record TextEmbeddingTokenUsage(int TotalTokens);
