using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample;

public abstract class ImageSynthesisSample : ISample
{
    /// <inheritdoc />
    public string Group => "ImageSynthesis";

    /// <inheritdoc />
    public abstract string Description { get; }

    /// <inheritdoc />
    public abstract Task RunAsync(IDashScopeClient client);
}
