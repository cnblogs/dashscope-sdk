using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Sample;

namespace Cnblogs.DashScope.Sample.Speech;

public class SpeechRecognitionFlashSample : SpeechSample
{
    /// <inheritdoc />
    public override string Description => "Non-realtime speech recognition flash (Fun-ASR-Flash / Qwen-Audio ASR Flash)";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        Console.Write("Audio URL (public HTTP/HTTPS) > ");
        var audioUrl = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(audioUrl))
        {
            audioUrl = "https://dashscope.oss-cn-beijing.aliyuncs.com/samples/audio/paraformer/hello_world_female2.wav";
            Console.WriteLine($"Using default: {audioUrl}");
        }

        var response = await client.GetSpeechRecognitionAsync(
            new ModelRequest<SpeechRecognitionInput, ISpeechRecognitionParameters>
            {
                Model = "fun-asr-flash-2026-06-15",
                Input = new SpeechRecognitionInput
                {
                    Messages = new[]
                    {
                        SpeechRecognitionMessage.User(
                        [
                            SpeechRecognitionMessageContent.InputAudioContent(audioUrl)
                        ])
                    }
                },
                Parameters = new SpeechRecognitionParameters
                {
                    Format = "wav",
                    SampleRate = "16000",
                    // Fun-ASR-Flash only uses the first language hint.
                    LanguageHints = new[] { "zh" }
                }
            });

        Console.WriteLine($"Text: {response.Output.Text}");
        if (response.Output.Sentence != null)
        {
            Console.WriteLine(
                $"Sentence #{response.Output.Sentence.SentenceId}: {response.Output.Sentence.Text} (end={response.Output.Sentence.SentenceEnd})");
        }

        if (response.Usage != null)
        {
            Console.WriteLine($"Usage duration: {response.Usage.Duration}s");
        }
    }
}
