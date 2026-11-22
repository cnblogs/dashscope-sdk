using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;

namespace Cnblogs.DashScope.Sdk.UnitTests;

[Collection(nameof(SocketTestsCollection))]
public class FunAsrRealtimeSocketTests
{
    [Fact]
    public async Task RunTask_FunAsrRealtime_WithContextAndLanguageHints_SuccessAsync()
    {
        // Arrange
        var (client, _, server) = await Sut.GetSocketTestClientAsync();
        var runTask = Snapshots.FunAsrRealtime.RunTask;
        server.Playlist.Enqueue(
            async s => await s.WriteServerMessageAsync(Snapshots.FunAsrRealtime.TaskStarted.GetMessageJson()));

        // Act
        using var session = await client.CreateSpeechRecognitionSocketSessionAsync(runTask.Message.Payload.Model!);
        var taskId = await session.RunTaskAsync(
            runTask.Message.Header.TaskId,
            runTask.Message.Payload.Parameters!,
            runTask.Message.Payload.Input);

        // Assert
        Assert.Equal(runTask.Message.Header.TaskId, taskId);
        Assert.True(Checkers.IsJsonEquivalent(server.ServerReceivedMessages.First(), runTask.GetMessageJson()));
    }

    [Fact]
    public async Task FullFlow_FunAsrRealtime_IntermediateResultsAndUsage_SuccessAsync()
    {
        // Arrange
        var (client, _, server) = await Sut.GetSocketTestClientAsync();
        var runTask = Snapshots.FunAsrRealtime.RunTask;
        var taskStarted = Snapshots.FunAsrRealtime.TaskStarted;
        var resultBegin = Snapshots.FunAsrRealtime.ResultGeneratedBegin;
        var resultFinal = Snapshots.FunAsrRealtime.ResultGeneratedFinal;
        var taskFinished = Snapshots.FunAsrRealtime.TaskFinished;
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(taskStarted.GetMessageJson()));
        server.Playlist.Enqueue(_ => Task.CompletedTask); // binary audio
        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(resultBegin.GetMessageJson());
            await s.WriteServerMessageAsync(resultFinal.GetMessageJson());
            await s.WriteServerMessageAsync(taskFinished.GetMessageJson());
            await s.WriteServerCloseAsync();
        });

        // Act
        using var session = await client.CreateSpeechRecognitionSocketSessionAsync(runTask.Message.Payload.Model!);
        var taskId = await session.RunTaskAsync(runTask.Message.Payload.Parameters!, runTask.Message.Payload.Input);
        await session.SendAudioAsync(Snapshots.FunAsrRealtime.InputAudio);
        await session.FinishTaskAsync(taskId);
        var jsonEvents = await session.GetMessagesAsync().ToListAsync();

        // Assert
        Assert.Equal(3, server.ServerReceivedMessages.Count); // run-task, audio, finish-task
        Assert.Equal(Snapshots.FunAsrRealtime.InputAudio, server.ServerReceivedMessages[1].ToArray());
        Assert.Equal(4, jsonEvents.Count); // task-started, sentence begin, final result, task-finished
        Assert.Equivalent(taskStarted.Message, jsonEvents[0]);
        Assert.Equivalent(resultBegin.Message, jsonEvents[1]);
        Assert.Equivalent(resultFinal.Message, jsonEvents[2]);
        Assert.Equivalent(taskFinished.Message, jsonEvents[3]);

        var begin = jsonEvents[1].Payload.Output!.Sentence!;
        Assert.True(begin.SentenceBegin);
        Assert.Equal(0, begin.ChannelId);
        Assert.Null(begin.SpeakerId);
        Assert.Equal(string.Empty, begin.Text);

        var final = jsonEvents[2].Payload.Output!.Sentence!;
        Assert.True(final.SentenceEnd);
        Assert.Equal("博客园成立于2004年，代码改变世界。", final.Text);
        Assert.All(final.Words!, w => Assert.True(w.Fixed));
        Assert.Equal(new DashScopeWebSocketResponseUsage(0, 3), jsonEvents[2].Payload.Usage);
        Assert.Null(jsonEvents[3].Payload.Usage);
    }
}
