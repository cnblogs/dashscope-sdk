using System.Text.Json.Serialization;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// The input of the text rerank api, shared by the models on this endpoint (qwen3.7-text-rerank,
/// qwen3-rerank, qwen3-vl-rerank, gte-rerank-v2 and other rerank models).
/// </summary>
[JsonConverter(typeof(TextRerankInputJsonConverter))]
public class TextRerankInput
{
    /// <summary>
    /// The query to rank the documents against, at most 4,000 tokens. A text for every model,
    /// qwen3-vl-rerank also accepts an image to rank the documents against.
    /// </summary>
    public TextRerankContent Query { get; set; } = new();

    /// <summary>
    /// The candidate documents to rank, in the order given: the <see cref="TextRerankResult.Index"/>
    /// of a result is the position in this list. Texts for every model, qwen3-vl-rerank also
    /// accepts images and videos. A text is sent as a plain string as long as every document is
    /// a text (the only form gte-rerank-v2 accepts), and as <c>{"text": ...}</c> otherwise, since
    /// qwen3-vl-rerank rejects a list that mixes strings and objects.
    /// </summary>
    public List<TextRerankContent> Documents { get; set; } = new();
}
