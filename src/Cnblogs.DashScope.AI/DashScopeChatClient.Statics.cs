using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.AI;

public sealed partial class DashScopeChatClient
{
    private static readonly TextGenerationParameters
        DefaultTextGenerationParameter = new() { ResultFormat = "message" };

    /// <summary>
    /// Model prefixes that support <c>response_format</c> of type <c>json_schema</c>, see
    /// https://docs.bailian.console.aliyun.com/zh/model-studio/qwen-structured-output for the full list.
    /// </summary>
    private static readonly string[] JsonSchemaModelPrefixes =
    [
        "qwen3.7-flash", "qwen3.7-plus", "qwen3.7-max", "qwen3.8-flash", "qwen3.8-max"
    ];
}
