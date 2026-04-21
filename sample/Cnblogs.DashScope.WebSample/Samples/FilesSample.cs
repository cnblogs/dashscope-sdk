using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.WebSample.Samples;

public abstract class FilesSample : ISample
{
    /// <inheritdoc />
    public string Group => "Files";

    /// <inheritdoc />
    public abstract string Description { get; }

    /// <inheritdoc />
    public abstract Task RunAsync(IDashScopeClient client);
}
