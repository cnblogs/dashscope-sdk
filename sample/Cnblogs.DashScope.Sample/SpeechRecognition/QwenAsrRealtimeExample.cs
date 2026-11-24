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
        // note: the realtime endpoint takes raw pcm, skip the wav header when sending a wav file, the
        // stream overload sends the rest as base64 chunks of 3200 bytes
        audio.Position = 44;
        await session.AppendAudioAsync(audio);

        // in the VAD mode do not finish right after the audio: the appends are not acknowledged, and
        // a finish that arrives before the server has consumed the buffered audio ends the session
        // without the transcription. A live stream delivers the audio continuously, a file is pushed
        // as fast as possible, so give the server a moment
        await Task.Delay(TimeSpan.FromSeconds(1));

        // the server returns the transcription of the remaining audio, then closes the connection
        await session.FinishAsync();
        await consuming;
    }
}
