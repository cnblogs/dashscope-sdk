namespace Cnblogs.DashScope.Core;

/// <summary>
/// The output of a text rerank request.
/// </summary>
/// <param name="Results">The ranked documents, sorted by <see cref="TextRerankResult.RelevanceScore"/> descending.</param>
public record TextRerankOutput(List<TextRerankResult> Results);
