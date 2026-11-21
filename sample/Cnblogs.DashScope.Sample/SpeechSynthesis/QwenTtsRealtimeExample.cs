using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.SpeechSynthesis;

public class QwenTtsRealtimeExample : SpeechSynthesisSample
{
    /// <inheritdoc />
    public override string Description => "Realtime text to speech with qwen3-tts-flash-realtime";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        Console.WriteLine("Synthesizing...");
        // Unlike the CosyVoice session, a realtime session is closed by the server after
        // finish, create a new session for each synthesis run.
        using var session = await client.CreateTtsRealtimeSocketSessionAsync("qwen3-tts-flash-realtime");
        await session.UpdateSessionAsync(
            new TtsRealtimeSession
            {
                Voice = "Cherry",
                Mode = "server_commit",
                ResponseFormat = "mp3",
                SampleRate = 24000
            });

        // In server_commit mode the server synthesizes complete sentences automatically,
        // call CommitTextAsync to force the synthesis of the buffered text.
        await session.AppendTextAsync("你好，");
        await session.AppendTextAsync("我是千问。");
        await session.CommitTextAsync();
        await session.FinishAsync();

        var file = new FileInfo("tts-realtime.mp3");
        await using var stream = file.OpenWrite();
        await foreach (var b in session.GetAudioAsync())
        {
            stream.WriteByte(b);
        }

        Console.WriteLine($"Audio saved to {file.FullName}");

        TtsRealtimeServerEvent? responseDone = null;
        await foreach (var serverEvent in session.GetEventsAsync())
        {
            if (serverEvent.Type == "response.done")
            {
                responseDone = serverEvent;
            }
        }

        Console.WriteLine($"Billed characters: {responseDone?.Response?.Usage?.Characters}");
    }
}
