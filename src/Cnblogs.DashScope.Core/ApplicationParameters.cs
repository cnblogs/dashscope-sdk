namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Parameters for application call.
    /// </summary>
    public class ApplicationParameters : IIncrementalOutputParameter, ISeedParameter, IProbabilityParameter
    {
        /// <inheritdoc />
        public bool? IncrementalOutput { get; set; }

        /// <summary>
        /// Output format for flow application. Can be <c>full_thoughts</c> or <c>agent_format</c>. Defaults to <c>full_thoughts</c>.
        /// </summary>
        public string? FlowStreamMode { get; set; }

        /// <summary>
        /// Options for RAG applications.
        /// </summary>
        public ApplicationRagOptions? RagOptions { get; set; }

        /// <inheritdoc />
        public ulong? Seed { get; set; }

        /// <inheritdoc />
        public float? TopP { get; set; }

        /// <inheritdoc />
        public int? TopK { get; set; }

        /// <inheritdoc />
        public float? Temperature { get; set; }

        /// <summary>
        /// Controls whether output contains think block.
        /// </summary>
        public bool? HasThoughts { get; set; }
    }
}
