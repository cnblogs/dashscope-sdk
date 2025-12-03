namespace Cnblogs.DashScope.Core
{
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
    }
}
