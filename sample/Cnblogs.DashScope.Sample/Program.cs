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
    .GroupBy(x => x.Group)
    .ToDictionary(x => x.Key, x => x.ToList());

var flatten = new List<ISample>();
Console.WriteLine("Choose the sample you want to run:");
foreach (var samplesKey in samples.Keys)
{
    Console.WriteLine(samplesKey);
    var samplesInGroup = samples[samplesKey];
    foreach (var sample in samplesInGroup)
    {
        flatten.Add(sample);
        Console.WriteLine($"{flatten.Count}. {sample.Description}");
    }
}

Console.WriteLine();
Console.Write("Choose an option: ");
var parsed = int.TryParse(Console.ReadLine()?.Trim(), out var index);
if (parsed == false)
{
    Console.WriteLine("Invalid choice");
    return;
}

await flatten[index - 1].RunAsync(dashScopeClient);
