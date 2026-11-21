using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.SpeechSynthesis;

public class SambertExample : SpeechSynthesisSample
{
    /// <inheritdoc />
    public override string Description => "One-shot text to speech with Sambert (word and phoneme timestamps)";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        Console.WriteLine("Synthesizing...");
        using var session = await client.CreateSpeechSynthesizerSocketSessionAsync("sambert-zhichu-v1");
        // Sambert sends the whole text in run-task (streaming: out), no continue/finish-task.
        await session.SynthesizeAsync(
            new SpeechSynthesizerParameters
            {
                Format = "wav",
                SampleRate = 16000,
                WordTimestampEnabled = true,
                PhonemeTimestampEnabled = true
            },
            "床前明月光，疑是地上霜");

        var file = new FileInfo("tts-sambert.wav");
        await using var stream = file.OpenWrite();
        await foreach (var b in session.GetAudioAsync())
        {
            stream.WriteByte(b);
        }

        Console.WriteLine($"Audio saved to {file.FullName}");

        await foreach (var message in session.GetMessagesAsync())
        {
            var sentence = message.Payload.Output?.Sentence;
            if (sentence?.Words is null || sentence.Words.Length == 0)
            {
                continue;
            }

            Console.WriteLine($"Sentence: {sentence.BeginTime}~{sentence.EndTime}ms");
            foreach (var word in sentence.Words)
            {
                var phonemes = word.Phonemes is null
                    ? string.Empty
                    : $" [{string.Join(", ", word.Phonemes.Select(p => $"{p.Text}({p.Tone})"))}]";
                Console.WriteLine($"  {word.Text}: {word.BeginTime}~{word.EndTime}ms{phonemes}");
            }
        }
    }
}
