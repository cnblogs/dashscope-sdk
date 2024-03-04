namespace Cnblogs.DashScope.Sample;

public static class SampleTypeDescriptor
{
    public static string GetDescription(this SampleType sampleType)
    {
        return sampleType switch
        {
            SampleType.TextCompletion => "Simple prompt completion",
            SampleType.TextCompletionSse => "Simple prompt completion with incremental output",
            SampleType.ChatCompletion => "Conversation between user and assistant",
            _ => throw new ArgumentOutOfRangeException(nameof(sampleType), sampleType, "Unsupported sample option")
        };
    }
}
