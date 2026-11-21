using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;

namespace Cnblogs.DashScope.Sdk.UnitTests;

[Collection(nameof(SocketTestsCollection))]
public class SambertSocketTests
{
    [Fact]
    public async Task Synthesize_SpecifyTaskId_SuccessAsync()
    {
        // Arrange
        var (client, _, server) = await Sut.GetSocketTestClientAsync();
        var runTask = Snapshots.Sambert.RunTask;
        server.Playlist.Enqueue(
            async s => await s.WriteServerMessageAsync(Snapshots.Sambert.TaskStarted.GetMessageJson()));

        // Act
        using var session = await client.CreateSpeechSynthesizerSocketSessionAsync(runTask.Message.Payload.Model!);
        var taskId = await session.SynthesizeAsync(
            runTask.Message.Header.TaskId,
            runTask.Message.Payload.Parameters!,
            runTask.Message.Payload.Input.Text!);

        // Assert
        Assert.Equal(runTask.Message.Header.TaskId, taskId);
        Assert.True(Checkers.IsJsonEquivalent(server.ServerReceivedMessages.First(), runTask.GetMessageJson()));
    }

    [Fact]
    public async Task Synthesize_GenerateTaskId_SuccessAsync()
    {
        // Arrange
        var (client, _, server) = await Sut.GetSocketTestClientAsync();
        var runTask = Snapshots.Sambert.RunTask;
        server.Playlist.Enqueue(
            async s => await s.WriteServerMessageAsync(Snapshots.Sambert.TaskStarted.GetMessageJson()));

        // Act
        using var session = await client.CreateSpeechSynthesizerSocketSessionAsync(runTask.Message.Payload.Model!);
        var taskId = await session.SynthesizeAsync(
            runTask.Message.Payload.Parameters!,
            runTask.Message.Payload.Input.Text!);

        // Assert
        var json = runTask.GetMessageJson().Replace(runTask.Message.Header.TaskId, taskId);
        Assert.True(Checkers.IsJsonEquivalent(server.ServerReceivedMessages.First(), json));
    }

    [Fact]
    public async Task Synthesize_FullFlow_TimestampsAndUsage_SuccessAsync()
    {
        // Arrange
        var (client, _, server) = await Sut.GetSocketTestClientAsync();
        var runTask = Snapshots.Sambert.RunTask;
        var resultGenerated = Snapshots.Sambert.ResultGenerated;
        var taskFinished = Snapshots.Sambert.TaskFinished;
        var audio = Snapshots.Sambert.Audio;
        server.Playlist.Enqueue(
            async s => await s.WriteServerMessageAsync(Snapshots.Sambert.TaskStarted.GetMessageJson()));

        // Act
        using var session = await client.CreateSpeechSynthesizerSocketSessionAsync(runTask.Message.Payload.Model!);
        await session.SynthesizeAsync(
            runTask.Message.Header.TaskId,
            runTask.Message.Payload.Parameters!,
            runTask.Message.Payload.Input.Text!);
        // Sambert has no continue-task / finish-task, the remaining events are not triggered
        // by another send, so they are written directly.
        await server.WriteServerMessageAsync(resultGenerated.GetMessageJson());
        await server.WriteServerMessageAsync(audio);
        await server.WriteServerMessageAsync(taskFinished.GetMessageJson());
        await server.WriteServerCloseAsync();
        var jsonEvents = await session.GetMessagesAsync().ToListAsync();
        var audioContent = await session.GetAudioAsync().ToArrayAsync();

        // Assert
        Assert.Equivalent(audio, audioContent);
        Assert.Equal(3, jsonEvents.Count); // task-started, result-generated, task-finished
        Assert.Equivalent(Snapshots.Sambert.TaskStarted.Message, jsonEvents[0]);
        var sentence = jsonEvents[1].Payload.Output!.Sentence!;
        Assert.Equal(0, sentence.BeginTime);
        var word = sentence.Words![0];
        Assert.Equal("床", word.Text);
        Assert.Null(word.BeginIndex); // Sambert words carry no index fields
        Assert.Equal(new SpeechSynthesizerPhoneme("ch_c", 2, 0, 119), word.Phonemes![0]);
        Assert.Equivalent(taskFinished.Message, jsonEvents[^1]);
        Assert.Equal(11, jsonEvents[^1].Payload.Usage?.Characters);
    }
}
