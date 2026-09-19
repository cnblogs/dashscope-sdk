using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample;

public abstract class SpeechSample : ISample
{
    /// <inheritdoc />
    public string Group => "Speech";

    /// <inheritdoc />
    public abstract string Description { get; }

    /// <inheritdoc />
    public abstract Task RunAsync(IDashScopeClient client);
}
