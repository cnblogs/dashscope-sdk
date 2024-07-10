using System.Text.Json.Nodes;

namespace Cnblogs.DashScope.Sdk.UnitTests.Utils;

public static class Checkers
{
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
