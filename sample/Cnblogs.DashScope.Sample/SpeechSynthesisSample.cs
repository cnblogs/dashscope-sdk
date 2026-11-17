using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample;

public abstract class SpeechSynthesisSample : ISample
{
    /// <inheritdoc />
    public string Group => "SpeechSynthesis";

    /// <inheritdoc />
    public abstract string Description { get; }

    /// <inheritdoc />
    public abstract Task RunAsync(IDashScopeClient client);
}
