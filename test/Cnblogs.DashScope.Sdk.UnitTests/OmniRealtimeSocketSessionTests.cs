using System.Net.WebSockets;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;

namespace Cnblogs.DashScope.Sdk.UnitTests;

[Collection(nameof(SocketTestsCollection))]
public class OmniRealtimeSocketSessionTests
{
    private const string Instructions = "你是个人助理小云，请你准确且友好地解答用户的问题，始终以乐于助人的态度回应。";

    public static TheoryData<SocketMessageSnapshot<OmniRealtimeServerEvent>> ServerEvents { get; } = CreateServerEventData();

    [Fact]
    public async Task Start_WaitSessionCreated_SuccessAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.OmniRealtime.SessionCreated;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        using var session = new OmniRealtimeSocketSession(server);

        // Act
        var sessionConfig = await session.StartAsync();

        // Assert
        Assert.Equivalent(sessionCreated.Message.Session, sessionConfig);
        Assert.Equal("qwen3.8-omni-flash-realtime", sessionConfig.Model);
        Assert.Equal("server_vad", sessionConfig.TurnDetection!.Type);
        Assert.Equal("qwen3-asr-flash-realtime", sessionConfig.InputAudioTranscription!.Model);
    }

    [Fact]
    public async Task UpdateSession_VadConfiguration_SendUpdateAndReturnEchoAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.OmniRealtime.SessionCreated;
        var sessionUpdated = Snapshots.OmniRealtime.SessionUpdated;
        var sessionUpdate = Snapshots.OmniRealtime.SessionUpdate;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(sessionUpdated.GetMessageJson()));
        using var session = new OmniRealtimeSocketSession(server);
        await session.StartAsync();

        // Act
        var updated = await session.UpdateSessionAsync(
            new OmniRealtimeSession
            {
                Modalities = new List<string> { "text", "audio" },
                Voice = "Tina",
                Instructions = Instructions,
                TurnDetection = new OmniRealtimeTurnDetection
                {
                    Type = "server_vad",
                    Threshold = 0.5f,
                    SilenceDurationMs = 800
                }
            });

        // Assert
        Assert.True(
            Checkers.IsJsonEquivalent(
                Checkers.NormalizeEventId(server, 0, sessionUpdate.GetMessageJson()),
                sessionUpdate.GetMessageJson()));
        Assert.Equivalent(sessionUpdated.Message.Session, updated);
        // the server echoes the turn detection behavior flags it applies
        Assert.True(updated.TurnDetection!.CreateResponse);
        Assert.True(updated.TurnDetection.InterruptResponse);
        Assert.Equal(300, updated.TurnDetection.PrefixPaddingMs);
    }

    [Fact]
    public async Task UpdateSession_DefaultOptionalMembers_OmitMembersAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.OmniRealtime.SessionCreated;
        var sessionUpdated = Snapshots.OmniRealtime.SessionUpdated;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        // the ack only completes the handshake here
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(sessionUpdated.GetMessageJson()));
        using var session = new OmniRealtimeSocketSession(server);
        await session.StartAsync();

        // Act
        // TurnDetection and InputAudioTranscription keep their defaults, which leave the members out
        await session.UpdateSessionAsync(new OmniRealtimeSession { Voice = "Tina" });

        // Assert
        // so the server keeps the turn detection and the transcription it starts the session with
        var sent = Checkers.GetSentMessage(server, 0)["session"]!.AsObject();
        Assert.False(sent.ContainsKey("turn_detection"));
        Assert.False(sent.ContainsKey("input_audio_transcription"));
        Assert.Equal("Tina", (string?)sent["voice"]);
    }

    [Fact]
    public async Task UpdateSession_ManualMode_SendExplicitNullTurnDetectionAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.OmniRealtimeManual.SessionCreated;
        var sessionUpdated = Snapshots.OmniRealtimeManual.SessionUpdated;
        var sessionUpdate = Snapshots.OmniRealtimeManual.SessionUpdate;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(sessionUpdated.GetMessageJson()));
        using var session = new OmniRealtimeSocketSession(server);
        await session.StartAsync();

        // Act
        var updated = await session.UpdateSessionAsync(CreateFullSession());

        // Assert
        Assert.True(
            Checkers.IsJsonEquivalent(
                Checkers.NormalizeEventId(server, 0, sessionUpdate.GetMessageJson()),
                sessionUpdate.GetMessageJson()));
        Assert.Equivalent(sessionUpdated.Message.Session, updated);
        // the manual mode is acknowledged without the member, which reads as a null
        var sent = Checkers.GetSentMessage(server, 0)["session"]!.AsObject();
        Assert.True(sent.ContainsKey("turn_detection"));
        Assert.Null(sent["turn_detection"]);
        Assert.Null(updated.TurnDetection);
        // the voice of the audio configuration wins over the legacy member
        Assert.Equal("Serena", updated.Voice);
        Assert.Equal("wav", updated.Audio!.Output!.Format!.Type);
        Assert.Equal("qwen3-asr-flash-realtime", updated.InputAudioTranscription!.Model);
        Assert.True(updated.SearchOptions!.EnableSource);
        Assert.Equal(1314, updated.Seed);
    }

    [Fact]
    public async Task UpdateSession_TranscriptionOff_SendExplicitNullAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.OmniRealtimeImage.SessionCreated;
        var sessionUpdated = Snapshots.OmniRealtimeImage.SessionUpdated;
        var sessionUpdate = Snapshots.OmniRealtimeImage.SessionUpdate;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(sessionUpdated.GetMessageJson()));
        using var session = new OmniRealtimeSocketSession(server);
        await session.StartAsync();

        // Act
        var updated = await session.UpdateSessionAsync(CreateImageSession());

        // Assert
        Assert.True(
            Checkers.IsJsonEquivalent(
                Checkers.NormalizeEventId(server, 0, sessionUpdate.GetMessageJson()),
                sessionUpdate.GetMessageJson()));
        Assert.Equivalent(sessionUpdated.Message.Session, updated);
        var sent = Checkers.GetSentMessage(server, 0)["session"]!.AsObject();
        Assert.True(sent.ContainsKey("input_audio_transcription"));
        Assert.Null(sent["input_audio_transcription"]);
        // the echo lacks the member, which reads as a null
        Assert.Null(updated.InputAudioTranscription);
    }

    [Fact]
    public async Task UpdateSession_FunctionTool_EchoedByServerAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.OmniRealtimeFunctionCall.SessionCreated;
        var sessionUpdated = Snapshots.OmniRealtimeFunctionCall.SessionUpdated;
        var sessionUpdate = Snapshots.OmniRealtimeFunctionCall.SessionUpdate;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(sessionUpdated.GetMessageJson()));
        using var session = new OmniRealtimeSocketSession(server);
        await session.StartAsync();

        // Act
        var updated = await session.UpdateSessionAsync(CreateFunctionCallSession());

        // Assert
        Assert.True(
            Checkers.IsJsonEquivalent(
                Checkers.NormalizeEventId(server, 0, sessionUpdate.GetMessageJson()),
                sessionUpdate.GetMessageJson()));
        Assert.Equivalent(sessionUpdated.Message.Session, updated);
        Assert.Equal("get_current_weather", updated.Tools![0].Function!.Name);
    }

    [Fact]
    public async Task UpdateSession_McpServer_EchoedWithoutUrlAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.OmniRealtimeMcp.SessionCreated;
        var sessionUpdated = Snapshots.OmniRealtimeMcp.SessionUpdated;
        var sessionUpdate = Snapshots.OmniRealtimeMcp.SessionUpdate;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(sessionUpdated.GetMessageJson()));
        using var session = new OmniRealtimeSocketSession(server);
        await session.StartAsync();

        // Act
        var updated = await session.UpdateSessionAsync(CreateMcpSession());

        // Assert
        Assert.True(
            Checkers.IsJsonEquivalent(
                Checkers.NormalizeEventId(server, 0, sessionUpdate.GetMessageJson()),
                sessionUpdate.GetMessageJson()));
        Assert.Equivalent(sessionUpdated.Message.Session, updated);
        // the server echoes the MCP server without its url
        var serverTool = Assert.Single(updated.Tools!);
        Assert.Equal("mcp", serverTool.Type);
        Assert.Equal("deepwiki", serverTool.ServerLabel);
        Assert.Null(serverTool.ServerUrl);
        Assert.Equal("always", serverTool.RequireApproval);
    }

    [Fact]
    public async Task UpdateSession_ServerRejectsValue_ThrowAndEndSessionAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.OmniRealtimeInvalidModalities.SessionCreated;
        var error = Snapshots.OmniRealtimeInvalidModalities.ServerError;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(error.GetMessageJson());
            // a rejected session.update ends the session: the server closes the connection right after
            await s.WriteServerCloseAsync();
        });
        using var session = new OmniRealtimeSocketSession(server);
        await session.StartAsync();

        // Act
        var ex = await Assert.ThrowsAsync<DashScopeException>(
            () => session.UpdateSessionAsync(new OmniRealtimeSession { Modalities = new List<string> { "audio" } }));
        server.CloseStatus = WebSocketCloseStatus.InvalidPayloadData;
        server.CloseStatusDescription = ex.Error!.Message;
        var ended = await Assert.ThrowsAsync<DashScopeException>(
            () => session.UpdateSessionAsync(new OmniRealtimeSession { TurnDetection = null }));

        // Assert
        Assert.Equal(400, ex.Status);
        Assert.Equal("invalid_value", ex.Error.Code);
        Assert.Equal(error.Message.Error!.Message, ex.Error.Message);
        // the session is over for good: the next wait gives up and tells how the server closed it
        Assert.Equal(0, ended.Status);
        Assert.Contains("(InvalidPayloadData: Invalid modalities", ended.Message);
    }

    [Fact]
    public async Task AppendAudio_VadMode_StreamsResponseTextAndAudioAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        await server.WriteServerMessageAsync(Snapshots.OmniRealtime.SessionCreated.GetMessageJson());
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(Snapshots.OmniRealtime.SessionUpdated.GetMessageJson()));
        for (var i = 0; i < 38; i++)
        {
            server.Playlist.Enqueue(_ => Task.CompletedTask); // appends have no ack, 123554 bytes / 3200
        }

        // the server fires the whole vad + response flow after the last audio chunk
        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(Snapshots.OmniRealtime.InputAudioBufferSpeechStarted.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtime.ConversationItemCreated.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtime.ConversationItemInputAudioTranscriptionDelta.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtime.InputAudioBufferSpeechStopped.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtime.InputAudioBufferCommitted.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtime.ConversationItemInputAudioTranscriptionDelta2.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtime.ConversationItemInputAudioTranscriptionCompleted.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtime.ResponseCreated.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtime.ResponseOutputItemAdded.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtime.ConversationItemCreated2.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtime.ResponseContentPartAdded.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtime.ResponseAudioTranscriptDelta.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtime.ResponseAudioDelta.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtime.ResponseAudioDelta2.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtime.ResponseAudioTranscriptDelta2.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtime.ResponseAudioTranscriptDone.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtime.ResponseAudioDone.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtime.ResponseContentPartDone.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtime.ResponseOutputItemDone.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtime.ResponseDone.GetMessageJson());
            await s.WriteServerCloseAsync();
        });
        using var session = new OmniRealtimeSocketSession(server);
        await session.StartAsync();
        await session.UpdateSessionAsync(
            new OmniRealtimeSession
            {
                Modalities = new List<string> { "text", "audio" },
                Voice = "Tina",
                Instructions = Instructions,
                TurnDetection = new OmniRealtimeTurnDetection
                {
                    Type = "server_vad",
                    Threshold = 0.5f,
                    SilenceDurationMs = 800
                }
            });

        // Act
        await using var audio = File.OpenRead(Path.Combine("RawHttpData", "socket-qwen-omni-realtime.input.pcm"));
        await session.AppendAudioAsync(audio);
        var events = await session.GetEventsAsync().ToListAsync();
        var audioBytes = await session.GetAudioAsync().ToListAsync();

        // Assert
        Assert.Equal(39, server.ServerReceivedMessages.Count - 1); // audio appends after the session.update
        var heard = events.First(e => e.Type == "conversation.item.input_audio_transcription.delta");
        Assert.Equal("伯", heard.Stash);
        Assert.Equal("zh", heard.Language);
        Assert.Equal("neutral", heard.Emotion);
        Assert.Equal(
            Snapshots.OmniRealtime.ConversationItemInputAudioTranscriptionCompleted.Message.Transcript,
            events.First(e => e.Type == "conversation.item.input_audio_transcription.completed").Transcript);
        Assert.Equal(3620, events.First(e => e.Type == "input_audio_buffer.speech_stopped").AudioEndMs);
        Assert.Equal(
            Snapshots.OmniRealtime.ResponseAudioTranscriptDone.Message.Transcript,
            events.First(e => e.Type == "response.audio_transcript.done").Transcript);
        var response = events[^1].Response!;
        Assert.Equal("response.done", events[^1].Type);
        Assert.Equal("completed", response.Status);
        Assert.Equal(Snapshots.OmniRealtime.ResponseDone.Message.Response!.Usage!.TotalTokens, response.Usage!.TotalTokens);
        // the base64 deltas are decoded into the audio stream, in arrival order (the snapshots
        // carry a placeholder, the payloads come from the fixtures)
        var expectedAudio = ReadDelta(Snapshots.OmniRealtime.ResponseAudioDelta)
            .Concat(ReadDelta(Snapshots.OmniRealtime.ResponseAudioDelta2)).ToArray();
        Assert.Equal(expectedAudio, audioBytes);
    }

    [Fact]
    public async Task CommitAudio_CreateResponse_ManualMode_ReturnResponseAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        await server.WriteServerMessageAsync(Snapshots.OmniRealtimeManual.SessionCreated.GetMessageJson());
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(Snapshots.OmniRealtimeManual.SessionUpdated.GetMessageJson()));
        for (var i = 0; i < 39; i++)
        {
            server.Playlist.Enqueue(_ => Task.CompletedTask); // appends have no ack, 123554 bytes / 3200
        }

        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeManual.ConversationItemCreated.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeManual.InputAudioBufferCommitted.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeManual.ConversationItemInputAudioTranscriptionCompleted.GetMessageJson());
        });
        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeManual.ResponseCreated.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeManual.ResponseOutputItemAdded.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeManual.ConversationItemCreated2.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeManual.ResponseContentPartAdded.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeManual.ResponseAudioTranscriptDelta.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeManual.ResponseAudioDelta.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeManual.ResponseAudioTranscriptDelta2.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeManual.ResponseAudioTranscriptDone.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeManual.ResponseDone.GetMessageJson());
            await s.WriteServerCloseAsync();
        });
        using var session = new OmniRealtimeSocketSession(server);
        await session.StartAsync();
        await session.UpdateSessionAsync(CreateFullSession());

        // Act
        await using var audio = File.OpenRead(Path.Combine("RawHttpData", "socket-qwen-omni-realtime-manual.input.pcm"));
        await session.AppendAudioAsync(audio);
        var itemId = await session.CommitAudioAsync();
        var response = await session.CreateResponseAsync();

        // Assert
        // session.update, 39 audio appends, the commit and the response.create
        Assert.Equal(42, server.ServerReceivedMessages.Count);
        Assert.True(
            Checkers.IsJsonEquivalent(
                Checkers.NormalizeEventId(server, 40, Snapshots.OmniRealtimeManual.CommitAudio.GetMessageJson()),
                Snapshots.OmniRealtimeManual.CommitAudio.GetMessageJson()));
        Assert.True(
            Checkers.IsJsonEquivalent(
                Checkers.NormalizeEventId(server, 41, Snapshots.OmniRealtimeManual.ResponseCreate.GetMessageJson()),
                Snapshots.OmniRealtimeManual.ResponseCreate.GetMessageJson()));
        Assert.Equal(Snapshots.OmniRealtimeManual.InputAudioBufferCommitted.Message.ItemId, itemId);
        Assert.Equivalent(Snapshots.OmniRealtimeManual.ResponseDone.Message.Response, response);
        Assert.Equal("completed", response.Status);
        Assert.Equal("Serena", response.Voice);
        Assert.Equal(
            Snapshots.OmniRealtimeManual.ResponseDone.Message.Response!.Output![0].Content![0].Transcript,
            response.Output![0].Content![0].Transcript);
    }

    [Fact]
    public async Task AppendImage_CommitAudio_CreateResponse_ImageTokensInUsageAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        await server.WriteServerMessageAsync(Snapshots.OmniRealtimeImage.SessionCreated.GetMessageJson());
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(Snapshots.OmniRealtimeImage.SessionUpdated.GetMessageJson()));
        for (var i = 0; i < 22; i++)
        {
            server.Playlist.Enqueue(_ => Task.CompletedTask); // appends have no ack, 69692 bytes / 3200
        }

        server.Playlist.Enqueue(_ => Task.CompletedTask); // the image append has no ack either
        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeImage.ConversationItemCreated.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeImage.InputAudioBufferCommitted.GetMessageJson());
        });
        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeImage.ResponseCreated.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeImage.ResponseOutputItemAdded.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeImage.ConversationItemCreated2.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeImage.ResponseContentPartAdded.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeImage.ResponseAudioTranscriptDone.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeImage.ResponseDone.GetMessageJson());
            await s.WriteServerCloseAsync();
        });
        using var session = new OmniRealtimeSocketSession(server);
        await session.StartAsync();
        await session.UpdateSessionAsync(CreateImageSession());
        await using var audio = File.OpenRead(Path.Combine("RawHttpData", "socket-qwen-omni-realtime-image.input.pcm"));
        await session.AppendAudioAsync(audio);
        var frame = await File.ReadAllBytesAsync(Path.Combine("RawHttpData", "socket-qwen-omni-realtime-image.image.jpg"));

        // Act
        await session.AppendImageAsync(frame);
        var itemId = await session.CommitAudioAsync();
        var response = await session.CreateResponseAsync();

        // Assert
        // session.update, 22 audio appends, the image append, the commit and the response.create
        Assert.Equal(26, server.ServerReceivedMessages.Count);
        var sentImage = Checkers.GetSentMessage(server, 23);
        Assert.Equal("input_image_buffer.append", (string?)sentImage["type"]);
        Assert.Equal(Convert.ToBase64String(frame), (string?)sentImage["image"]);
        Assert.Equal(Snapshots.OmniRealtimeImage.InputAudioBufferCommitted.Message.ItemId, itemId);
        Assert.Equivalent(Snapshots.OmniRealtimeImage.ResponseDone.Message.Response, response);
        // the image is billed as video tokens
        Assert.Equal(128, response.Usage!.InputTokensDetails!.VideoTokens);
    }

    [Fact]
    public async Task CreateItem_TextMessage_CreateResponse_ReturnAnswerAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        await server.WriteServerMessageAsync(Snapshots.OmniRealtimeText.SessionCreated.GetMessageJson());
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(Snapshots.OmniRealtimeText.SessionUpdated.GetMessageJson()));
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(Snapshots.OmniRealtimeText.ConversationItemCreated.GetMessageJson()));
        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeText.ResponseCreated.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeText.ResponseOutputItemAdded.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeText.ConversationItemCreated2.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeText.ResponseContentPartAdded.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeText.ResponseAudioTranscriptDone.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeText.ResponseDone.GetMessageJson());
            await s.WriteServerCloseAsync();
        });
        using var session = new OmniRealtimeSocketSession(server);
        await session.StartAsync();
        await session.UpdateSessionAsync(CreateManualSession());

        // Act
        var created = await session.CreateItemAsync(
            new OmniRealtimeItem
            {
                Type = "message",
                Role = "user",
                Content = new List<OmniRealtimeItemContent> { new(Type: "input_text", Text: "用一句话介绍博客园。") }
            });
        var response = await session.CreateResponseAsync();

        // Assert
        Assert.True(
            Checkers.IsJsonEquivalent(
                Checkers.NormalizeEventId(server, 1, Snapshots.OmniRealtimeText.ItemCreate.GetMessageJson()),
                Snapshots.OmniRealtimeText.ItemCreate.GetMessageJson()));
        Assert.True(
            Checkers.IsJsonEquivalent(
                Checkers.NormalizeEventId(server, 2, Snapshots.OmniRealtimeText.ResponseCreate.GetMessageJson()),
                Snapshots.OmniRealtimeText.ResponseCreate.GetMessageJson()));
        Assert.Equivalent(Snapshots.OmniRealtimeText.ConversationItemCreated.Message, created);
        Assert.Equal("completed", created.Item!.Status);
        Assert.Equivalent(Snapshots.OmniRealtimeText.ResponseDone.Message.Response, response);
    }

    [Fact]
    public async Task CreateItem_FunctionCallOutput_CreateResponse_ContinueWithAnswerAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        await server.WriteServerMessageAsync(Snapshots.OmniRealtimeFunctionCall.SessionCreated.GetMessageJson());
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(Snapshots.OmniRealtimeFunctionCall.SessionUpdated.GetMessageJson()));
        for (var i = 0; i < 20; i++)
        {
            server.Playlist.Enqueue(_ => Task.CompletedTask); // appends have no ack, 62266 bytes / 3200
        }

        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeFunctionCall.ConversationItemCreated.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeFunctionCall.InputAudioBufferCommitted.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeFunctionCall.ConversationItemInputAudioTranscriptionCompleted.GetMessageJson());
        });
        // the first response asks for the function call
        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeFunctionCall.ResponseCreated.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeFunctionCall.ResponseOutputItemAdded.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeFunctionCall.ConversationItemCreated2.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeFunctionCall.ResponseFunctionCallArgumentsDelta.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeFunctionCall.ResponseFunctionCallArgumentsDelta2.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeFunctionCall.ResponseFunctionCallArgumentsDone.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeFunctionCall.ResponseOutputItemDone.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeFunctionCall.ResponseDone.GetMessageJson());
        });
        server.Playlist.Enqueue(async s =>
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeFunctionCall.ConversationItemCreated3.GetMessageJson()));
        // the second one answers in text with the result of the call
        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeFunctionCall.ResponseCreated2.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeFunctionCall.ResponseOutputItemAdded2.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeFunctionCall.ConversationItemCreated4.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeFunctionCall.ResponseContentPartAdded.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeFunctionCall.ResponseTextDelta.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeFunctionCall.ResponseTextDelta2.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeFunctionCall.ResponseTextDone.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeFunctionCall.ResponseContentPartDone.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeFunctionCall.ResponseOutputItemDone2.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeFunctionCall.ResponseDone2.GetMessageJson());
            await s.WriteServerCloseAsync();
        });
        using var session = new OmniRealtimeSocketSession(server);
        await session.StartAsync();
        await session.UpdateSessionAsync(CreateFunctionCallSession());
        await using var audio =
            File.OpenRead(Path.Combine("RawHttpData", "socket-qwen-omni-realtime-function-call.input.pcm"));
        await session.AppendAudioAsync(audio);
        await session.CommitAudioAsync();

        // Act
        var callResponse = await session.CreateResponseAsync();
        var call = callResponse.Output![0];
        var created = await session.CreateItemAsync(
            new OmniRealtimeItem
            {
                Type = "function_call_output",
                CallId = call.CallId,
                Output = "北京今天天气为霾转晴，气温4/-4℃，微风"
            });
        var answer = await session.CreateResponseAsync();

        // Assert
        Assert.Equivalent(Snapshots.OmniRealtimeFunctionCall.ResponseDone.Message.Response, callResponse);
        Assert.Equal("function_call", call.Type);
        Assert.Equal("get_current_weather", call.Name);
        Assert.Equal(Snapshots.OmniRealtimeFunctionCall.ResponseDone.Message.Response!.Output![0].Arguments, call.Arguments);
        Assert.Contains("\"location\"", call.Arguments);
        // session.update, 20 appends, the commit, 2 response.create and the conversation.item.create
        Assert.Equal(25, server.ServerReceivedMessages.Count);
        Assert.True(
            Checkers.IsJsonEquivalent(
                Checkers.NormalizeEventId(server, 23, Snapshots.OmniRealtimeFunctionCall.ItemCreate.GetMessageJson()),
                Snapshots.OmniRealtimeFunctionCall.ItemCreate.GetMessageJson()));
        Assert.Equivalent(Snapshots.OmniRealtimeFunctionCall.ConversationItemCreated3.Message, created);
        Assert.Equal("function_call_output", created.Item!.Type);
        Assert.Equivalent(Snapshots.OmniRealtimeFunctionCall.ResponseDone2.Message.Response, answer);
        var text = Assert.Single(Assert.Single(answer.Output!).Content!);
        Assert.Equal("text", text.Type);
        Assert.Equal(Snapshots.OmniRealtimeFunctionCall.ResponseTextDone.Message.Text, text.Text);
    }

    [Fact]
    public async Task RespondToMcpApproval_Approve_ExecuteToolThenAnswerAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        await server.WriteServerMessageAsync(Snapshots.OmniRealtimeMcp.SessionCreated.GetMessageJson());
        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcp.McpListToolsInProgress.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcp.SessionUpdated.GetMessageJson());
        });
        for (var i = 0; i < 47; i++)
        {
            server.Playlist.Enqueue(_ => Task.CompletedTask); // appends have no ack, 148370 bytes / 3200
        }

        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcp.ConversationItemCreated.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcp.ConversationItemCreated2.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcp.McpListToolsCompleted.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcp.InputAudioBufferCommitted.GetMessageJson());
        });
        // the response stops at the approval request until the client answers it
        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcp.ResponseCreated.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcp.ResponseOutputItemAdded.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcp.ConversationItemCreated3.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcp.ResponseMcpCallArgumentsDelta.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcp.ResponseMcpCallArgumentsDelta2.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcp.ResponseMcpCallArgumentsDone.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcp.ConversationItemCreated4.GetMessageJson());
        });
        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcp.ResponseMcpCallInProgress.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcp.ResponseMcpCallCompleted.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcp.ResponseOutputItemDone.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcp.ResponseDone.GetMessageJson());
        });
        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcp.ResponseCreated2.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcp.ResponseOutputItemAdded2.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcp.ConversationItemCreated5.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcp.ResponseContentPartAdded.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcp.ResponseAudioTranscriptDone.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcp.ResponseDone2.GetMessageJson());
            await s.WriteServerCloseAsync();
        });
        using var session = new OmniRealtimeSocketSession(server);
        await session.StartAsync();
        await session.UpdateSessionAsync(CreateMcpSession());
        await using var audio = File.OpenRead(Path.Combine("RawHttpData", "socket-qwen-omni-realtime-mcp.input.pcm"));
        await session.AppendAudioAsync(audio);
        await session.CommitAudioAsync();

        // Act
        var responseTask = session.CreateResponseAsync();
        string? approvalRequestId = null;
        await foreach (var e in session.GetEventsAsync())
        {
            if (e.Type == "conversation.item.created" && e.Item?.Type == "mcp_approval_request")
            {
                approvalRequestId = e.Item.Id;
                break;
            }
        }

        await session.RespondToMcpApprovalAsync(approvalRequestId!);
        var toolResponse = await responseTask;
        var answer = await session.CreateResponseAsync();
        var rest = await session.GetEventsAsync().ToListAsync();

        // Assert
        Assert.Equal(Snapshots.OmniRealtimeMcp.ConversationItemCreated4.Message.Item!.Id, approvalRequestId);
        // session.update, 47 appends, the commit, 2 response.create and the approval response
        Assert.Equal(52, server.ServerReceivedMessages.Count);
        Assert.True(
            Checkers.IsJsonEquivalent(
                Checkers.NormalizeEventId(server, 50, Snapshots.OmniRealtimeMcp.McpApprovalResponse.GetMessageJson()),
                Snapshots.OmniRealtimeMcp.McpApprovalResponse.GetMessageJson()));
        Assert.Equivalent(Snapshots.OmniRealtimeMcp.ResponseDone.Message.Response, toolResponse);
        var toolCall = toolResponse.Output![0];
        Assert.Equal("mcp_call", toolCall.Type);
        Assert.Equal("read_wiki_structure", toolCall.Name);
        Assert.Equal("deepwiki", toolCall.ServerLabel);
        Assert.Contains("Available pages for facebook/react", toolCall.Output);
        Assert.Equivalent(Snapshots.OmniRealtimeMcp.ResponseDone2.Message.Response, answer);
        // the follow-up response was created after the events read so far
        Assert.Contains(rest, e => e.Type == "response.created" && e.Response!.Id == answer.Id);
    }

    [Fact]
    public async Task RespondToMcpApproval_Reject_FailTheCallAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        await server.WriteServerMessageAsync(Snapshots.OmniRealtimeMcpRejected.SessionCreated.GetMessageJson());
        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcpRejected.McpListToolsInProgress.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcpRejected.SessionUpdated.GetMessageJson());
        });
        for (var i = 0; i < 47; i++)
        {
            server.Playlist.Enqueue(_ => Task.CompletedTask); // appends have no ack, 148370 bytes / 3200
        }

        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcpRejected.ConversationItemCreated.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcpRejected.InputAudioBufferCommitted.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcpRejected.ConversationItemCreated2.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcpRejected.McpListToolsCompleted.GetMessageJson());
        });
        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcpRejected.ResponseCreated.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcpRejected.ResponseOutputItemAdded.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcpRejected.ConversationItemCreated3.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcpRejected.ResponseMcpCallArgumentsDelta.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcpRejected.ResponseMcpCallArgumentsDelta2.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcpRejected.ResponseMcpCallArgumentsDone.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcpRejected.ConversationItemCreated4.GetMessageJson());
        });
        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcpRejected.ResponseMcpCallFailed.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcpRejected.ResponseOutputItemDone.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeMcpRejected.ResponseDone.GetMessageJson());
            await s.WriteServerCloseAsync();
        });
        using var session = new OmniRealtimeSocketSession(server);
        await session.StartAsync();
        await session.UpdateSessionAsync(CreateMcpSession());
        await using var audio =
            File.OpenRead(Path.Combine("RawHttpData", "socket-qwen-omni-realtime-mcp-rejected.input.pcm"));
        await session.AppendAudioAsync(audio);
        await session.CommitAudioAsync();

        // Act
        var responseTask = session.CreateResponseAsync();
        string? approvalRequestId = null;
        await foreach (var e in session.GetEventsAsync())
        {
            if (e.Type == "conversation.item.created" && e.Item?.Type == "mcp_approval_request")
            {
                approvalRequestId = e.Item.Id;
                break;
            }
        }

        await session.RespondToMcpApprovalAsync(approvalRequestId!, false);
        var response = await responseTask;

        // Assert
        Assert.Equal(Snapshots.OmniRealtimeMcpRejected.ConversationItemCreated4.Message.Item!.Id, approvalRequestId);
        Assert.True(
            Checkers.IsJsonEquivalent(
                Checkers.NormalizeEventId(server, 50, Snapshots.OmniRealtimeMcpRejected.McpApprovalResponse.GetMessageJson()),
                Snapshots.OmniRealtimeMcpRejected.McpApprovalResponse.GetMessageJson()));
        Assert.False(Checkers.GetSentMessage(server, 50)["item"]!["approve"]!.GetValue<bool>());
        Assert.Equivalent(Snapshots.OmniRealtimeMcpRejected.ResponseDone.Message.Response, response);
        var toolCall = response.Output![0];
        Assert.Equal("failed", toolCall.Status);
        Assert.Equal("tool_execution_error", toolCall.Error!.Type);
        Assert.Equal("MCP tool call failed (approval_rejected).", toolCall.Error.Message);
    }

    [Fact]
    public async Task CancelResponse_ResponseInProgress_ReturnCancelledResponseAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        await server.WriteServerMessageAsync(Snapshots.OmniRealtimeCancel.SessionCreated.GetMessageJson());
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(Snapshots.OmniRealtimeCancel.SessionUpdated.GetMessageJson()));
        for (var i = 0; i < 39; i++)
        {
            server.Playlist.Enqueue(_ => Task.CompletedTask); // appends have no ack, 123554 bytes / 3200
        }

        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeCancel.ConversationItemCreated.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeCancel.InputAudioBufferCommitted.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeCancel.ConversationItemInputAudioTranscriptionCompleted.GetMessageJson());
        });
        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeCancel.ResponseCreated.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeCancel.ResponseOutputItemAdded.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeCancel.ConversationItemCreated2.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeCancel.ResponseContentPartAdded.GetMessageJson());
        });
        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeCancel.ResponseAudioDelta.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeCancel.ResponseAudioDelta2.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeCancel.ResponseAudioTranscriptDone.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeCancel.ResponseAudioDone.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeCancel.ResponseContentPartDone.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeCancel.ResponseOutputItemDone.GetMessageJson());
            await s.WriteServerMessageAsync(Snapshots.OmniRealtimeCancel.ResponseDone.GetMessageJson());
            await s.WriteServerCloseAsync();
        });
        using var session = new OmniRealtimeSocketSession(server);
        await session.StartAsync();
        await session.UpdateSessionAsync(CreateManualSession());
        await using var audio = File.OpenRead(Path.Combine("RawHttpData", "socket-qwen-omni-realtime-cancel.input.pcm"));
        await session.AppendAudioAsync(audio);
        await session.CommitAudioAsync();
        var createTask = session.CreateResponseAsync();

        // Act
        var cancelled = await session.CancelResponseAsync();
        var created = await createTask;

        // Assert
        Assert.True(
            Checkers.IsJsonEquivalent(
                Checkers.NormalizeEventId(server, 42, Snapshots.OmniRealtimeCancel.ResponseCancel.GetMessageJson()),
                Snapshots.OmniRealtimeCancel.ResponseCancel.GetMessageJson()));
        Assert.Equivalent(Snapshots.OmniRealtimeCancel.ResponseDone.Message.Response, cancelled);
        Assert.Equal("cancelled", cancelled.Status);
        Assert.Equal("client_cancelled", cancelled.StatusDetails!.Reason);
        Assert.Equal("incomplete", cancelled.Output![0].Status);
        Assert.Null(cancelled.Usage);
        // the create waiting for the response ends with the cancelled one as well
        Assert.Same(cancelled, created);
    }

    [Fact]
    public async Task CreateResponse_ServerEndsSession_ThrowAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.OmniRealtime.SessionCreated;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        // the server closes the connection instead of answering the response.create
        server.Playlist.Enqueue(async s => await s.WriteServerCloseAsync());
        using var session = new OmniRealtimeSocketSession(server);
        await session.StartAsync();

        // Act
        var ex = await Assert.ThrowsAsync<DashScopeException>(() => session.CreateResponseAsync());

        // Assert
        // the wait gives up instead of staying pending forever
        Assert.Equal(0, ex.Status);
        Assert.Contains("response.done", ex.Message);
    }

    [Fact]
    public async Task CreateItem_McpApprovalResponse_ThrowAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        await server.WriteServerMessageAsync(Snapshots.OmniRealtimeMcp.SessionCreated.GetMessageJson());
        using var session = new OmniRealtimeSocketSession(server);
        await session.StartAsync();
        var item = new OmniRealtimeItem { Type = "mcp_approval_response", ApprovalRequestId = "mcpr_x", Approve = true };

        // Act
        var ex = await Assert.ThrowsAsync<ArgumentException>(() => session.CreateItemAsync(item));

        // Assert
        // the server does not confirm this one, waiting for it would never end
        Assert.Equal("item", ex.ParamName);
        Assert.Empty(server.ServerReceivedMessages);
    }

    [Fact]
    public async Task CreateOmniRealtimeSocketSession_ConnectWithModel_AwaitSessionCreatedAsync()
    {
        // Arrange
        var fakeSocket = new FakeClientWebSocket();
        await fakeSocket.WriteServerMessageAsync(Snapshots.OmniRealtime.SessionCreated.GetMessageJson());
        var pool = new DashScopeClientWebSocketPool(
            new DashScopeOptions { WorkspaceId = "llm-test" },
            null,
            (_, _, _) => fakeSocket);
        var client = new DashScopeClientCore(new HttpClient(), pool);

        // Act
        using var session = await client.CreateOmniRealtimeSocketSessionAsync("qwen3.8-omni-flash-realtime");

        // Assert
        Assert.Equal(
            new Uri("wss://llm-test.cn-beijing.maas.aliyuncs.com/api-ws/v1/realtime?model=qwen3.8-omni-flash-realtime"),
            fakeSocket.ConnectedUri);
        var first = await session.GetEventsAsync().FirstAsync();
        Assert.Equivalent(Snapshots.OmniRealtime.SessionCreated.Message, first);
    }

    [Theory]
    [MemberData(nameof(ServerEvents))]
    public async Task ServerEvents_CapturedEvents_DeserializeIntoSnapshotsAsync(SocketMessageSnapshot<OmniRealtimeServerEvent> snapshot)
    {
        // Arrange
        var json = snapshot.GetMessageJson();

        // Act
        var actual = JsonSerializer.Deserialize<OmniRealtimeServerEvent>(json, DashScopeDefaults.SerializationOptions)!;

        // Assert
        // every captured event of every group reads into the typed model, member by member
        Assert.Equal(snapshot.Message.Type, actual.Type);
        if (snapshot.Message.Delta == "<base64>")
        {
            // the fixtures keep the real audio payload, the snapshots the placeholder
            actual = actual with { Delta = "<base64>" };
        }

        AssertJsonEquivalent(snapshot.Message, actual);
    }

    private static byte[] ReadDelta(SocketMessageSnapshot<OmniRealtimeServerEvent> snapshot)
        => Convert.FromBase64String(JsonNode.Parse(snapshot.GetMessageJson())!["delta"]!.GetValue<string>());

    private static void AssertJsonEquivalent(OmniRealtimeServerEvent expected, OmniRealtimeServerEvent actual)
    {
        // both go through the same serializer, so the comparison is about the members and their values
        var expectedJson = JsonSerializer.Serialize(expected, DashScopeDefaults.SerializationOptions);
        var actualJson = JsonSerializer.Serialize(actual, DashScopeDefaults.SerializationOptions);
        Assert.True(
            JsonNode.DeepEquals(JsonNode.Parse(expectedJson), JsonNode.Parse(actualJson)),
            $"Expected {expected.Type}:{Environment.NewLine}{expectedJson}{Environment.NewLine}Actual:{Environment.NewLine}{actualJson}");
    }

    private static TheoryData<SocketMessageSnapshot<OmniRealtimeServerEvent>> CreateServerEventData()
    {
        var data = new TheoryData<SocketMessageSnapshot<OmniRealtimeServerEvent>>();
        var groups = typeof(Snapshots).GetNestedTypes(BindingFlags.Public)
            .Where(t => t.Name.StartsWith("OmniRealtime", StringComparison.Ordinal));
        foreach (var field in groups.SelectMany(g => g.GetFields(BindingFlags.Public | BindingFlags.Static)))
        {
            if (field.FieldType == typeof(SocketMessageSnapshot<OmniRealtimeServerEvent>))
            {
                data.Add((SocketMessageSnapshot<OmniRealtimeServerEvent>)field.GetValue(null)!);
            }
        }

        return data;
    }

    private static OmniRealtimeSession CreateManualSession()
        => new()
        {
            Modalities = new List<string> { "text", "audio" },
            Voice = "Tina",
            Instructions = Instructions,
            TurnDetection = null
        };

    private static OmniRealtimeSession CreateImageSession()
        => new()
        {
            Modalities = new List<string> { "text", "audio" },
            Voice = "Tina",
            InputAudioTranscription = null,
            Instructions = Instructions,
            TurnDetection = null
        };

    private static OmniRealtimeSession CreateFullSession()
        => new()
        {
            Modalities = new List<string> { "text", "audio" },
            Voice = "Tina",
            Audio = new OmniRealtimeAudio(
                Input: new OmniRealtimeAudioInput(
                    Format: new OmniRealtimeAudioFormat(
                        Type: "pcm",
                        SampleRate: 16000,
                        SampleFormat: "s16le",
                        Channels: 1,
                        Packing: "interleaved",
                        ChannelLayout: "mono")),
                Output: new OmniRealtimeAudioOutput(
                    Voice: "Serena",
                    Format: new OmniRealtimeAudioFormat(Type: "wav", SampleRate: 24000))),
            InputAudioTranscription = new OmniRealtimeTranscription(Model: "qwen3-asr-flash-realtime"),
            Instructions = Instructions,
            TurnDetection = null,
            EnableSearch = false,
            SearchOptions = new OmniRealtimeSearchOptions(EnableSource: true),
            Video = new OmniRealtimeVideo(Input: new OmniRealtimeVideoInput(RepresentationCompact: "none")),
            Temperature = 0.6f,
            TopP = 0.95f,
            TopK = 20,
            MaxTokens = 16384,
            RepetitionPenalty = 0,
            PresencePenalty = 1,
            Seed = 1314
        };

    private static OmniRealtimeSession CreateFunctionCallSession()
        => new()
        {
            Modalities = new List<string> { "text" },
            Voice = "Tina",
            Instructions = Instructions,
            TurnDetection = null,
            Tools = new List<OmniRealtimeTool>
            {
                new(
                    Type: "function",
                    Function: new OmniRealtimeFunction(
                        "get_current_weather",
                        "当你想查询指定城市的天气时非常有用。",
                        JsonSerializer.SerializeToElement(new
                        {
                            type = "object",
                            properties = new
                            {
                                location = new { type = "string", description = "城市或县区，比如北京市、杭州市、余杭区等。" }
                            },
                            required = new[] { "location" }
                        })))
            }
        };

    private static OmniRealtimeSession CreateMcpSession()
        => new()
        {
            Modalities = new List<string> { "text", "audio" },
            Voice = "Tina",
            Instructions = "你是技术助理，涉及 GitHub 仓库的问题必须先调用 deepwiki 工具查询，不要凭记忆回答。",
            TurnDetection = null,
            Tools = new List<OmniRealtimeTool>
            {
                new(
                    Type: "mcp",
                    ServerLabel: "deepwiki",
                    ServerUrl: "https://mcp.deepwiki.com/mcp",
                    RequireApproval: "always")
            }
        };
}
