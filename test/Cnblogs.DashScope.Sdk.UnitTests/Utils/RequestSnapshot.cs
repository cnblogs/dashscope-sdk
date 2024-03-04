using System.Net;
using System.Text;

namespace Cnblogs.DashScope.Sdk.UnitTests.Utils;

public record RequestSnapshot<TResponse>(string Name, TResponse ResponseModel)
{
    protected string GetSnapshotCaseName(bool sse) => $"{Name}-{(sse ? "sse" : "nosse")}";

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
    : RequestSnapshot<TResponse>(Name, ResponseModel)
{
    public string GetRequestJson(bool sse)
    {
        return File.ReadAllText(Path.Combine("RawHttpData", $"{GetSnapshotCaseName(sse)}.request.json"));
    }
}
