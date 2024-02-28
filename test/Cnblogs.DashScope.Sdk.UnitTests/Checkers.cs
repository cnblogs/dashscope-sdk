using System.Text.Json.Nodes;

namespace Cnblogs.DashScope.Sdk.UnitTests;

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
}
