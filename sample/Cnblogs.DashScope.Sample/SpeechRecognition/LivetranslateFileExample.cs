using System.Text;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.SpeechRecognition;

public class LivetranslateFileExample : SpeechRecognitionSample
{
    /// <inheritdoc />
    public override string Description
        => "Non-realtime audio file translation with qwen3-livetranslate-flash (OpenAI compatible endpoint)";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        Console.WriteLine("Translating the audio file...");
        const string audioUrl =
            "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250211/tixcef/cherry.wav";

        // the qwen3-livetranslate-flash models are only served on the OpenAI compatible
        // endpoint and only in the streaming mode; prefer the DashScope methods for the
        // models that support both styles
        var chunks = client.OpenAiCompatibleGetChatCompletionStreamAsync(
            new OpenAiCompatibleChatRequest
            {
                Model = "qwen3-livetranslate-flash",
                Messages =
                [
                    new OpenAiCompatibleChatMessage
                    {
                        Role = "user",
                        Content =
                        [
                            new OpenAiCompatibleChatContentPart(
                                Type: "input_audio",
                                InputAudio: new OpenAiCompatibleChatInputAudio(Data: audioUrl, Format: "wav"))
                        ]
                    }
                ],
                Modalities = ["text", "audio"],
                Audio = new OpenAiCompatibleChatAudioOptions(Voice: "Cherry", Format: "wav"),
                StreamOptions = new OpenAiCompatibleChatStreamOptions(IncludeUsage: true),
                TranslationOptions = new OpenAiCompatibleTranslationOptions(SourceLang: "zh", TargetLang: "en")
            });

        var text = new StringBuilder();
        await using var audio = File.Create("livetranslate.wav");
        OpenAiCompatibleChatUsage? usage = null;
        await foreach (var chunk in chunks)
        {
            usage = chunk.Usage ?? usage; // present in the last chunk (include_usage)
            var delta = chunk.Choices.FirstOrDefault()?.Delta;
            if (delta?.Content is { Length: > 0 })
            {
                Console.Write(delta.Content);
                text.Append(delta.Content);
            }

            if (delta?.Audio?.Data is { Length: > 0 } data)
            {
                // the audio deltas are base64 wav chunks
                await audio.WriteAsync(Convert.FromBase64String(data));
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Translated ({text.Length} chars), usage: {usage?.TotalTokens} tokens, audio saved to livetranslate.wav");
    }
}
