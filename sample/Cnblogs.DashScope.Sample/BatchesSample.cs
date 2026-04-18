using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample;

public abstract class BatchesSample : ISample
{
    /// <inheritdoc />
    public string Group => "Batches";

    /// <inheritdoc />
    public abstract string Description { get; }

    /// <inheritdoc />
    public abstract Task RunAsync(IDashScopeClient client);
}
