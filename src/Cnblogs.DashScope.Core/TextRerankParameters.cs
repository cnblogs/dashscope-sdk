namespace Cnblogs.DashScope.Core;

/// <summary>
/// The optional parameters for text rerank. Which members take effect depends on the model.
/// </summary>
public class TextRerankParameters
{
    /// <summary>
    /// The number of top ranked documents to return. All documents are returned when unset or
    /// when the value exceeds the document count.
    /// </summary>
    public int? TopN { get; set; }

    /// <summary>
    /// Whether to echo each returned document in <see cref="TextRerankResult.Document"/>,
    /// defaults to false to save the network traffic.
    /// </summary>
    public bool? ReturnDocuments { get; set; }

    /// <summary>
    /// The task instruction that guides the ranking strategy, e.g.
    /// <c>Given a web search query, retrieve relevant passages that answer the query.</c> (the
    /// default, which favors the documents answering the query) or <c>Retrieve semantically
    /// similar text.</c> (which favors the documents with the same meaning). English is
    /// recommended. qwen3.7-text-rerank, qwen3-rerank and qwen3-vl-rerank only, ignored by
    /// gte-rerank-v2.
    /// </summary>
    public string? Instruct { get; set; }

    /// <summary>
    /// The ratio of the video frames to read, in [0, 1], the smaller the fewer frames are read.
    /// Defaults to 1. qwen3-vl-rerank only.
    /// </summary>
    public float? Fps { get; set; }
}
