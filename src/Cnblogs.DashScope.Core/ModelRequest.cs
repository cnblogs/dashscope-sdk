using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Represents a request for model generation.
    /// </summary>
    /// <typeparam name="TInput">The input type for this request.</typeparam>
    public class ModelRequest<TInput>
        where TInput : class
    {
        /// <summary>
        /// The model to use.
        /// </summary>
        public string Model { get; init; } = string.Empty;

        /// <summary>
        /// Input of this request.
        /// </summary>
        public TInput Input { get; init; } = null!;
    }

    /// <summary>
    /// Represents a request for model generation.
    /// </summary>
    /// <typeparam name="TInput">The input type for this request.</typeparam>
    /// <typeparam name="TParameter">The option type for this request.</typeparam>
    public class ModelRequest<TInput, TParameter> : ModelRequest<TInput>, IDashScopeOssUploadConfig
        where TInput : class
        where TParameter : class
    {
        /// <summary>
        /// Optional configuration of this request.
        /// </summary>
        public TParameter? Parameters { get; set; }

        /// <inheritdoc />
        public bool EnableOssResolve() => Input is IDashScopeOssUploadConfig config && config.EnableOssResolve();
    }
}
