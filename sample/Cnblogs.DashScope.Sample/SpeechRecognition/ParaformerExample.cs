using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.SpeechRecognition;

public class ParaformerExample : SpeechRecognitionSample
{
    /// <inheritdoc />
    public override string Description => "Real-time speech recognition with Paraformer";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        Console.WriteLine("Recognizing...");
        using var session = await client.CreateSpeechRecognitionSocketSessionAsync("paraformer-realtime-v2");
        var taskId = await session.RunTaskAsync(
            new SpeechRecognitionParameters
            {
                Format = "wav",
                SampleRate = 16000,
                LanguageHints = ["zh"],
                MaxSentenceSilence = 800
            });

        await using var audio = File.OpenRead("speech.wav");
        await session.SendAudioAsync(audio);
        await session.FinishTaskAsync(taskId);

        var duration = 0;
        await foreach (var message in session.GetMessagesAsync())
        {
            var sentence = message.Payload.Output?.Sentence;
            if (sentence is null)
            {
                continue;
            }

            if (sentence.SentenceEnd == true)
            {
                Console.WriteLine($"Recognized: {sentence.Text}");
                var words = sentence.Words;
                if (words is { Length: > 0 })
                {
                    Console.WriteLine(
                        "Word timestamps: " + string.Join(", ", words.Select(w => $"{w.Text}[{w.BeginTime}-{w.EndTime}ms]")));
                }

                duration = Math.Max(duration, message.Payload.Usage?.Duration ?? 0);
            }
            else
            {
                Console.WriteLine($"Interim result: {sentence.Text}");
            }
        }

        Console.WriteLine($"Billed duration: {duration}s");
    }
}
