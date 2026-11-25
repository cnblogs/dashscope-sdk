using Xunit;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static class WaitUtils
{
    // Polls instead of sleeping a fixed time: whatever the condition waits for is done by another
    // thread, whose continuations go through the thread pool and the xunit worker queue, and each
    // hop can take tens of milliseconds when the machine is busy.
    public static async Task UntilAsync(Func<bool> condition, int timeoutMilliseconds = 30_000)
    {
        var deadline = Environment.TickCount64 + timeoutMilliseconds;
        while (condition() == false && Environment.TickCount64 < deadline)
        {
            await Task.Delay(10);
        }

        Assert.True(condition(), $"The condition was not met within {timeoutMilliseconds}ms.");
    }
}
