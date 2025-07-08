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
            SampleType.ChatCompletionWithTool => "Function call sample",
            SampleType.ChatCompletionWithFiles => "File upload sample using qwen-long",
            SampleType.MultimodalCompletion => "Multimodal completion",
            SampleType.Text2Image => "Text to Image generation",
            SampleType.MicrosoftExtensionsAi => "Use with Microsoft.Extensions.AI",
            SampleType.MicrosoftExtensionsAiToolCall => "Use tool call with Microsoft.Extensions.AI interfaces",
            SampleType.ApplicationCall => "Call pre-defined application",
            SampleType.TextToSpeech => "TTS task",
            SampleType.TextEmbedding => "Get text embedding",
            _ => throw new ArgumentOutOfRangeException(nameof(sampleType), sampleType, "Unsupported sample option")
        };
    }
}
