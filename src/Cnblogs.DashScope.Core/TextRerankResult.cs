namespace Cnblogs.DashScope.Core;

/// <summary>
/// One result of a text rerank request, the results are sorted by the relevance score descending.
/// </summary>
/// <param name="Index">The position of the document in <see cref="TextRerankInput.Documents"/>.</param>
/// <param name="RelevanceScore">The relevance of the document to the query, in [0, 1]. The score is relative to the documents of the same request, do not compare it across requests.</param>
/// <param name="Document">The original document, present only when <see cref="TextRerankParameters.ReturnDocuments"/> is true.</param>
public record TextRerankResult(int Index, double RelevanceScore, TextRerankContent? Document = null);
