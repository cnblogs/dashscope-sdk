using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Tripo3D;

public abstract class Tripo3DSample : ISample
{
    /// <inheritdoc />
    public string Group => "Tripo3D";

    /// <inheritdoc />
    public abstract string Description { get; }

    /// <inheritdoc />
    public abstract Task RunAsync(IDashScopeClient client);

    protected static async Task<DashScopeTask<Tripo3DOutput, Tripo3DUsage>> WaitTaskAsync(
        IDashScopeClient client,
        string taskId)
    {
        while (true)
        {
            var task = await client.GetTripo3DTaskAsync(taskId);
            if (task.Output.TaskStatus == DashScopeTaskStatus.Succeeded)
            {
                Console.WriteLine($"Task succeeded, credits consumed: {task.Usage?.CreditsConsumed}");
                return task;
            }

            if (task.Output.TaskStatus is DashScopeTaskStatus.Failed or DashScopeTaskStatus.Canceled
                or DashScopeTaskStatus.Unknown)
            {
                throw new InvalidOperationException(
                    $"Task ended in {task.Output.TaskStatus}: {task.Output.Code} {task.Output.Message}");
            }

            Console.WriteLine($"Task status: {task.Output.TaskStatus}");
            await Task.Delay(TimeSpan.FromSeconds(15));
        }
    }
}
