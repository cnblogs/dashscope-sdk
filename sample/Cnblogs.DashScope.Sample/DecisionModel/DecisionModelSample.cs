using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.DecisionModel;

public abstract class DecisionModelSample : ISample
{
    /// <inheritdoc />
    public string Group => "DecisionModel";

    /// <inheritdoc />
    public abstract string Description { get; }

    /// <inheritdoc />
    public abstract Task RunAsync(IDashScopeClient client);
}
