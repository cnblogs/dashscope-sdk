namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// The inputs for text embedding api
    /// </summary>
    public class TextEmbeddingInput
    {
        /// <summary>
        /// The texts to be computed.
        /// </summary>
        public IEnumerable<string> Texts { get; set; } = Array.Empty<string>();
    }
}
