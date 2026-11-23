using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.SpeechRecognition;

public class LiveTranslateExample : SpeechRecognitionSample
{
    /// <inheritdoc />
    public override string Description
        => "Real-time live translation with qwen3.5-livetranslate-flash-realtime (zh to en, hotword corpus)";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        Console.WriteLine("Translating...");
        using var session = await client.CreateLiveTranslateSocketSessionAsync("qwen3.5-livetranslate-flash-realtime");
        await session.UpdateSessionAsync(
            new LiveTranslateSession
            {
                InputAudioFormat = "pcm",
                SampleRate = 16000,
                InputAudioTranscription = new LiveTranslateTranscription(
                    Model: "qwen3-asr-flash-realtime",
                    Language: "zh"),
                TurnDetection = new LiveTranslateTurnDetection { Threshold = 0.2f, SilenceDurationMs = 1000 },
                Translation = new LiveTranslateTranslation
                {
                    Language = "en",
                    Corpus = new LiveTranslateCorpus
                    {
                        // the hotword corpus pins the translation of specific phrases
                        Phrases = new Dictionary<string, string> { ["博客园"] = "Cnblogs" }
                    }
                }
            });

        // consume the events while the audio is being sent
        var consuming = Task.Run(async () =>
        {
            // ReSharper disable once AccessToDisposedClosure
            await foreach (var e in session.GetEventsAsync())
            {
                switch (e.Type)
                {
                    case "conversation.item.input_audio_transcription.completed":
                        Console.WriteLine($"Source: {e.Transcript} ({e.Language})");
                        break;
                    case "response.audio_transcript.done":
                        Console.WriteLine($"Translated: {e.Transcript}");
                        break;
                    case "response.done":
                        Console.WriteLine($"Response done: {e.Response!.Status}, tokens: {e.Response.Usage?.TotalTokens}");
                        break;
                }
            }
        });

        await using var audio = File.OpenRead("speech.wav");
        // note: the realtime endpoint takes raw pcm, strip the wav header first
        var buffer = new byte[audio.Length - 44];
        audio.Position = 44;
        await audio.ReadExactlyAsync(buffer);
        await session.AppendAudioAsync(buffer);

        // the server finishes the remaining translation, then ends the session
        await session.FinishAsync();
        await consuming;
    }
}
