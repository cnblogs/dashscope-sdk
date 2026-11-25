using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.UniTranslation;

public abstract class UniTranslationSample : ISample
{
    /// <inheritdoc />
    public string Group => "UniTranslation";

    /// <inheritdoc />
    public abstract string Description { get; }

    /// <inheritdoc />
    public abstract Task RunAsync(IDashScopeClient client);
}
