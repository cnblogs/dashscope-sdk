using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample
{
    public abstract class MultimodalSample : ISample
    {
        /// <inheritdoc />
        public string Group => "Multimodal";

        /// <inheritdoc />
        public abstract string Description { get; }

        /// <inheritdoc />
        public abstract Task RunAsync(IDashScopeClient client);
    }
}
