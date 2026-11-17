using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.VideoSynthesis;

public abstract class VideoSynthesisSample : ISample
{
    /// <inheritdoc />
    public string Group => "VideoSynthesis";

    /// <inheritdoc />
    public abstract string Description { get; }

    /// <inheritdoc />
    public abstract Task RunAsync(IDashScopeClient client);
}
