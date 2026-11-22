using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.SpeechRecognition;

public class QwenAsrMessageExample : SpeechRecognitionSample
{
    /// <inheritdoc />
    public override string Description => "Real-time speech recognition with qwen-audio-3.1-asr-flash-message (context and intermediate results)";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        Console.WriteLine("Recognizing...");
        using var session = await client.CreateSpeechRecognitionSocketSessionAsync("qwen-audio-3.1-asr-flash-message");
        var taskId = await session.RunTaskAsync(
            new SpeechRecognitionParameters
            {
                Format = "wav",
                SampleRate = 16000,
                LanguageHints = ["zh"],
                // intermediate results are off by default for this model
                IntermediateResultEnabled = true,
                // instant hot words, the weight is 1~5 or 50
                Vocabulary = new Dictionary<string, int> { ["博客园"] = 5, ["代码改变世界"] = 5 }
            },
            new SpeechRecognitionInput
            {
                // the conversation context helps the model recognize proper nouns
                Context =
                [
                    new SpeechRecognitionContext("user", [new SpeechRecognitionContextContent("input_text", "博客园是什么时候成立的")]),
                    new SpeechRecognitionContext("assistant", [new SpeechRecognitionContextContent("text", "博客园成立于2004年。")])
                ]
            });

        await using var audio = File.OpenRead("speech.wav");
        await session.SendAudioAsync(audio);
        // the context can be updated while the task is running
        await session.ContinueTaskAsync(
            taskId,
            new SpeechRecognitionInput
            {
                Context = [new SpeechRecognitionContext("user", [new SpeechRecognitionContextContent("input_text", "代码改变世界")])]
            });
        await session.FinishTaskAsync(taskId);

        // intermediate results of a sentence may be revised, keep the text of each sentence by its id
        var sentences = new Dictionary<int, string>();
        var duration = 0;
        await foreach (var message in session.GetMessagesAsync())
        {
            var sentence = message.Payload.Output?.Sentence;
            if (sentence?.SentenceId == null || sentence.Heartbeat == true)
            {
                continue;
            }

            sentences[sentence.SentenceId.Value] = sentence.Text ?? string.Empty;
            if (sentence.SentenceEnd == true)
            {
                Console.WriteLine($"Recognized: {sentence.Text}");
                duration = Math.Max(duration, message.Payload.Usage?.Duration ?? 0);
            }
            else if (sentence.SentenceBegin != true)
            {
                Console.WriteLine($"Interim result of sentence {sentence.SentenceId}: {sentence.Text}");
            }
        }

        Console.WriteLine($"Billed duration: {duration}s, {sentences.Count} sentences");
    }
}
