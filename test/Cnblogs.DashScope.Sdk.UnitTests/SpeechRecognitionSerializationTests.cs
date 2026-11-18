using Cnblogs.DashScope.Tests.Shared.Utils;

namespace Cnblogs.DashScope.Sdk.UnitTests;

[Collection(nameof(SocketTestsCollection))]
public class SpeechRecognitionSerializationTests
{
    [Fact]
    public async Task RunTask_SpecifyTaskId_SuccessAsync()
    {
        // Arrange
        var (client, _, server) = await Sut.GetSocketTestClientAsync();
        var snapshot = Snapshots.SpeechRecognition.RunTask;
        var taskStartedEvent = Snapshots.SpeechRecognition.TaskStarted;
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(taskStartedEvent.GetMessageJson()));

        // Act
        using var session = await client.CreateSpeechRecognitionSocketSessionAsync(snapshot.Message.Payload.Model!);
        var taskId = await session.RunTaskAsync(snapshot.Message.Header.TaskId, snapshot.Message.Payload.Parameters!);

        // Assert
        Assert.Equal(snapshot.Message.Header.TaskId, taskId);
        Assert.True(Checkers.IsJsonEquivalent(server.ServerReceivedMessages.First(), snapshot.GetMessageJson()));
    }

    [Fact]
    public async Task RunTask_GenerateTaskId_SuccessAsync()
    {
        // Arrange
        var (client, _, server) = await Sut.GetSocketTestClientAsync();
        var snapshot = Snapshots.SpeechRecognition.RunTask;
        var taskStartedEvent = Snapshots.SpeechRecognition.TaskStarted;
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(taskStartedEvent.GetMessageJson()));

        // Act
        using var session = await client.CreateSpeechRecognitionSocketSessionAsync(snapshot.Message.Payload.Model!);
        var taskId = await session.RunTaskAsync(snapshot.Message.Payload.Parameters!);

        // Assert
        var json = snapshot.GetMessageJson().Replace(snapshot.Message.Header.TaskId, taskId);
        Assert.True(Checkers.IsJsonEquivalent(server.ServerReceivedMessages.First(), json));
    }

    [Fact]
    public async Task RunTask_EightKModel_SuccessAsync()
    {
        // Arrange
        var (client, _, server) = await Sut.GetSocketTestClientAsync();
        var snapshot = Snapshots.SpeechRecognition8k.RunTask;
        var taskStartedEvent = new SocketMessageSnapshot("speech-recognition-8k", "task-started");
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(taskStartedEvent.GetMessageJson()));

        // Act
        using var session = await client.CreateSpeechRecognitionSocketSessionAsync(snapshot.Message.Payload.Model!);
        await session.RunTaskAsync(snapshot.Message.Header.TaskId, snapshot.Message.Payload.Parameters!);

        // Assert
        Assert.True(Checkers.IsJsonEquivalent(server.ServerReceivedMessages.First(), snapshot.GetMessageJson()));
    }

    [Fact]
    public async Task RunTask_EnglishLanguageHints_SuccessAsync()
    {
        // Arrange
        var (client, _, server) = await Sut.GetSocketTestClientAsync();
        var snapshot = Snapshots.SpeechRecognitionEn.RunTask;
        var taskStartedEvent = new SocketMessageSnapshot("speech-recognition-en", "task-started");
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(taskStartedEvent.GetMessageJson()));

        // Act
        using var session = await client.CreateSpeechRecognitionSocketSessionAsync(snapshot.Message.Payload.Model!);
        await session.RunTaskAsync(snapshot.Message.Header.TaskId, snapshot.Message.Payload.Parameters!);

        // Assert
        Assert.True(Checkers.IsJsonEquivalent(server.ServerReceivedMessages.First(), snapshot.GetMessageJson()));
    }

    [Fact]
    public async Task SendAudio_Buffer_ServerReceivesBinaryAsync()
    {
        // Arrange
        var (client, _, server) = await Sut.GetSocketTestClientAsync();
        var runTask = Snapshots.SpeechRecognition.RunTask;
        var taskStartedEvent = Snapshots.SpeechRecognition.TaskStarted;
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(taskStartedEvent.GetMessageJson()));

        // Act
        using var session = await client.CreateSpeechRecognitionSocketSessionAsync(runTask.Message.Payload.Model!);
        await session.RunTaskAsync(runTask.Message.Header.TaskId, runTask.Message.Payload.Parameters!);
        await session.SendAudioAsync(Snapshots.SpeechRecognition.InputPcm);

        // Assert
        var lastMessage = server.ServerReceivedMessages.Last();
        Assert.Equal(Snapshots.SpeechRecognition.InputPcm, lastMessage.ToArray());
    }

    [Fact]
    public async Task SendAudio_Stream_SentInSmallChunksAsync()
    {
        // Arrange
        var (client, _, server) = await Sut.GetSocketTestClientAsync();
        var runTask = Snapshots.SpeechRecognition.RunTask;
        var taskStartedEvent = Snapshots.SpeechRecognition.TaskStarted;
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(taskStartedEvent.GetMessageJson()));

        // Act
        using var session = await client.CreateSpeechRecognitionSocketSessionAsync(runTask.Message.Payload.Model!);
        await session.RunTaskAsync(runTask.Message.Header.TaskId, runTask.Message.Payload.Parameters!);
        using var audioStream = new MemoryStream(Snapshots.SpeechRecognition.InputPcm);
        await session.SendAudioAsync(audioStream);

        // Assert
        // 123520 bytes / 3200-byte chunks = 39 binary frames, plus the run-task text frame
        Assert.Equal(40, server.ServerReceivedMessages.Count);
        var binaryFrames = server.ServerReceivedMessages.Skip(1).Select(x => x.ToArray()).ToList();
        Assert.All(binaryFrames, frame => Assert.InRange(frame.Length, 1, 3200));
        using var received = new MemoryStream();
        foreach (var frame in binaryFrames)
        {
            received.Write(frame);
        }

        Assert.Equal(Snapshots.SpeechRecognition.InputPcm, received.ToArray());
    }

    [Fact]
    public async Task FinishTask_AfterRunTask_SuccessAsync()
    {
        // Arrange
        var (client, _, server) = await Sut.GetSocketTestClientAsync();
        var runTask = Snapshots.SpeechRecognition.RunTask;
        var finishTask = Snapshots.SpeechRecognition.FinishTask;
        var taskStartedEvent = Snapshots.SpeechRecognition.TaskStarted;
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(taskStartedEvent.GetMessageJson()));

        // Act
        using var session = await client.CreateSpeechRecognitionSocketSessionAsync(runTask.Message.Payload.Model!);
        await session.RunTaskAsync(runTask.Message.Header.TaskId, runTask.Message.Payload.Parameters!);
        await session.FinishTaskAsync(finishTask.Message.Header.TaskId);

        // Assert
        Assert.True(Checkers.IsJsonEquivalent(server.ServerReceivedMessages.Last(), finishTask.GetMessageJson()));
    }

    [Fact]
    public async Task ResultGenerated_IntermediateAndFinal_SuccessAsync()
    {
        // Arrange
        var (client, _, server) = await Sut.GetSocketTestClientAsync();
        var runTask = Snapshots.SpeechRecognition.RunTask;
        var finishTask = Snapshots.SpeechRecognition.FinishTask;
        var intermediate = Snapshots.SpeechRecognition.ResultGeneratedIntermediate;
        var final = Snapshots.SpeechRecognition.ResultGenerated;
        var taskStartedEvent = Snapshots.SpeechRecognition.TaskStarted;
        var taskFinished = Snapshots.SpeechRecognition.TaskFinished;
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(taskStartedEvent.GetMessageJson()));
        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(intermediate.GetMessageJson());
            await s.WriteServerMessageAsync(final.GetMessageJson());
            await s.WriteServerMessageAsync(taskFinished.GetMessageJson());
            await s.WriteServerCloseAsync();
        });

        // Act
        using var session = await client.CreateSpeechRecognitionSocketSessionAsync(runTask.Message.Payload.Model!);
        await session.RunTaskAsync(runTask.Message.Header.TaskId, runTask.Message.Payload.Parameters!);
        await session.FinishTaskAsync(finishTask.Message.Header.TaskId);
        var jsonEvents = await session.GetMessagesAsync().ToListAsync();

        // Assert
        Assert.Equal(4, jsonEvents.Count); // task-started, intermediate result, final result, task-finished
        Assert.Equal("task-started", jsonEvents[0].Header.Event);
        Assert.Equivalent(intermediate.Message, jsonEvents[1]);
        Assert.Equivalent(final.Message, jsonEvents[2]);
        Assert.Equivalent(taskFinished.Message, jsonEvents[3]);

        var intermediateSentence = jsonEvents[1].Payload.Output!.Sentence!;
        Assert.False(intermediateSentence.SentenceEnd);
        Assert.Null(intermediateSentence.EndTime);
        Assert.Null(jsonEvents[1].Payload.Usage);

        var finalSentence = jsonEvents[2].Payload.Output!.Sentence!;
        Assert.True(finalSentence.SentenceEnd);
        Assert.Equal(3580, finalSentence.EndTime);
        Assert.Equal(8, finalSentence.Words!.Length);
        Assert.Equal("博客源成立于2004年，代码改变世界。", finalSentence.Text);
        Assert.Equal(4, jsonEvents[2].Payload.Usage!.Duration);
    }

    [Fact]
    public async Task ResultGenerated_EightKModel_ContainsEmotionAsync()
    {
        // Arrange
        var (client, _, server) = await Sut.GetSocketTestClientAsync();
        var runTask = Snapshots.SpeechRecognition8k.RunTask;
        var resultGenerated = Snapshots.SpeechRecognition8k.ResultGenerated;
        var taskStartedEvent = new SocketMessageSnapshot("speech-recognition-8k", "task-started");
        var taskFinishedEvent = new SocketMessageSnapshot("speech-recognition-8k", "task-finished");
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(taskStartedEvent.GetMessageJson()));
        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(resultGenerated.GetMessageJson());
            await s.WriteServerMessageAsync(taskFinishedEvent.GetMessageJson());
            await s.WriteServerCloseAsync();
        });

        // Act
        using var session = await client.CreateSpeechRecognitionSocketSessionAsync(runTask.Message.Payload.Model!);
        await session.RunTaskAsync(runTask.Message.Header.TaskId, runTask.Message.Payload.Parameters!);
        await session.FinishTaskAsync(runTask.Message.Header.TaskId);
        var jsonEvents = await session.GetMessagesAsync().ToListAsync();

        // Assert
        Assert.Equal(3, jsonEvents.Count); // task-started, result-generated, task-finished
        Assert.Equivalent(resultGenerated.Message, jsonEvents[1]);
        var sentence = jsonEvents[1].Payload.Output!.Sentence!;
        Assert.True(sentence.SentenceEnd);
        Assert.Equal("neutral", sentence.EmoTag);
        Assert.Equal(0.995f, sentence.EmoConfidence);
        Assert.Equal(4, jsonEvents[1].Payload.Usage!.Duration);
    }

    [Fact]
    public async Task TaskFailed_ServerClose_SuccessAsync()
    {
        // Arrange
        var (client, _, server) = await Sut.GetSocketTestClientAsync();
        var runTask = Snapshots.SpeechRecognition.RunTask;
        var finishTask = Snapshots.SpeechRecognition.FinishTask;
        var taskFailed = Snapshots.SpeechRecognitionInvalid.TaskFailed;
        var taskStartedEvent = Snapshots.SpeechRecognition.TaskStarted;
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(taskStartedEvent.GetMessageJson()));
        server.Playlist.Enqueue(async s =>
        {
            await s.WriteServerMessageAsync(taskFailed.GetMessageJson());
            await s.WriteServerCloseAsync();
        });

        // Act
        using var session = await client.CreateSpeechRecognitionSocketSessionAsync(runTask.Message.Payload.Model!);
        await session.RunTaskAsync(runTask.Message.Header.TaskId, runTask.Message.Payload.Parameters!);
        await session.FinishTaskAsync(finishTask.Message.Header.TaskId);
        var jsonEvents = await session.GetMessagesAsync().ToListAsync();

        // Assert
        Assert.Equal(2, jsonEvents.Count); // task-started, task-failed
        Assert.Equivalent(taskFailed.Message, jsonEvents.Last());
        Assert.Equal("UNSUPPORTED_FORMAT", jsonEvents.Last().Header.ErrorCode);
    }

    [Fact]
    public async Task Dispose_DisposedByUsings_ReturnSocketAsync()
    {
        // Arrange
        var (client, _, server) = await Sut.GetSocketTestClientAsync();
        var runTask = Snapshots.SpeechRecognition.RunTask;
        var finishTask = Snapshots.SpeechRecognition.FinishTask;
        var taskStartedEvent = Snapshots.SpeechRecognition.TaskStarted;
        var taskFinished = Snapshots.SpeechRecognition.TaskFinished;
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(taskStartedEvent.GetMessageJson()));
        server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(taskFinished.GetMessageJson()));

        // Act
        using (var session = await client.CreateSpeechRecognitionSocketSessionAsync(runTask.Message.Payload.Model!))
        {
            await session.RunTaskAsync(runTask.Message.Header.TaskId, runTask.Message.Payload.Parameters!);
            await session.FinishTaskAsync(finishTask.Message.Header.TaskId);
        }

        // Assert
        Assert.False(server.DisposeCalled);
    }
}
