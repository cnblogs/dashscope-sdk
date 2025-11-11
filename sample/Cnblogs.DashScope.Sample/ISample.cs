using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample;

public interface ISample
{
    string Description { get; }
    Task RunAsync(IDashScopeClient client);
}
