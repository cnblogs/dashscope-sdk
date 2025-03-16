using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Sdk.UnitTests.Utils;

public record TestApplicationBizParam(
    [property: JsonPropertyName("sourceCode")]
    string SourceCode);
