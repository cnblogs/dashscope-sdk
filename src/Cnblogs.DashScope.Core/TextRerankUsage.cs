namespace Cnblogs.DashScope.Core;

/// <summary>
/// The token usage of a text rerank request.
/// </summary>
/// <param name="TotalTokens">The total token count of the request.</param>
/// <param name="PromptTokens">The prompt token count, same value as <see cref="TotalTokens"/>. Only returned by qwen3.7-text-rerank.</param>
/// <param name="ImageTokens">The token count of the images and videos. Only returned by qwen3-vl-rerank.</param>
/// <param name="InputTokens">The token count of the texts. Only returned by qwen3-vl-rerank.</param>
public record TextRerankUsage(
    int TotalTokens,
    int? PromptTokens = null,
    int? ImageTokens = null,
    int? InputTokens = null);
