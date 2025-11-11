using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Sample;
using Cnblogs.DashScope.Sample.Text;

Console.WriteLine("Reading key from environment variable DASHSCOPE_KEY");
var apiKey = Environment.GetEnvironmentVariable("DASHSCOPE_KEY", EnvironmentVariableTarget.Process)
             ?? Environment.GetEnvironmentVariable("DASHSCOPE_KEY", EnvironmentVariableTarget.User);
if (string.IsNullOrEmpty(apiKey))
{
    Console.Write("ApiKey > ");
    apiKey = Console.ReadLine();
}

var dashScopeClient = new DashScopeClient(apiKey!);

var samples = typeof(ChatSample).Assembly.GetTypes()
    .Where(t => t.IsAssignableTo(typeof(ISample)) && t is { IsClass: true, IsAbstract: false })
    .Select(x => Activator.CreateInstance(x) as ISample)
    .Where(x => x != null)
    .Select(x => x!)
    .ToList();

Console.WriteLine("Choose the sample you want to run:");
for (var i = 0; i < samples.Count; i++)
{
    Console.WriteLine($"{i}. {samples[i].Description}");
}

Console.WriteLine();
Console.Write("Choose an option: ");
var parsed = int.TryParse(Console.ReadLine()?.Trim(), out var index);
if (parsed == false)
{
    Console.WriteLine("Invalid choice");
    return;
}

await samples[index].RunAsync(dashScopeClient);
