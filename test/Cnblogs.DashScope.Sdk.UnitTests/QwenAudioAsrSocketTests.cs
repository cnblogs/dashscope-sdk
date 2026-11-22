using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;

namespace Cnblogs.DashScope.Sdk.UnitTests;

[Collection(nameof(SocketTestsCollection))]
public class QwenAudioAsrSocketTests
{
    [Fact]
    public async Task RunTask_QwenAsrMessage_WithContextAndParameters_SuccessAsync()
    {
        // Arrange
        var (client, _, server) = await Sut.GetSocketTestClientAsync();
        var runTask = Snapshots.QwenAsrMessage.RunTask;
        server.Playlist.Enqueue(
            async s => await s.WriteServerMessageAsync(Snapshots.QwenAsrMessage.TaskStarted.GetMessageJson()));

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
    public async Task RunTask_QwenAsrMessage_GenerateTaskId_SuccessAsync()
    {
        // Arrange
        var (client, _, server) = await Sut.GetSocketTestClientAsync();
        var runTask = Snapshots.QwenAsrMessage.RunTask;
        server.Playlist.Enqueue(
            async s => await s.WriteServerMessageAsync(Snapshots.QwenAsrMessage.TaskStarted.GetMessageJson()));

        // Act
        using var session = await client.CreateSpeechRecognitionSocketSessionAsync(runTask.Message.Payload.Model!);
        var taskId = await session.RunTaskAsync(runTask.Message.Payload.Parameters!, runTask.Message.Payload.Input);

        // Assert
        var json = runTask.GetMessageJson().Replace(runTask.Message.Header.TaskId, taskId);
        Assert.True(Checkers.IsJsonEquivalent(server.ServerReceivedMessages.First(), json));
    }

    [Fact]
    public async Task ContinueTask_QwenAsrMessage_UpdateContext_SuccessAsync()
    {
        // Arrange
        var (client, _, server) = await Sut.GetSocketTestClientAsync();
        var runTask = Snapshots.QwenAsrMessage.RunTask;
        var continueTask = Snapshots.QwenAsrMessage.ContinueTask;
        server.Playlist.Enqueue(
            async s => await s.WriteServerMessageAsync(Snapshots.QwenAsrMessage.TaskStarted.GetMessageJson()));

        // Act
        using var session = await client.CreateSpeechRecognitionSocketSessionAsync(runTask.Message.Payload.Model!);
        await session.RunTaskAsync(
            runTask.Message.Header.TaskId,
            runTask.Message.Payload.Parameters!,
            runTask.Message.Payload.Input);
        await session.ContinueTaskAsync(continueTask.Message.Header.TaskId, continueTask.Message.Payload.Input);

        // Assert
        Assert.Equal(2, server.ServerReceivedMessages.Count); // run-task, continue-task
        Assert.True(Checkers.IsJsonEquivalent(server.ServerReceivedMessages.Last(), continueTask.GetMessageJson()));
    }

    [Fact]
    public async Task FullFlow_QwenAsrMessage_IntermediateResultsAndUsage_SuccessAsync()
    {
        // Arrange
        var (client, _, server) = await Sut.GetSocketTestClientAsync();
        var runTask = Snapshots.QwenAsrMessage.RunTask;
        var continueTask = Snapshots.QwenAsrMessage.ContinueTask;
        var taskStarted = Snapshots.QwenAsrMessage.TaskStarted;
        var sentenceBegin = Snapshots.QwenAsrMessage.ResultGeneratedSentenceBegin;
        var intermediate = Snapshots.QwenAsrMessage.ResultGeneratedIntermediate;
        var final = Snapshots.QwenAsrMessage.ResultGenerated;
        var taskFinished = Snapshots.QwenAsrMessage.TaskFinished;
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(taskStarted.GetMessageJson()));
        server.Playlist.Enqueue(_ => Task.CompletedTask); // continue-task
        server.Playlist.Enqueue(_ => Task.CompletedTask); // binary audio
        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(sentenceBegin.GetMessageJson());
            await s.WriteServerMessageAsync(intermediate.GetMessageJson());
            await s.WriteServerMessageAsync(final.GetMessageJson());
            await s.WriteServerMessageAsync(taskFinished.GetMessageJson());
            await s.WriteServerCloseAsync();
        });

        // Act
        using var session = await client.CreateSpeechRecognitionSocketSessionAsync(runTask.Message.Payload.Model!);
        var taskId = await session.RunTaskAsync(runTask.Message.Payload.Parameters!, runTask.Message.Payload.Input);
        await session.ContinueTaskAsync(taskId, continueTask.Message.Payload.Input);
        await session.SendAudioAsync(Snapshots.SpeechRecognition.InputPcm);
        await session.FinishTaskAsync(taskId);
        var jsonEvents = await session.GetMessagesAsync().ToListAsync();

        // Assert
        Assert.Equal(4, server.ServerReceivedMessages.Count); // run-task, continue-task, audio, finish-task
        Assert.Equal(Snapshots.SpeechRecognition.InputPcm, server.ServerReceivedMessages[2].ToArray());
        Assert.Equal(5, jsonEvents.Count); // task-started, sentence begin, intermediate result, final result, task-finished
        Assert.Equivalent(taskStarted.Message, jsonEvents[0]);
        Assert.Equivalent(sentenceBegin.Message, jsonEvents[1]);
        Assert.Equivalent(intermediate.Message, jsonEvents[2]);
        Assert.Equivalent(final.Message, jsonEvents[3]);
        Assert.Equivalent(taskFinished.Message, jsonEvents[4]);

        var sentenceBeginSentence = jsonEvents[1].Payload.Output!.Sentence!;
        Assert.True(sentenceBeginSentence.SentenceBegin);
        Assert.Equal(1, sentenceBeginSentence.SentenceId);
        Assert.Equal(string.Empty, sentenceBeginSentence.Text);
        Assert.Null(jsonEvents[1].Payload.Usage);

        // intermediate results of one sentence share its id, so the displayed text can be updated in place
        var intermediateSentence = jsonEvents[2].Payload.Output!.Sentence!;
        Assert.Equal(1, intermediateSentence.SentenceId);
        Assert.Equal("博客", intermediateSentence.Text);
        Assert.Null(intermediateSentence.SentenceBegin);
        Assert.Null(jsonEvents[2].Payload.Usage);

        Assert.Equal("博客园成立于2004年，代码改变世界。", jsonEvents[3].Payload.Output!.Sentence!.Text);
        Assert.Equal(new DashScopeWebSocketResponseUsage(0, 4, 70, 12, 82), jsonEvents[3].Payload.Usage);
        Assert.Null(jsonEvents[4].Payload.Usage);
    }

    [Fact]
    public async Task RunTask_QwenAsrStreaming_WithContextAndSpecialWordFilter_SuccessAsync()
    {
        // Arrange
        var (client, _, server) = await Sut.GetSocketTestClientAsync();
        var runTask = Snapshots.QwenAudioAsrStreaming.RunTask;
        server.Playlist.Enqueue(
            async s => await s.WriteServerMessageAsync(Snapshots.QwenAudioAsrStreaming.TaskStarted.GetMessageJson()));

        // Act
        using var session = await client.CreateSpeechRecognitionSocketSessionAsync(runTask.Message.Payload.Model!);
        var taskId = await session.RunTaskAsync(
            runTask.Message.Header.TaskId,
            runTask.Message.Payload.Parameters!,
            runTask.Message.Payload.Input);

        // Assert
        Assert.Equal(runTask.Message.Header.TaskId, taskId);
        // special_word_filter has to be the filter object JSON-encoded as a string, a plain JSON object is rejected
        Assert.True(Checkers.IsJsonEquivalent(server.ServerReceivedMessages.First(), runTask.GetMessageJson()));
    }

    [Fact]
    public async Task FullFlow_QwenAsrStreaming_FilteredResultAndUsage_SuccessAsync()
    {
        // Arrange
        var (client, _, server) = await Sut.GetSocketTestClientAsync();
        var runTask = Snapshots.QwenAudioAsrStreaming.RunTask;
        var taskStarted = Snapshots.QwenAudioAsrStreaming.TaskStarted;
        var sentenceBegin = Snapshots.QwenAudioAsrStreaming.ResultGeneratedSentenceBegin;
        var intermediate = Snapshots.QwenAudioAsrStreaming.ResultGeneratedIntermediate;
        var final = Snapshots.QwenAudioAsrStreaming.ResultGenerated;
        var taskFinished = Snapshots.QwenAudioAsrStreaming.TaskFinished;
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(taskStarted.GetMessageJson()));
        server.Playlist.Enqueue(_ => Task.CompletedTask); // binary audio
        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(sentenceBegin.GetMessageJson());
            await s.WriteServerMessageAsync(intermediate.GetMessageJson());
            await s.WriteServerMessageAsync(final.GetMessageJson());
            await s.WriteServerMessageAsync(taskFinished.GetMessageJson());
            await s.WriteServerCloseAsync();
        });

        // Act
        using var session = await client.CreateSpeechRecognitionSocketSessionAsync(runTask.Message.Payload.Model!);
        await session.RunTaskAsync(
            runTask.Message.Header.TaskId,
            runTask.Message.Payload.Parameters!,
            runTask.Message.Payload.Input);
        await session.SendAudioAsync(Snapshots.SpeechRecognition.InputPcm);
        await session.FinishTaskAsync(runTask.Message.Header.TaskId);
        var jsonEvents = await session.GetMessagesAsync().ToListAsync();

        // Assert
        Assert.Equal(3, server.ServerReceivedMessages.Count); // run-task, audio, finish-task
        Assert.Equal(5, jsonEvents.Count); // task-started, sentence begin, intermediate result, final result, task-finished
        Assert.Equivalent(taskStarted.Message, jsonEvents[0]);
        Assert.Equivalent(sentenceBegin.Message, jsonEvents[1]);
        Assert.Equivalent(intermediate.Message, jsonEvents[2]);
        Assert.Equivalent(final.Message, jsonEvents[3]);
        Assert.Equivalent(taskFinished.Message, jsonEvents[4]);

        Assert.Equal("156ed026476041748d3340ed5d5fb3bb", jsonEvents[0].Header.TaskUuid);

        // the sensitive words are filtered in the intermediate results too
        Assert.True(jsonEvents[1].Payload.Output!.Sentence!.SentenceBegin);
        Assert.Equal("博客园成立于2004年**改编", jsonEvents[2].Payload.Output!.Sentence!.Text);
        Assert.Null(jsonEvents[2].Payload.Usage);

        var finalSentence = jsonEvents[3].Payload.Output!.Sentence!;
        Assert.Equal("博客园成立于2004年，**改变。", finalSentence.Text); // 代码 -> **, 世界 removed
        Assert.Equal(1, finalSentence.SentenceId);
        Assert.Equal(new DashScopeWebSocketResponseUsage(0, 4, 74, 13, 87), jsonEvents[3].Payload.Usage);
        Assert.Equal(new DashScopeWebSocketResponseUsage(0, 4, 74, 13, 87), jsonEvents[4].Payload.Usage);
    }

    [Fact]
    public async Task RunTask_QwenAsrStreamingV30_SuccessAsync()
    {
        // Arrange
        var (client, _, server) = await Sut.GetSocketTestClientAsync();
        var runTask = Snapshots.QwenAudioAsrStreamingV30.RunTask;
        server.Playlist.Enqueue(
            async s => await s.WriteServerMessageAsync(Snapshots.QwenAudioAsrStreamingV30.TaskStarted.GetMessageJson()));

        // Act
        using var session = await client.CreateSpeechRecognitionSocketSessionAsync(runTask.Message.Payload.Model!);
        await session.RunTaskAsync(runTask.Message.Header.TaskId, runTask.Message.Payload.Parameters!);

        // Assert
        Assert.True(Checkers.IsJsonEquivalent(server.ServerReceivedMessages.First(), runTask.GetMessageJson()));
    }

    [Fact]
    public async Task FullFlow_QwenAsrStreamingV30_DurationOnlyUsage_SuccessAsync()
    {
        // Arrange
        var (client, _, server) = await Sut.GetSocketTestClientAsync();
        var runTask = Snapshots.QwenAudioAsrStreamingV30.RunTask;
        var taskStarted = Snapshots.QwenAudioAsrStreamingV30.TaskStarted;
        var final = Snapshots.QwenAudioAsrStreamingV30.ResultGenerated;
        var taskFinished = Snapshots.QwenAudioAsrStreamingV30.TaskFinished;
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(taskStarted.GetMessageJson()));
        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(final.GetMessageJson());
            await s.WriteServerMessageAsync(taskFinished.GetMessageJson());
            await s.WriteServerCloseAsync();
        });

        // Act
        using var session = await client.CreateSpeechRecognitionSocketSessionAsync(runTask.Message.Payload.Model!);
        await session.RunTaskAsync(runTask.Message.Header.TaskId, runTask.Message.Payload.Parameters!);
        await session.FinishTaskAsync(runTask.Message.Header.TaskId);
        var jsonEvents = await session.GetMessagesAsync().ToListAsync();

        // Assert
        Assert.Equal(3, jsonEvents.Count); // task-started, final result, task-finished
        Assert.Equivalent(taskStarted.Message, jsonEvents[0]);
        Assert.Equivalent(final.Message, jsonEvents[1]);
        Assert.Equivalent(taskFinished.Message, jsonEvents[2]);

        Assert.Equal("3baeae085bac44ccb7f30f2120f64710", jsonEvents[0].Header.TaskUuid);
        var sentence = jsonEvents[1].Payload.Output!.Sentence!;
        Assert.Equal("The quick brown fox jumps over the lazy dog. ", sentence.Text);
        Assert.Equal(9, sentence.Words!.Length);
        // billed by duration, no tokens
        Assert.Equal(new DashScopeWebSocketResponseUsage(0, 3), jsonEvents[1].Payload.Usage);
        Assert.Equal(new DashScopeWebSocketResponseUsage(0, 3), jsonEvents[2].Payload.Usage);
    }
}
