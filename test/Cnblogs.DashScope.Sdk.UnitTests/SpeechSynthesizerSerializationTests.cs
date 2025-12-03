using Cnblogs.DashScope.Tests.Shared.Utils;

namespace Cnblogs.DashScope.Sdk.UnitTests
{
    [Collection(nameof(SocketTestsCollection))]
    public class SpeechSynthesizerSerializationTests
    {
        [Fact]
        public async Task RunTask_SpecifyTaskId_SuccessAsync()
        {
            // Arrange
            var (client, _, server) = await Sut.GetSocketTestClientAsync();
            var snapshot = Snapshots.SpeechSynthesizer.RunTask;
            var taskStartedEvent = Snapshots.SpeechSynthesizer.TaskStarted;
            server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(taskStartedEvent.GetMessageJson()));

            // Act
            using var session = await client.CreateSpeechSynthesizerSocketSessionAsync(snapshot.Message.Payload.Model!);
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
            var snapshot = Snapshots.SpeechSynthesizer.RunTask;
            var taskStartedEvent = Snapshots.SpeechSynthesizer.TaskStarted;
            server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(taskStartedEvent.GetMessageJson()));

            // Act
            using var session = await client.CreateSpeechSynthesizerSocketSessionAsync(snapshot.Message.Payload.Model!);
            var taskId = await session.RunTaskAsync(snapshot.Message.Payload.Parameters!);

