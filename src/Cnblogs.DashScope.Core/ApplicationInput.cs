namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Inputs for application call.
    /// </summary>
    /// <typeparam name="TBizParams">Type of the BizContent.</typeparam>
    public class ApplicationInput<TBizParams>
        where TBizParams : class
    {
        /// <summary>
        /// The prompt for model to generate response upon. Optional when <see cref="Messages"/> has been set.
        /// </summary>
        /// <remarks>
        /// Prompt will be appended to <see cref="Messages"/> when both set.
        /// </remarks>
        public string? Prompt { get; set; }

        /// <summary>
        /// The session id for conversation history. This will be ignored if <see cref="Messages"/> has been set.
        /// </summary>
        public string? SessionId { get; set; }

        /// <summary>
        /// The conversation history.
        /// </summary>
        public IEnumerable<ApplicationMessage>? Messages { get; set; }

        /// <summary>
        /// The id of memory when enabled.
        /// </summary>
        public string? MemoryId { get; set; }

        /// <summary>
        /// List of image urls for inputs.
        /// </summary>
        public IEnumerable<string>? ImageList { get; set; }

        /// <summary>
        /// User defined content.
        /// </summary>
        public TBizParams? BizParams { get; set; } = null;
    }

    /// <summary>
    /// Inputs for application call.
    /// </summary>
    public class ApplicationInput : ApplicationInput<Dictionary<string, object?>>
    {
    }
}
