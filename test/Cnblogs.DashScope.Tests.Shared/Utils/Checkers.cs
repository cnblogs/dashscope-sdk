using System.Text.Json.Nodes;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static class Checkers
{
    public static bool IsJsonEquivalent(ArraySegment<byte> socketBuffer, string requestSnapshot)
    {
        var actual = JsonNode.Parse(socketBuffer);
        var expected = JsonNode.Parse(requestSnapshot);
        return JsonNode.DeepEquals(actual, expected);
    }

    public static bool CheckFormContent(HttpRequestMessage message, ICollection<HttpContent> contents)
    {
        if (message.Content is not MultipartContent formContent)
        {
            return false;
        }

        foreach (var httpContent in formContent)
        {
            // check field name
            var fieldName = httpContent.Headers.ContentDisposition!.Name?.Trim('"');
            var expectedField = contents.FirstOrDefault(f => f.Headers.ContentDisposition!.Name == fieldName);
            if (expectedField is null)
            {
                return false;
            }

            if (httpContent is not StringContent)
            {
                continue;
            }

#pragma warning disable VSTHRD002
            var stringEqual = expectedField.ReadAsStringAsync().Result == httpContent.ReadAsStringAsync().Result;
#pragma warning restore VSTHRD002
            if (stringEqual == false)
            {
                return false;
            }
        }

        return true;
    }

    public static bool IsJsonEquivalent(HttpContent content, string requestSnapshot)
    {
#pragma warning disable VSTHRD002
        var actual = JsonNode.Parse(content.ReadAsStringAsync().Result);
#pragma warning restore VSTHRD002
        var expected = JsonNode.Parse(requestSnapshot);
        return JsonNode.DeepEquals(actual, expected);
    }

    public static bool IsFileUploaded(HttpContent? content, params string[] files)
    {
        if (content is not MultipartFormDataContent form)
        {
            return false;
        }

        if (form.Count(x => x.GetType() == typeof(StreamContent)) != files.Length)
        {
            return false;
        }

        return true;
    }
}
