namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Represents input of batch embedding.
    /// </summary>
    public class BatchGetEmbeddingsInput
    {
        /// <summary>
        /// The url of text file to compute embeddings from.
        /// </summary>
        public string Url { get; set; } = string.Empty;
    }
}
