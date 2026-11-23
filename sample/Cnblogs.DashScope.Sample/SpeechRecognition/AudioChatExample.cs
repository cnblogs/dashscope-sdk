using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.SpeechRecognition;

public class AudioChatExample : SpeechRecognitionSample
{
    /// <inheritdoc />
    public override string Description
        => "Real-time voice conversation with qwen-audio-3.0-realtime-flash (VAD mode, streamed reply)";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        Console.WriteLine("Chatting...");
        using var session = await client.CreateAudioChatSocketSessionAsync("qwen-audio-3.0-realtime-flash");
        await session.UpdateSessionAsync(
            new AudioChatSession
            {
                Voice = "longanqian",
                TurnDetection = new AudioChatTurnDetection
                {
                    Type = "server_vad",
                    Threshold = 0.5f,
                    SilenceDurationMs = 500
                }
                // set DisableTurnDetection = true for the push-to-talk mode, then submit the
                // audio with CommitAudioAsync() and trigger the reply with CreateResponseAsync()
            });

        // consume the events while the audio is being sent
        var consuming = Task.Run(async () =>
        {
            // ReSharper disable once AccessToDisposedClosure
            await foreach (var e in session.GetEventsAsync())
            {
                switch (e.Type)
                {
                    case "conversation.item.input_audio_transcription.delta":
                        Console.WriteLine($"Heard: {e.Text}{e.Stash}");
                        break;
                    case "response.audio_transcript.delta":
                        Console.WriteLine($"Reply: {e.Delta}");
                        break;
                    case "response.done":
                        Console.WriteLine($"Turn done: {e.Response!.Status}, tokens: {e.Response.Usage?.TotalTokens}");
                        break;
                }
            }
        });
        var audioTask = Task.Run(async () =>
        {
            // ReSharper disable once AccessToDisposedClosure
            await using var output = File.Create("audiochat-reply.pcm");
            await foreach (var b in session.GetAudioAsync())
            {
                output.WriteByte(b);
            }
        });

        await using var audio = File.OpenRead("speech.wav");
        // note: the realtime endpoint takes raw pcm (16kHz 16bit mono), strip the wav header first
        var buffer = new byte[audio.Length - 44];
        audio.Position = 44;
        await audio.ReadExactlyAsync(buffer);
        await session.AppendAudioAsync(buffer);

        // wait for the vad turn to finish (response.done), then close the session
        await Task.WhenAny(consuming, Task.Delay(TimeSpan.FromSeconds(30)));
        session.Dispose();
        await Task.WhenAll(consuming, audioTask);
        Console.WriteLine("Reply audio saved to audiochat-reply.pcm (24kHz 16bit mono pcm)");
    }
}
