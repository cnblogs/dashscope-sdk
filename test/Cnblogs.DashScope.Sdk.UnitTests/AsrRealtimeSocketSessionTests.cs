using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;

namespace Cnblogs.DashScope.Sdk.UnitTests;

[Collection(nameof(SocketTestsCollection))]
public class AsrRealtimeSocketSessionTests
{
    [Fact]
    public async Task Connect_WaitSessionCreated_SuccessAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.AsrRealtime.SessionCreated;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        using var session = new AsrRealtimeSocketSession(server);

        // Act
        var sessionConfig = await session.StartAsync();

        // Assert
        Assert.Equivalent(sessionCreated.Message.Session, sessionConfig);
    }

    [Fact]
    public async Task UpdateSession_ApplyLanguageAndServerVad_SuccessAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.AsrRealtime.SessionCreated;
        var sessionUpdated = Snapshots.AsrRealtime.SessionUpdated;
        var sessionUpdate = Snapshots.AsrRealtime.SessionUpdate;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(sessionUpdated.GetMessageJson()));
        using var session = new AsrRealtimeSocketSession(server);
        await session.StartAsync();

        // Act
        var updated = await session.UpdateSessionAsync(
            new AsrRealtimeSession
            {
                InputAudioFormat = "pcm",
                SampleRate = 16000,
                InputAudioTranscription = new AsrRealtimeTranscription { Language = "zh" },
                TurnDetection = new AsrRealtimeTurnDetection { Threshold = 0.0f, SilenceDurationMs = 400 }
            });

        // Assert
        Assert.True(Checkers.IsJsonEquivalent(
            Checkers.NormalizeEventId(server, 0, sessionUpdate.GetMessageJson()),
            sessionUpdate.GetMessageJson()));
        Assert.Equivalent(sessionUpdated.Message.Session, updated);
    }

    [Fact]
    public async Task UpdateSession_NullTurnDetection_SendsExplicitNull_SuccessAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.AsrRealtimeManual.SessionCreated;
        var sessionUpdated = Snapshots.AsrRealtimeManual.SessionUpdated;
        var sessionUpdate = Snapshots.AsrRealtimeManual.SessionUpdate;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(sessionUpdated.GetMessageJson()));
        using var session = new AsrRealtimeSocketSession(server);
        await session.StartAsync();

        // Act
        var updated = await session.UpdateSessionAsync(
            new AsrRealtimeSession
            {
                InputAudioFormat = "pcm",
                SampleRate = 16000,
                InputAudioTranscription = new AsrRealtimeTranscription { Language = "zh" },
                TurnDetection = null // the manual mode, which the server acknowledges without turn_detection
            });

        // Assert
        // the fixture carries an explicit "turn_detection": null
        Assert.True(Checkers.IsJsonEquivalent(
            Checkers.NormalizeEventId(server, 0, sessionUpdate.GetMessageJson()),
            sessionUpdate.GetMessageJson()));
        Assert.Equivalent(sessionUpdated.Message.Session, updated);
        Assert.Null(updated.TurnDetection);
    }

    [Fact]
    public async Task UpdateSession_ServerDefaultTurnDetection_OmitsMember_SuccessAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.AsrRealtime.SessionCreated;
        var sessionUpdated = Snapshots.AsrRealtime.SessionUpdated;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        // the ack only completes the handshake here
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(sessionUpdated.GetMessageJson()));
        using var session = new AsrRealtimeSocketSession(server);
        await session.StartAsync();

        // Act
        await session.UpdateSessionAsync(
            new AsrRealtimeSession
            {
                InputAudioTranscription = new AsrRealtimeTranscription { Language = "zh" },
                TurnDetection = AsrRealtimeTurnDetection.ServerDefault
            });

        // Assert
        // the member stays out, so the server keeps the default VAD configuration of session.created
        var sent = Checkers.GetSentMessage(server, 0)["session"]!.AsObject();
        Assert.False(sent.ContainsKey("turn_detection"));
        Assert.Equal("zh", (string?)sent["input_audio_transcription"]!["language"]);
    }

    [Fact]
    public async Task AppendAudio_ThenCommitAudio_SuccessAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.AsrRealtimeManual.SessionCreated;
        var commitAudio = Snapshots.AsrRealtimeManual.CommitAudio;
        var committed = Snapshots.AsrRealtimeManual.Committed;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        for (var i = 0; i < 39; i++)
        {
            server.Playlist.Enqueue(_ => Task.CompletedTask); // append has no ack, 123520 bytes / 3200
        }

        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(committed.GetMessageJson()));
        using var session = new AsrRealtimeSocketSession(server);
        await session.StartAsync();

        // Act
        await using var audio = File.OpenRead(Path.Combine("RawHttpData", "socket-qwen-asr-realtime-manual.input.pcm"));
        await session.AppendAudioAsync(audio);
        var itemId = await session.CommitAudioAsync();

        // Assert
        // 39 audio appends followed by the commit, the audio is base64 encoded inside the json
        Assert.Equal(40, server.ServerReceivedMessages.Count);
        Assert.True(Checkers.IsJsonEquivalent(
            Checkers.NormalizeEventId(server, 39, commitAudio.GetMessageJson()),
            commitAudio.GetMessageJson()));
        // the committed event carries no item_id, it arrives in conversation.item.created instead
        Assert.Equal(string.Empty, itemId);
    }

    [Fact]
    public async Task FinishSession_VadModeTranscription_SuccessAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.AsrRealtime.SessionCreated;
        var sessionUpdated = Snapshots.AsrRealtime.SessionUpdated;
        var itemCreated = Snapshots.AsrRealtime.ItemCreated;
        var transcriptionText = Snapshots.AsrRealtime.TranscriptionText;
        var transcriptionCompleted = Snapshots.AsrRealtime.TranscriptionCompleted;
        var sessionFinished = Snapshots.AsrRealtime.SessionFinished;
        var finishSession = Snapshots.AsrRealtime.FinishSession;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(sessionUpdated.GetMessageJson()));
        for (var i = 0; i < 39; i++)
        {
            server.Playlist.Enqueue(_ => Task.CompletedTask); // append has no ack
        }

        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(itemCreated.GetMessageJson());
            await s.WriteServerMessageAsync(transcriptionText.GetMessageJson());
            await s.WriteServerMessageAsync(transcriptionCompleted.GetMessageJson());
            await s.WriteServerMessageAsync(sessionFinished.GetMessageJson());
        });
        using var session = new AsrRealtimeSocketSession(server);
        await session.StartAsync();
        await session.UpdateSessionAsync(
            new AsrRealtimeSession
            {
                InputAudioTranscription = new AsrRealtimeTranscription { Language = "zh" },
                TurnDetection = new AsrRealtimeTurnDetection()
            });

        // Act
        await using var audio = File.OpenRead(Path.Combine("RawHttpData", "socket-qwen-asr-realtime.input.pcm"));
        await session.AppendAudioAsync(audio);
        await session.FinishAsync();
        var events = await session.GetEventsAsync().ToListAsync();

        // Assert
        Assert.True(Checkers.IsJsonEquivalent(
            Checkers.NormalizeEventId(server, 40, finishSession.GetMessageJson()),
            finishSession.GetMessageJson()));
        // session.created, session.updated, item created, transcription text / completed, session.finished
        Assert.Equal(6, events.Count);
        Assert.Equivalent(transcriptionCompleted.Message, events[4]);
        Assert.Equal("博客园成立于二零零四年。", events[4].Transcript);
        Assert.Equal("zh", events[4].Language);
        Assert.Equal("neutral", events[4].Emotion);
        Assert.Equal(2, events[4].Usage?.Duration);
        Assert.Equal(62, events[4].Usage?.TotalTokens);

        // the incremental text events carry the accumulated text in the stash, text stays empty
        Assert.Equal(string.Empty, events[3].Text);
        Assert.Equal("博客", events[3].Stash);
    }
}
