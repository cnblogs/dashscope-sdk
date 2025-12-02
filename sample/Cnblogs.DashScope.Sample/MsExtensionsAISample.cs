using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample;

public abstract class MsExtensionsAISample : ISample
{
    /// <inheritdoc />
    public string Group => "Microsoft.Extensions.AI";

    /// <inheritdoc />
    public abstract string Description { get; }

    /// <inheritdoc />
    public abstract Task RunAsync(IDashScopeClient client);
}
