using System.Net;
using System.Net.Http.Headers;
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

    public async Task<MultipartMemoryStreamProvider> GetRequestFormAsync(bool sse)
    {
        var body = GetRequestBody(sse);
        var stringContent = new StringContent(body);
        stringContent.Headers.ContentType = MediaTypeHeaderValue.Parse($"multipart/form-data; boundary={Boundary}");
        stringContent.Headers.ContentLength = body.Length;
        var provider = await stringContent.ReadAsMultipartAsync();
        return provider;
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
