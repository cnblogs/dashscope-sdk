using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.SpeechRecognition;

public class QwenAsrRealtimeExample : SpeechRecognitionSample
{
    /// <inheritdoc />
    public override string Description => "Real-time speech recognition with qwen3-asr-flash-realtime (VAD mode, incremental results)";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        Console.WriteLine("Recognizing...");
        using var session = await client.CreateAsrRealtimeSocketSessionAsync("qwen3-asr-flash-realtime");
        await session.UpdateSessionAsync(
            new AsrRealtimeSession
            {
                InputAudioFormat = "pcm",
                SampleRate = 16000,
                // unset the language to let the model detect it automatically
                InputAudioTranscription = new AsrRealtimeTranscription { Language = "zh" },
                // the VAD mode with custom settings; leave it unset to keep the server settings,
                // null switches to the manual mode (CommitAudioAsync after the audio)
                TurnDetection = new AsrRealtimeTurnDetection { Threshold = 0.0f, SilenceDurationMs = 400 }
            });

        // consume the events while the audio is being recognized
        var consuming = Task.Run(async () =>
        {
            // ReSharper disable once AccessToDisposedClosure
            await foreach (var e in session.GetEventsAsync())
            {
                switch (e.Type)
                {
                    case "conversation.item.input_audio_transcription.text":
                        // interim results arrive in the stash and may still be revised
                        Console.WriteLine($"Interim: {e.Stash}");
                        break;
                    case "conversation.item.input_audio_transcription.completed":
                        Console.WriteLine($"Recognized: {e.Transcript}");
                        Console.WriteLine($"Language: {e.Language}, emotion: {e.Emotion}, tokens: {e.Usage?.TotalTokens}");
                        break;
                }
            }
        });

        await using var audio = File.OpenRead("speech.wav");
        // note: the realtime endpoint takes raw pcm, strip the wav header first when sending a wav file
        var buffer = new byte[audio.Length - 44];
        audio.Position = 44;
        await audio.ReadExactlyAsync(buffer);
        await session.AppendAudioAsync(buffer);

        // the server returns the transcription of the remaining audio, then closes the connection
        await session.FinishAsync();
        await consuming;
    }
}
