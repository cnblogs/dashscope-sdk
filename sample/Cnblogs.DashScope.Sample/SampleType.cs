using System.ComponentModel;

namespace Cnblogs.DashScope.Sample;

public enum SampleType
{
    [Description("Simple prompt completion")]
    TextCompletion,

    [Description("Simple prompt completion with incremental output")]
    TextCompletionSse,

    [Description("Conversation between user and assistant")]
    ChatCompletion,
}
