using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;

namespace Cnblogs.DashScope.Sdk.UnitTests;

[Collection(nameof(SocketTestsCollection))]
public class LiveTranslateSocketSessionTests
{
    [Fact]
    public async Task Connect_WaitSessionCreated_SuccessAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.LiveTranslate.SessionCreated;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        using var session = new LiveTranslateSocketSession(server);

        // Act
        var sessionConfig = await session.StartAsync();

        // Assert
        Assert.Equivalent(sessionCreated.Message.Session, sessionConfig);
    }

    [Fact]
    public async Task UpdateSession_ApplyTranslationAndVad_SuccessAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.LiveTranslate.SessionCreated;
        var sessionUpdated = Snapshots.LiveTranslate.SessionUpdated;
        var sessionUpdate = Snapshots.LiveTranslate.SessionUpdate;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(sessionUpdated.GetMessageJson()));
        using var session = new LiveTranslateSocketSession(server);
        await session.StartAsync();

        // Act
        var updated = await session.UpdateSessionAsync(
            new LiveTranslateSession
            {
                Voice = "Tina",
                InputAudioTranscription = new LiveTranslateTranscription(
                    Model: "qwen3-asr-flash-realtime",
                    Language: "zh"),
                TurnDetection = new LiveTranslateTurnDetection
                {
                    Type = "server_vad",
                    Threshold = 0.2f,
                    SilenceDurationMs = 1000
                },
                Translation = new LiveTranslateTranslation
                {
                    Language = "en",
                    Corpus = new LiveTranslateCorpus
                    {
                        Phrases = new Dictionary<string, string> { ["博客园"] = "Cnblogs" }
                    }
                }
            });

        // Assert
        Assert.True(Checkers.IsJsonEquivalent(
            Checkers.NormalizeEventId(server, 0, sessionUpdate.GetMessageJson()),
            sessionUpdate.GetMessageJson()));
        Assert.Equivalent(sessionUpdated.Message.Session, updated);
        // the server echoes the VAD behavior flags it applies
        Assert.True(updated.TurnDetection!.CreateResponse);
        Assert.True(updated.TurnDetection.InterruptResponse);
        // the hotword corpus round-trips
        Assert.Equal("Cnblogs", updated.Translation!.Corpus!.Phrases!["博客园"]);
    }

    [Fact]
    public async Task UpdateSession_NullTurnDetection_SendsExplicitNullAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.LiveTranslate.SessionCreated;
        var sessionUpdated = Snapshots.LiveTranslate.SessionUpdated;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        // the ack only completes the handshake here
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(sessionUpdated.GetMessageJson()));
        using var session = new LiveTranslateSocketSession(server);
        await session.StartAsync();

        // Act
        await session.UpdateSessionAsync(
            new LiveTranslateSession
            {
                Translation = new LiveTranslateTranslation { Language = "en" },
                TurnDetection = null // the manual mode
            });

        // Assert
        // the manual mode is requested with an explicit turn_detection null (JsonObject hands
        // a JSON null out as a null node, so only ContainsKey tells it from a missing member)
        var sent = Checkers.GetSentMessage(server, 0)["session"]!.AsObject();
        Assert.True(sent.ContainsKey("turn_detection"));
        Assert.Null(sent["turn_detection"]);
    }

    [Fact]
    public async Task UpdateSession_ServerDefaultTurnDetection_OmitsMemberAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.LiveTranslate.SessionCreated;
        var sessionUpdated = Snapshots.LiveTranslate.SessionUpdated;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        // the ack only completes the handshake here
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(sessionUpdated.GetMessageJson()));
        using var session = new LiveTranslateSocketSession(server);
        await session.StartAsync();

        // Act
        await session.UpdateSessionAsync(
            new LiveTranslateSession
            {
                Translation = new LiveTranslateTranslation { Language = "en" },
                TurnDetection = LiveTranslateTurnDetection.ServerDefault
            });

        // Assert
        // the member stays out, so the server keeps the default VAD configuration of session.created
        var sent = Checkers.GetSentMessage(server, 0)["session"]!.AsObject();
        Assert.False(sent.ContainsKey("turn_detection"));
        Assert.Equal("en", (string?)sent["translation"]!["language"]);
    }

    [Fact]
    public async Task UpdateSession_ServerError_ThrowAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.LiveTranslate.SessionCreated;
        var timeout = Snapshots.LiveTranslate.Timeout;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(timeout.GetMessageJson()));
        using var session = new LiveTranslateSocketSession(server);
        await session.StartAsync();

        // Act
        // ReSharper disable once AccessToDisposedClosure
        var act = () => session.UpdateSessionAsync(new LiveTranslateSession { Voice = "Tina" });

        // Assert
        var exception = await Assert.ThrowsAsync<DashScopeException>(act);
        Assert.Equal("COMMON_ERROR", exception.Error?.Code);
    }

    [Fact]
    public async Task AppendAudioThenFinish_TranslatesAndFinishes_SuccessAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.LiveTranslate.SessionCreated;
        var sessionUpdated = Snapshots.LiveTranslate.SessionUpdated;
        var finishSession = Snapshots.LiveTranslate.FinishSession;
        var responseDone = Snapshots.LiveTranslate.ResponseDone;
        var transcriptionCompleted = Snapshots.LiveTranslate.ConversationItemInputAudioTranscriptionCompleted;
        var sessionFinished = Snapshots.LiveTranslate.SessionFinished;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(sessionUpdated.GetMessageJson()));
        for (var i = 0; i < 39; i++)
        {
            server.Playlist.Enqueue(_ => Task.CompletedTask); // appends have no ack, 123554 bytes / 3200
        }

        // the server fires the translation flow while session.finish arrives
        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(Snapshots.LiveTranslate.InputAudioBufferSpeechStarted.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.LiveTranslate.ConversationItemInputAudioTranscriptionText.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.LiveTranslate.ResponseAudioTranscriptText.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.LiveTranslate.InputAudioBufferSpeechStopped.GetMessageJson());
            await s.WriteServerMessageAsync(transcriptionCompleted.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.LiveTranslate.ResponseAudioTranscriptDone.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.LiveTranslate.ResponseAudioDelta.GetMessageJson());
            await s.WriteServerMessageAsync(responseDone.GetMessageJson());
            await s.WriteServerMessageAsync(sessionFinished.GetMessageJson());
            await s.WriteServerCloseAsync();
        });
        using var session = new LiveTranslateSocketSession(server);
        await session.StartAsync();
        await session.UpdateSessionAsync(new LiveTranslateSession { Voice = "Tina" });

        // Act
        await using var audio = File.OpenRead(Path.Combine("RawHttpData", "socket-qwen-livetranslate-realtime.input.pcm"));
        await session.AppendAudioAsync(audio);
        await session.FinishAsync();
        var events = await session.GetEventsAsync().ToListAsync();
        var audioBytes = await session.GetAudioAsync().ToListAsync();

        // Assert
        // 39 appends followed by the session.finish
        Assert.True(Checkers.IsJsonEquivalent(
            Checkers.NormalizeEventId(server, 40, finishSession.GetMessageJson()),
            finishSession.GetMessageJson()));
        // session.created, session.updated, speech + transcription events, response events,
        // session.finished
        Assert.Equal(11, events.Count);
        var source = events.First(e => e.Type == "conversation.item.input_audio_transcription.completed");
        Assert.Equal("博客园成立于二零零四年，代码改变世界。", source.Transcript);
        Assert.Equal("zh", source.Language);
        Assert.Equal("neutral", source.Emotion);
        var translation = events.First(e => e.Type == "response.audio_transcript.done");
        Assert.StartsWith("Cnblogs was founded in 2004", translation.Transcript); // the corpus phrase applies
        Assert.Equal("completed", events[9].Response!.Status);
        Assert.NotEmpty(audioBytes); // the base64 deltas are decoded into the audio stream
    }
}
