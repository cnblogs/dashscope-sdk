namespace Cnblogs.DashScope.Core.Internals
{
    /// <summary>
    /// Indicates the request have configuration for oss resource resolve.
    /// </summary>
    public interface IDashScopeOssUploadConfig
    {
        /// <summary>
        /// Needs resolve oss resource.
        /// </summary>
        public bool EnableOssResolve();
    }
}
