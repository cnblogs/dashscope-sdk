using System.Text.Json;
using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;

namespace Cnblogs.DashScope.Sdk.UnitTests;

[Collection(nameof(SocketTestsCollection))]
public class TtsRealtimeSocketSessionTests
{
    [Fact]
    public async Task Connect_WaitSessionCreated_SuccessAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.TtsRealtime.SessionCreated;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        using var session = new TtsRealtimeSocketSession(server);

        // Act
        var sessionConfig = await session.StartAsync();

        // Assert
        Assert.Equivalent(sessionCreated.Message.Session, sessionConfig);
    }

    [Fact]
    public async Task UpdateSession_ApplyVoiceAndFormat_SuccessAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.TtsRealtime.SessionCreated;
        var sessionUpdated = Snapshots.TtsRealtime.SessionUpdated;
        var sessionUpdate = Snapshots.TtsRealtime.SessionUpdate;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(sessionUpdated.GetMessageJson()));
        using var session = new TtsRealtimeSocketSession(server);
        await session.StartAsync();

        // Act
        var updated = await session.UpdateSessionAsync(
            new TtsRealtimeSession
            {
                Voice = "Cherry",
                Mode = "server_commit",
                LanguageType = "Chinese",
                ResponseFormat = "pcm",
                SampleRate = 24000
            });

        // Assert
        Assert.True(Checkers.IsJsonEquivalent(
            NormalizeEventId(server, 0, sessionUpdate.GetMessageJson()),
            sessionUpdate.GetMessageJson()));
        Assert.Equivalent(sessionUpdated.Message.Session, updated);
    }

    [Fact]
    public async Task AppendText_ThenCommitText_SuccessAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.TtsRealtime.SessionCreated;
        var appendText = Snapshots.TtsRealtime.AppendText;
        var commitText = Snapshots.TtsRealtime.CommitText;
        var committed = Snapshots.TtsRealtime.Committed;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        server.Playlist.Enqueue(async _ => await Task.CompletedTask); // append has no ack
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(committed.GetMessageJson()));
        using var session = new TtsRealtimeSocketSession(server);
        await session.StartAsync();

        // Act
        await session.AppendTextAsync("您好，我是千问。");
        var itemId = await session.CommitTextAsync();

        // Assert
        Assert.True(Checkers.IsJsonEquivalent(
            NormalizeEventId(server, 0, appendText.GetMessageJson()),
            appendText.GetMessageJson()));
        Assert.True(Checkers.IsJsonEquivalent(
            NormalizeEventId(server, 1, commitText.GetMessageJson()),
            commitText.GetMessageJson()));
        Assert.Equal(committed.Message.ItemId, itemId);
    }

    [Fact]
    public async Task FinishSession_DecodeAudioDelta_SuccessAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.TtsRealtime.SessionCreated;
        var sessionUpdated = Snapshots.TtsRealtime.SessionUpdated;
        var committed = Snapshots.TtsRealtime.Committed;
        var audioDelta = Snapshots.TtsRealtime.AudioDelta;
        var responseDone = Snapshots.TtsRealtime.ResponseDone;
        var sessionFinished = Snapshots.TtsRealtime.SessionFinished;
        var finishSession = Snapshots.TtsRealtime.FinishSession;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(sessionUpdated.GetMessageJson()));
        server.Playlist.Enqueue(async _ => await Task.CompletedTask); // append has no ack
        server.Playlist.Enqueue(
            async s =>
            {
                await s.WriteServerMessageAsync(committed.GetMessageJson());
                await s.WriteServerMessageAsync(audioDelta.GetMessageJson());
                await s.WriteServerMessageAsync(responseDone.GetMessageJson());
            });
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(sessionFinished.GetMessageJson()));
        using var session = new TtsRealtimeSocketSession(server);
        await session.StartAsync();
        await session.UpdateSessionAsync(new TtsRealtimeSession { Voice = "Cherry" });

        // Act
        await session.AppendTextAsync("您好，我是千问。");
        await session.CommitTextAsync();
        await session.FinishAsync();
        var audio = await session.GetAudioAsync().ToArrayAsync();
        var events = await session.GetEventsAsync().ToListAsync();

        // Assert
        Assert.Equal(Convert.FromBase64String(Snapshots.TtsRealtime.AudioDeltaBase64), audio);
        Assert.Equal(6, events.Count); // created, updated, committed, delta, done, finished
        Assert.Equivalent(responseDone.Message, events[^2]);
        Assert.Equivalent(sessionFinished.Message, events[^1]);
        Assert.True(Checkers.IsJsonEquivalent(
            NormalizeEventId(server, 3, finishSession.GetMessageJson()),
            finishSession.GetMessageJson()));
    }

    [Fact]
    public async Task UpdateSession_ServerError_ThrowAsync()
    {
        // Arrange
        var server = new FakeClientWebSocket();
        var sessionCreated = Snapshots.TtsRealtime.SessionCreated;
        await server.WriteServerMessageAsync(sessionCreated.GetMessageJson());
        server.Playlist.Enqueue(
            async s => await s.WriteServerMessageAsync(
                "{\"event_id\":\"event_QzAVZRVa9hKqM5VOaHunh\",\"type\":\"error\",\"error\":{\"code\":\"invalid_value\",\"message\":\"Session update error: session already started or finished or failed.\"}}"));
        using var session = new TtsRealtimeSocketSession(server);
        await session.StartAsync();

        // Act
        // ReSharper disable once AccessToDisposedClosure
        var act = () => session.UpdateSessionAsync(new TtsRealtimeSession { Voice = "Cherry" });

        // Assert
        var exception = await Assert.ThrowsAsync<DashScopeException>(act);
        Assert.Equal("invalid_value", exception.Error?.Code);
    }

    private static ArraySegment<byte> NormalizeEventId(FakeClientWebSocket server, int index, string fixtureJson)
    {
        var sentJson = System.Text.Encoding.UTF8.GetString(server.ServerReceivedMessages[index].ToArray());
        var sentId = JsonDocument.Parse(sentJson).RootElement.GetProperty("event_id").GetString()!;
        var fixtureId = JsonDocument.Parse(fixtureJson).RootElement.GetProperty("event_id").GetString()!;
        return new ArraySegment<byte>(System.Text.Encoding.UTF8.GetBytes(sentJson.Replace(sentId, fixtureId)));
    }
}
