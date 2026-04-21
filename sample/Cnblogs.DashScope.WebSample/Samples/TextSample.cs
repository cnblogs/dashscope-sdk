using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.WebSample.Resources;

namespace Cnblogs.DashScope.WebSample.Samples;

public abstract class TextSample : ISample
{
    /// <inheritdoc />
    public string Group => Home.Group_Text;

    /// <inheritdoc />
    public abstract string Description { get; }

    /// <inheritdoc />
    public abstract Task RunAsync(IDashScopeClient client);
}
