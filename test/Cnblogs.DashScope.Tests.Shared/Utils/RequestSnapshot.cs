using System.Net;
using System.Text;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public record RequestSnapshot(string Name)
{
    public string? Boundary { get; set; }

    protected string GetSnapshotCaseName(bool sse) => $"{Name}-{(sse ? "sse" : "nosse")}";

    public string GetRequestJson(bool sse)
    {
        return GetRequestBody(sse, "json");
    }

    public List<HttpContent> GetRequestForm(bool sse)
    {
        var body = GetRequestBody(sse);
        var blocks = body
            .Split($"--{Boundary}", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .SkipLast(1)
            .Select(HttpContent (x) =>
            {
                var lines = x.Split("\r\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var data = new StringBuilder();
                var headers = new Dictionary<string, string>();
                foreach (var line in lines)
                {
                    var colonIndex = line.IndexOf(':');
                    if (colonIndex < 0)
                    {
                        data.Append(line);
                    }
                    else
                    {
                        headers.Add(line[..colonIndex].Trim(), line[(colonIndex + 1)..].Trim());
                    }
                }

                var content = new StringContent(data.ToString());
                foreach (var keyValuePair in headers)
                {
                    content.Headers.TryAddWithoutValidation(keyValuePair.Key, keyValuePair.Value);
                }

                return content;
            })
            .ToList();
        return blocks;
    }

    public string GetRequestBody(bool sse, string ext = "txt")
    {
        return File.ReadAllText(Path.Combine("RawHttpData", $"{GetSnapshotCaseName(sse)}.request.body.{ext}"));
    }
}

public record RequestSnapshot<TResponse>(string Name, TResponse ResponseModel) : RequestSnapshot(Name)
{
    public async Task<HttpResponseMessage> ToResponseMessageAsync(bool sse)
    {
        var responseHeader =
            await File.ReadAllLinesAsync(
                Path.Combine("RawHttpData", $"{GetSnapshotCaseName(sse)}.response.header.txt"));
        var statusCode = int.Parse(responseHeader[0].Split(' ')[1]);
        var message = new HttpResponseMessage
        {
            StatusCode = (HttpStatusCode)statusCode,
            Content = new StringContent(
                await File.ReadAllTextAsync(
                    Path.Combine("RawHttpData", $"{GetSnapshotCaseName(sse)}.response.body.txt")),
                Encoding.UTF8,
                sse ? "text/event-stream" : "application/json")
        };

        return message;
    }
}

public record RequestSnapshot<TRequest, TResponse>(
    string Name,
    TRequest RequestModel,
    TResponse ResponseModel)
    : RequestSnapshot<TResponse>(Name, ResponseModel);
