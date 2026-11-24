using System.Text.Json.Nodes;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static class Checkers
{
    public static bool IsJsonEquivalent(ArraySegment<byte> socketBuffer, string requestSnapshot)
    {
        var actual = JsonNode.Parse(socketBuffer);
        var expected = JsonNode.Parse(requestSnapshot);
        return JsonNode.DeepEquals(actual, expected);
    }

    public static JsonNode GetSentMessage(FakeClientWebSocket server, int index)
    {
        return JsonNode.Parse(System.Text.Encoding.UTF8.GetString(server.ServerReceivedMessages[index].ToArray()))!;
    }

    public static ArraySegment<byte> NormalizeEventId(
        FakeClientWebSocket server,
        int index,
        string snapshotJson)
    {
        var sentJson = System.Text.Encoding.UTF8.GetString(server.ServerReceivedMessages[index].ToArray());
        var sentId = JsonNode.Parse(sentJson)!["event_id"]!.GetValue<string>();
        var fixtureId = JsonNode.Parse(snapshotJson)!["event_id"]!.GetValue<string>();
        return new ArraySegment<byte>(System.Text.Encoding.UTF8.GetBytes(sentJson.Replace(sentId, fixtureId)));
    }

    public static bool CheckFormContent(HttpRequestMessage message, ICollection<HttpContent> contents)
    {
        var content = message.Content;
        if (content is ThrottledContent throttled)
        {
            content = throttled.InnerContent;
        }

        if (content is not MultipartContent formContent)
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
}
