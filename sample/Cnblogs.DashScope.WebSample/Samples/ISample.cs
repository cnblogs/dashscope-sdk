using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.WebSample.Samples;

public interface ISample
{
    string Group { get; }
    string Description { get; }
    Task RunAsync(IDashScopeClient client);
}
