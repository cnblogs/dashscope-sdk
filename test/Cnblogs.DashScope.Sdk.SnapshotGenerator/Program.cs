using System.Net;
using System.Text;

const string basePath = "../../../../Cnblogs.DashScope.Sdk.UnitTests/RawHttpData";
var snapshots = new DirectoryInfo(basePath);
Console.WriteLine("Reading key from environment variable DASHSCOPE_KEY");
var apiKey = Environment.GetEnvironmentVariable("DASHSCOPE_API_KEY");
if (string.IsNullOrEmpty(apiKey))
{
    Console.Write("ApiKey > ");
    apiKey = Console.ReadLine();
}

var handler = new SocketsHttpHandler { AutomaticDecompression = DecompressionMethods.All, };
var client = new HttpClient(handler) { BaseAddress = new Uri("https://dashscope.aliyuncs.com/api/v1/") };
client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

while (true)
{
    Console.Write("Snapshot Name > ");
    var snapshotName = Console.ReadLine()?.Trim();
    if (string.IsNullOrEmpty(snapshotName))
    {
        continue;
    }

    var snapshot = snapshots.EnumerateFiles().Where(s => s.Name.StartsWith(snapshotName))
        .Select(s => s.Name.Split('.').First()).Distinct()
        .ToList();
    if (snapshot.Count == 0)
    {
        Console.WriteLine($"No snapshot was found with name: {snapshotName}");
    }

    Console.WriteLine($"Updating {snapshot.Count} snapshots ...");
    foreach (var name in snapshot)
    {
        Console.WriteLine($"Updating {name}");
        await UpdateSnapshotsAsync(client, name);
        Console.WriteLine($"{name} updated");
    }
}

static async Task UpdateSnapshotsAsync(HttpClient client, string name)
{
    var requestHeader = await File.ReadAllLinesAsync(Path.Combine(basePath, $"{name}.request.header.txt"));
    var requestBodyFile = Path.Combine(basePath, $"{name}.request.body.json");
    var requestBody = File.Exists(requestBodyFile)
        ? await File.ReadAllTextAsync(Path.Combine(basePath, $"{name}.request.body.json"))
        : string.Empty;
    var firstLine = requestHeader[0].Split(' ');
    var method = HttpMethod.Parse(firstLine[0]);
    var request = new HttpRequestMessage(method, firstLine[1]);
    var contentType = "application/json";
    foreach (var header in requestHeader.Skip(1))
    {
        if (string.IsNullOrWhiteSpace(header))
        {
            continue;
        }

        var values = header.Split(':', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (values[0] == "Content-Type")
        {
            contentType = values[1];
            continue;
        }

        if (values[0] == "Content-Length")
        {
            continue;
        }

        request.Headers.Add(values[0], values[1]);
    }

    if (string.IsNullOrWhiteSpace(requestBodyFile) == false)
    {
        request.Content = new StringContent(requestBody, Encoding.Default, contentType);
    }

    var response = await client.SendAsync(request);
    var responseBody = await response.Content.ReadAsStringAsync();
    var responseHeaderFile = new StringBuilder();
    responseHeaderFile.AppendLine($"HTTP/1.1 {(int)response.StatusCode} {response.StatusCode}");
    responseHeaderFile = response.Headers.Aggregate(
        responseHeaderFile,
        (sb, pair) => sb.AppendLine($"{pair.Key}: {string.Join(',', pair.Value)}"));
    await File.WriteAllTextAsync(Path.Combine(basePath, $"{name}.response.header.txt"), responseHeaderFile.ToString());
    await File.WriteAllTextAsync(Path.Combine(basePath, $"{name}.response.body.txt"), responseBody);
}
