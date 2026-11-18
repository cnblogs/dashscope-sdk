using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample;

public abstract class SpeechRecognitionSample : ISample
{
    /// <inheritdoc />
    public string Group => "SpeechRecognition";

    /// <inheritdoc />
    public abstract string Description { get; }

    /// <inheritdoc />
    public abstract Task RunAsync(IDashScopeClient client);
}
