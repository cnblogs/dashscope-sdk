using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public record TestApplicationBizParam(
    [property: JsonPropertyName("sourceCode")]
    string SourceCode);
