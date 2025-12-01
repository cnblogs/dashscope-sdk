using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample;

public abstract class TextSample : ISample
{
    /// <inheritdoc />
    public string Group => "Text";

    /// <inheritdoc />
    public abstract string Description { get; }

    /// <inheritdoc />
    public abstract Task RunAsync(IDashScopeClient client);
}
