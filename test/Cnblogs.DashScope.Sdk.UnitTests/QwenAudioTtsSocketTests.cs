using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;

namespace Cnblogs.DashScope.Sdk.UnitTests;

[Collection(nameof(SocketTestsCollection))]
public class QwenAudioTtsSocketTests
{
    [Fact]
    public async Task RunTask_QwenAudioTts_SuccessAsync()
    {
        // Arrange
        var (client, _, server) = await Sut.GetSocketTestClientAsync();
        var runTask = Snapshots.QwenAudioTts.RunTask;
        server.Playlist.Enqueue(
            async s => await s.WriteServerMessageAsync(Snapshots.QwenAudioTts.TaskStarted.GetMessageJson()));

        // Act
        using var session = await client.CreateSpeechSynthesizerSocketSessionAsync(runTask.Message.Payload.Model!);
        var taskId = await session.RunTaskAsync(
            runTask.Message.Header.TaskId,
            runTask.Message.Payload.Parameters!);

        // Assert
        Assert.Equal(runTask.Message.Header.TaskId, taskId);
        Assert.True(Checkers.IsJsonEquivalent(server.ServerReceivedMessages.First(), runTask.GetMessageJson()));
    }

    [Fact]
    public async Task FullFlow_UsageToken_DeserializedAsync()
    {
        // Arrange
        var (client, _, server) = await Sut.GetSocketTestClientAsync();
        var runTask = Snapshots.QwenAudioTts.RunTask;
        var continueTask = Snapshots.QwenAudioTts.ContinueTask;
        var finishTask = Snapshots.QwenAudioTts.FinishTask;
        var taskStarted = Snapshots.QwenAudioTts.TaskStarted;
        var taskFinished = Snapshots.QwenAudioTts.TaskFinished;
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(taskStarted.GetMessageJson()));
        server.Playlist.Enqueue(
            async s => await s.WriteServerMessageAsync(taskFinished.GetMessageJson()));

        // Act
        using var session = await client.CreateSpeechSynthesizerSocketSessionAsync(runTask.Message.Payload.Model!);
        await session.RunTaskAsync(runTask.Message.Header.TaskId, runTask.Message.Payload.Parameters!);
        await session.ContinueTaskAsync(continueTask.Message.Header.TaskId, continueTask.Message.Payload.Input.Text!);
        await session.FinishTaskAsync(finishTask.Message.Header.TaskId);
        var jsonEvents = await session.GetMessagesAsync().ToListAsync();

        // Assert
        Assert.Equal(2, jsonEvents.Count); // task-started, task-finished
        var usage = jsonEvents.Last().Payload.Usage;
        Assert.Equivalent(new DashScopeWebSocketResponseUsage(21, InputTokens: 11, OutputTokens: 55, TotalTokens: 66), usage);
    }
}