            // Assert
            var json = snapshot.GetMessageJson().Replace(snapshot.Message.Header.TaskId, taskId);
            Assert.True(Checkers.IsJsonEquivalent(server.ServerReceivedMessages.First(), json));
        }

        [Fact]
        public async Task ContinueTask_WithInput_SuccessAsync()
        {
            // Arrange
            var (client, _, server) = await Sut.GetSocketTestClientAsync();
            var runTask = Snapshots.SpeechSynthesizer.RunTask;
            var continueTask = Snapshots.SpeechSynthesizer.ContinueTask;
            var taskStartedEvent = Snapshots.SpeechSynthesizer.TaskStarted;
            server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(taskStartedEvent.GetMessageJson()));

            // Act
            using var session = await client.CreateSpeechSynthesizerSocketSessionAsync(runTask.Message.Payload.Model!);
            await session.RunTaskAsync(runTask.Message.Header.TaskId, runTask.Message.Payload.Parameters!);
            await session.ContinueTaskAsync(continueTask.Message.Header.TaskId, continueTask.Message.Payload.Input.Text!);

            // Assert
            Assert.True(Checkers.IsJsonEquivalent(server.ServerReceivedMessages.Last(), continueTask.GetMessageJson()));
        }

        [Fact]
        public async Task FinishTask_NoPayload_SuccessAsync()
        {
            // Arrange
            var (client, _, server) = await Sut.GetSocketTestClientAsync();
            var runTask = Snapshots.SpeechSynthesizer.RunTask;
            var continueTask = Snapshots.SpeechSynthesizer.ContinueTask;
            var finishTask = Snapshots.SpeechSynthesizer.FinishTask;
            var taskStartedEvent = Snapshots.SpeechSynthesizer.TaskStarted;
            server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(taskStartedEvent.GetMessageJson()));

            // Act
            using var session = await client.CreateSpeechSynthesizerSocketSessionAsync(runTask.Message.Payload.Model!);
            await session.RunTaskAsync(runTask.Message.Header.TaskId, runTask.Message.Payload.Parameters!);
            await session.ContinueTaskAsync(continueTask.Message.Header.TaskId, continueTask.Message.Payload.Input.Text!);
            await session.FinishTaskAsync(finishTask.Message.Header.TaskId);

            // Assert
            Assert.True(Checkers.IsJsonEquivalent(server.ServerReceivedMessages.Last(), finishTask.GetMessageJson()));
        }

        [Fact]
        public async Task ResultGenerated_WithBinary_SuccessAsync()
        {
            // Arrange
            var (client, _, server) = await Sut.GetSocketTestClientAsync();
            var runTask = Snapshots.SpeechSynthesizer.RunTask;
            var finishTask = Snapshots.SpeechSynthesizer.FinishTask;
            var resultGenerated = Snapshots.SpeechSynthesizer.ResultGenerated;
            var ttsBinary = Snapshots.SpeechSynthesizer.AudioTts;
            var taskStartedEvent = Snapshots.SpeechSynthesizer.TaskStarted;
            server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(taskStartedEvent.GetMessageJson()));
            server.Playlist.Enqueue(async s =>
            {
                await s.WriteServerMessageAsync(resultGenerated.GetMessageJson());
                await s.WriteServerMessageAsync(ttsBinary);
                await s.WriteServerCloseAsync();
            });

            // Act
            using var session = await client.CreateSpeechSynthesizerSocketSessionAsync(runTask.Message.Payload.Model!);
            await session.RunTaskAsync(runTask.Message.Header.TaskId, runTask.Message.Payload.Parameters!);
            await session.FinishTaskAsync(finishTask.Message.Header.TaskId);
            var jsonEvents = await session.GetMessagesAsync().ToListAsync();
            var binaryContent = await session.GetAudioAsync().ToArrayAsync();

            // Assert
            Assert.Equivalent(ttsBinary, binaryContent);
            Assert.Equal(2, jsonEvents.Count); // task-started, result-generated
            Assert.Equivalent(resultGenerated.Message, jsonEvents.Last());
        }

        [Fact]
        public async Task TaskFinished_ServerClose_SuccessAsync()
        {
            // Arrange
            var (client, _, server) = await Sut.GetSocketTestClientAsync();
            var runTask = Snapshots.SpeechSynthesizer.RunTask;
            var finishTask = Snapshots.SpeechSynthesizer.FinishTask;
            var resultGenerated = Snapshots.SpeechSynthesizer.ResultGenerated;
            var taskFinished = Snapshots.SpeechSynthesizer.TaskFinished;
            var ttsBinary = Snapshots.SpeechSynthesizer.AudioTts;
            var taskStartedEvent = Snapshots.SpeechSynthesizer.TaskStarted;
            server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(taskStartedEvent.GetMessageJson()));
            server.Playlist.Enqueue(async s =>
            {
                await s.WriteServerMessageAsync(resultGenerated.GetMessageJson());
                await s.WriteServerMessageAsync(ttsBinary);
                await s.WriteServerMessageAsync(taskFinished.GetMessageJson());
                await s.WriteServerCloseAsync();
            });

            // Act
            using var session = await client.CreateSpeechSynthesizerSocketSessionAsync(runTask.Message.Payload.Model!);
            await session.RunTaskAsync(runTask.Message.Header.TaskId, runTask.Message.Payload.Parameters!);
            await session.FinishTaskAsync(finishTask.Message.Header.TaskId);
            var jsonEvents = await session.GetMessagesAsync().ToListAsync();
            var binaryContent = await session.GetAudioAsync().ToArrayAsync();

            // Assert
            Assert.Equivalent(ttsBinary, binaryContent);
            Assert.Equal(3, jsonEvents.Count); // task-started, result-generated, task-finished
            Assert.Equivalent(taskFinished.Message, jsonEvents.Last());
        }

        [Fact]
        public async Task TaskFailed_ServerClose_SuccessAsync()
        {
            // Arrange
            var (client, _, server) = await Sut.GetSocketTestClientAsync();
            var runTask = Snapshots.SpeechSynthesizer.RunTask;
            var finishTask = Snapshots.SpeechSynthesizer.FinishTask;
            var taskFailed = Snapshots.SpeechSynthesizer.TaskFailed;
            var taskStarted = Snapshots.SpeechSynthesizer.TaskStarted;
            server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(taskStarted.GetMessageJson()));
            server.Playlist.Enqueue(async s =>
            {
                await s.WriteServerMessageAsync(taskFailed.GetMessageJson());
                await s.WriteServerCloseAsync();
            });

            // Act
            using var session = await client.CreateSpeechSynthesizerSocketSessionAsync(runTask.Message.Payload.Model!);
            await session.RunTaskAsync(runTask.Message.Header.TaskId, runTask.Message.Payload.Parameters!);
            await session.FinishTaskAsync(finishTask.Message.Header.TaskId);
            var jsonEvents = await session.GetMessagesAsync().ToListAsync();
            var binaryContent = await session.GetAudioAsync().ToArrayAsync();

            // Assert
            Assert.Empty(binaryContent);
            Assert.Equal(2, jsonEvents.Count); // task-started, task-failed
            Assert.Equivalent(taskFailed.Message, jsonEvents.Last());
        }

        [Fact]
        public async Task Dispose_DisposedByUsings_ReturnSocketAsync()
        {
            // Arrange
            var (client, _, server) = await Sut.GetSocketTestClientAsync();
            var runTask = Snapshots.SpeechSynthesizer.RunTask;
            var finishTask = Snapshots.SpeechSynthesizer.FinishTask;
            var resultGenerated = Snapshots.SpeechSynthesizer.ResultGenerated;
            var taskFinished = Snapshots.SpeechSynthesizer.TaskFinished;
            var ttsBinary = Snapshots.SpeechSynthesizer.AudioTts;
            var taskStartedEvent = Snapshots.SpeechSynthesizer.TaskStarted;
            server.Playlist.Enqueue(async s => await s.WriteServerMessageAsync(taskStartedEvent.GetMessageJson()));
            server.Playlist.Enqueue(async s =>
            {
                await s.WriteServerMessageAsync(resultGenerated.GetMessageJson());
                await s.WriteServerMessageAsync(ttsBinary);
                await s.WriteServerMessageAsync(taskFinished.GetMessageJson());
            });

            // Act
            using (var session = await client.CreateSpeechSynthesizerSocketSessionAsync(runTask.Message.Payload.Model!))
            {
                await session.RunTaskAsync(runTask.Message.Header.TaskId, runTask.Message.Payload.Parameters!);
                await session.FinishTaskAsync(finishTask.Message.Header.TaskId);
            }

            // Assert
            Assert.False(server.DisposeCalled);
        }
    }
}
