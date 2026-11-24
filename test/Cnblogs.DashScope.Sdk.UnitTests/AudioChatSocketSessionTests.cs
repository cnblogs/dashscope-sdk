using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;

namespace Cnblogs.DashScope.Sdk.UnitTests;

[Collection(nameof(SocketTestsCollection))]
public class AudioChatSocketSessionTests
{
    [Fact]
    public async Task Connect_WaitSessionCreated_SuccessAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.AudioChat.SessionCreated;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        using var session = new AudioChatSocketSession(server);

        // Act
        var sessionConfig = await session.StartAsync();

        // Assert
        Assert.Equivalent(sessionCreated.Message.Session, sessionConfig);
    }

    [Fact]
    public async Task UpdateSession_ApplyVoiceAndServerVad_SuccessAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.AudioChat.SessionCreated;
        var sessionUpdated = Snapshots.AudioChat.SessionUpdated;
        var sessionUpdate = Snapshots.AudioChat.SessionUpdate;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(sessionUpdated.GetMessageJson()));
        using var session = new AudioChatSocketSession(server);
        await session.StartAsync();

        // Act
        var updated = await session.UpdateSessionAsync(
            new AudioChatSession
            {
                Modalities = new List<string> { "text", "audio" },
                Voice = "longanqian",
                TurnDetection = new AudioChatTurnDetection
                {
                    Type = "server_vad",
                    Threshold = 0.5f,
                    SilenceDurationMs = 500
                }
            });

        // Assert
        Assert.True(
            Checkers.IsJsonEquivalent(
                Checkers.NormalizeEventId(server, 0, sessionUpdate.GetMessageJson()),
                sessionUpdate.GetMessageJson()));
        Assert.Equivalent(sessionUpdated.Message.Session, updated);
    }

    [Fact]
    public async Task UpdateSession_NullTurnDetection_SendsExplicitNullAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.AudioChatManual.SessionCreated;
        var sessionUpdated = Snapshots.AudioChatManual.SessionUpdated;
        var sessionUpdate = Snapshots.AudioChatManual.SessionUpdate;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(sessionUpdated.GetMessageJson()));
        using var session = new AudioChatSocketSession(server);
        await session.StartAsync();

        // Act
        var updated = await session.UpdateSessionAsync(
            new AudioChatSession
            {
                Modalities = new List<string> { "text", "audio" },
                Voice = "longanqian",
                TurnDetection = null // the push-to-talk mode, which the server acknowledges without turn_detection
            });

        // Assert
        // the fixture carries an explicit "turn_detection": null
        Assert.True(
            Checkers.IsJsonEquivalent(
                Checkers.NormalizeEventId(server, 0, sessionUpdate.GetMessageJson()),
                sessionUpdate.GetMessageJson()));
        Assert.Equivalent(sessionUpdated.Message.Session, updated);
        Assert.Null(updated.TurnDetection);
    }

    [Fact]
    public async Task UpdateSession_ServerDefaultTurnDetection_OmitsMemberAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.AudioChat.SessionCreated;
        var sessionUpdated = Snapshots.AudioChat.SessionUpdated;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        // the ack only completes the handshake here
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(sessionUpdated.GetMessageJson()));
        using var session = new AudioChatSocketSession(server);
        await session.StartAsync();

        // Act
        await session.UpdateSessionAsync(
            new AudioChatSession
            {
                Voice = "longanqian",
                TurnDetection = AudioChatTurnDetection.ServerDefault
            });

        // Assert
        // the member stays out, so the server keeps the default turn detection of session.created
        var sent = Checkers.GetSentMessage(server, 0)["session"]!.AsObject();
        Assert.False(sent.ContainsKey("turn_detection"));
        Assert.Equal("longanqian", (string?)sent["voice"]);
    }

    [Fact]
    public async Task AppendCommitCreateResponse_PushToTalk_SuccessAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.AudioChatManual.SessionCreated;
        var commitAudio = Snapshots.AudioChatManual.CommitAudio;
        var responseCreate = Snapshots.AudioChatManual.ResponseCreate;
        var committed = Snapshots.AudioChatManual.InputAudioBufferCommitted;
        var transcriptionCompleted = Snapshots.AudioChatManual.ConversationItemInputAudioTranscriptionCompleted;
        var responseDone = Snapshots.AudioChatManual.ResponseDone;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        server.Playlist.Enqueue(async s =>
            await s.WriteServerMessageAsync(Snapshots.AudioChatManual.SessionUpdated.GetMessageJson()));
        for (var i = 0; i < 39; i++)
        {
            server.Playlist.Enqueue(_ => Task.CompletedTask); // appends have no ack, 123554 bytes / 3200
        }

        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(committed.GetMessageJson());
            await s.WriteServerMessageAsync(transcriptionCompleted.GetMessageJson());
        });
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(responseDone.GetMessageJson()));
        using var session = new AudioChatSocketSession(server);
        await session.StartAsync();
        await session.UpdateSessionAsync(new AudioChatSession()); // push-to-talk: turn_detection null

        // Act
        await using var audio =
            File.OpenRead(Path.Combine("RawHttpData", "socket-qwen-audio-realtime-manual.input.pcm"));
        await session.AppendAudioAsync(audio);
        var itemId = await session.CommitAudioAsync();
        var response = await session.CreateResponseAsync(new List<string> { "text", "audio" });

        // Assert
        // 39 audio appends, the commit and the response.create
        Assert.Equal(42, server.ServerReceivedMessages.Count);
        Assert.True(
            Checkers.IsJsonEquivalent(
                Checkers.NormalizeEventId(server, 40, commitAudio.GetMessageJson()),
                commitAudio.GetMessageJson()));
        Assert.True(
            Checkers.IsJsonEquivalent(
                Checkers.NormalizeEventId(server, 41, responseCreate.GetMessageJson()),
                responseCreate.GetMessageJson()));
        Assert.Equal(committed.Message.ItemId, itemId);
        Assert.Equal("completed", response.Status);
        Assert.Equal("item_BTciU9CfDIRXyf6BjSZOJ", response.Output![0].Id);
        Assert.Equal("audio", response.Output[0].Content![0].Type);
        Assert.Equal(526, response.Usage!.TotalTokens);
        Assert.Equal(178, response.Usage.OutputTokensDetails!.AudioTokens);
    }

    [Fact]
    public async Task FullFlow_VadMode_StreamsTextAndAudio_SuccessAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.AudioChat.SessionCreated;
        var sessionUpdated = Snapshots.AudioChat.SessionUpdated;
        var speechStarted = Snapshots.AudioChat.InputAudioBufferSpeechStarted;
        var itemCreated = Snapshots.AudioChat.ConversationItemCreated;
        var responseDone = Snapshots.AudioChat.ResponseDone;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(sessionUpdated.GetMessageJson()));
        for (var i = 0; i < 38; i++)
        {
            server.Playlist.Enqueue(_ => Task.CompletedTask); // appends have no ack
        }

        // the server fires the whole vad + response flow while the last audio chunk arrives
        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(speechStarted.GetMessageJson());
            await s.WriteServerMessageAsync(
                Snapshots.AudioChat.ConversationItemInputAudioTranscriptionDelta.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.AudioChat.InputAudioBufferSpeechStopped.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.AudioChat.InputAudioBufferCommitted.GetMessageJson());
            await s.WriteServerMessageAsync(itemCreated.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.AudioChat.ResponseCreated.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.AudioChat.ResponseAudioTranscriptDelta.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.AudioChat.ResponseAudioDelta.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.AudioChat.ResponseAudioTranscriptDone.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.AudioChat.ResponseAudioDone.GetMessageJson());
            await s.WriteServerMessageAsync(responseDone.GetMessageJson());
            await s.WriteServerCloseAsync();
        });
        using var session = new AudioChatSocketSession(server);
        await session.StartAsync();
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
            });

        // Act
        await using var audio = File.OpenRead(Path.Combine("RawHttpData", "socket-qwen-audio-realtime.input.pcm"));
        await session.AppendAudioAsync(audio);
        var events = await session.GetEventsAsync().ToListAsync();
        var audioBytes = await session.GetAudioAsync().ToListAsync();

        // Assert
        // session.created, session.updated, vad + committed events, response events and the trimmed deltas
        Assert.Equal(13, events.Count);
        Assert.Equal(
            Snapshots.AudioChat.ResponseAudioTranscriptDone.Message.Transcript,
            events.First(e => e.Type == "response.audio_transcript.done").Transcript);
        Assert.Equal(
            responseDone.Message.Response!.Output![0].Content![0].Transcript,
            events[12].Response!.Output![0].Content![0].Transcript);
        Assert.Equal("驳", events.First(e => e.Type == "conversation.item.input_audio_transcription.delta").Stash);
        Assert.NotEmpty(audioBytes); // the base64 deltas are decoded into the audio stream
    }
}
