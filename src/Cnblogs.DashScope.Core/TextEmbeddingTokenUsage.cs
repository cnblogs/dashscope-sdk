namespace Cnblogs.DashScope.Core;

/// <summary>
/// The token usage of text embedding request.
/// </summary>
/// <param name="TotalTokens">The total token number of input texts.</param>
/// <param name="PromptTokens">The prompt token number, same value as <see cref="TotalTokens"/>. Only returned by qwen3.7-text-embedding-flash.</param>
/// <param name="InputTokens">The input token number, same value as <see cref="TotalTokens"/>. Only returned by qwen3.7-text-embedding.</param>
/// <param name="PromptTokensDetails">The prompt token details. Only returned by qwen3.7-text-embedding-flash.</param>
public record TextEmbeddingTokenUsage(
    int TotalTokens,
    int? PromptTokens = null,
    int? InputTokens = null,
    TextEmbeddingPromptTokenDetails? PromptTokensDetails = null);
