namespace Cnblogs.DashScope.Core;

/// <summary>
/// The optional parameters for text embedding.
/// </summary>
public interface ITextEmbeddingParameters
{
    /// <summary>
    /// The text type("query" or "document"). Defaults to "document".
    /// </summary>
    public string? TextType { get; }

    /// <summary>
    /// The dimension of output vector(v3 only), possible values: 1024, 768, 512.
    /// </summary>
    public int? Dimension { get; set; }

    /// <summary>
    /// Dense or sparse of output vector(v3 only), possible values are: sparse, dense, dense&amp;sparse.
    /// </summary>
    public string? OutputType { get; set; }

    /// <summary>
    /// Custom task description that guides the model to understand the query intent. Writing in English usually brings about 1%-5% improvement.
    /// Only effective when <see cref="TextType"/> is "query", and it is counted into token usage for each input text.
    /// </summary>
    public string? Instruct { get; set; }
}
