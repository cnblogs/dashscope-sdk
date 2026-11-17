using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.SpeechSynthesis;

public class CosyVoiceExample : SpeechSynthesisSample
{
    /// <inheritdoc />
    public override string Description => "Text to speech with CosyVoice";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        Console.WriteLine("Synthesizing...");
        using var session = await client.CreateSpeechSynthesizerSocketSessionAsync("cosyvoice-v3-flash");
        var taskId = await session.RunTaskAsync(
            new SpeechSynthesizerParameters
            {
                Voice = "longanhuan_v3",
                Format = "mp3",
                SampleRate = 24000,
                BitRate = 96,
                Seed = 42,
                LanguageHints = ["zh"],
                Instruction = "请用河南话表达。",
                WordTimestampEnabled = true,
                EnableAigcTag = true
            });
        await session.ContinueTaskAsync(taskId, "博客园成立于2004年，代码改变世界。");
        await session.FinishTaskAsync(taskId);

        var file = new FileInfo("tts.mp3");
        await using var stream = file.OpenWrite();
        await foreach (var b in session.GetAudioAsync())
        {
            stream.WriteByte(b);
        }

        Console.WriteLine($"Audio saved to {file.FullName}");

        var characters = 0;
        await foreach (var message in session.GetMessagesAsync())
        {
            characters = Math.Max(characters, message.Payload.Usage?.Characters ?? 0);
            var words = message.Payload.Output?.Sentence?.Words;
            if (words is { Length: > 0 })
            {
                Console.WriteLine(
                    "Word timestamps: " + string.Join(", ", words.Select(w => $"{w.Text}[{w.BeginTime}-{w.EndTime}ms]")));
            }
        }

        Console.WriteLine($"Billed characters: {characters}");
    }
}
