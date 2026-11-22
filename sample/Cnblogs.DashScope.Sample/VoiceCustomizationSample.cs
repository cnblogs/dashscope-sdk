using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample;

public abstract class VoiceCustomizationSample : ISample
{
    /// <inheritdoc />
    public string Group => "VoiceCustomization";

    /// <inheritdoc />
    public abstract string Description { get; }

    /// <inheritdoc />
    public abstract Task RunAsync(IDashScopeClient client);
}
