using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;

namespace Cnblogs.DashScope.Sdk.UnitTests
{
    public class TaskSerializationTests
    {
        [Fact]
        public async Task GetTask_Unknown_SuccessAsync()
        {
            // Arrange
            const bool sse = false;
            var testCase = Snapshots.Tasks.Unknown;
            var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

            // Act
            var task = await client.GetTaskAsync<BatchGetEmbeddingsOutput, TextEmbeddingTokenUsage>(
                testCase.ResponseModel.Output.TaskId);

            // Assert
            Assert.Equivalent(testCase.ResponseModel, task);
        }

        [Fact]
        public async Task CancelTask_TaskAlreadyCompleted_FailAsync()
        {
            // Arrange
            const bool sse = false;
            var testCase = Snapshots.Tasks.CancelCompletedTask;
            var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

            // Act
            var act = async () => await client.CancelTaskAsync(Cases.Uuid);

            // Assert
            var ex = await Assert.ThrowsAsync<DashScopeException>(act);
            Assert.Equivalent(testCase.ResponseModel, ex.Error);
        }

        [Fact]
        public async Task GetTask_BatchEmbeddings_SuccessAsync()
        {
            // Arrange
            const bool sse = false;
            var testCase = Snapshots.Tasks.BatchEmbeddingSuccess;
            var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

            // Act
            var task = await client.GetTaskAsync<BatchGetEmbeddingsOutput, TextEmbeddingTokenUsage>(
                testCase.ResponseModel.Output.TaskId);

            // Assert
            Assert.Equivalent(testCase.ResponseModel, task);
        }

        [Fact]
        public async Task GetTask_ImageSynthesis_SuccessAsync()
        {
            // Arrange
            const bool sse = false;
            var testCase = Snapshots.Tasks.ImageSynthesisSuccess;
            var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

            // Act
            var task = await client.GetTaskAsync<ImageSynthesisOutput, ImageSynthesisUsage>(
                testCase.ResponseModel.Output.TaskId);

            // Assert
            Assert.Equivalent(testCase.ResponseModel, task);
        }

        [Fact]
        public async Task GetTask_ImageGeneration_SuccessAsync()
        {
            // Arrange
            const bool sse = false;
            var testCase = Snapshots.Tasks.ImageGenerationSuccess;
            var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

            // Act
            var task = await client.GetTaskAsync<ImageGenerationOutput, ImageGenerationUsage>(
                testCase.ResponseModel.Output.TaskId);

            // Assert
            Assert.Equivalent(testCase.ResponseModel, task);
        }

        [Fact]
        public async Task GetTask_BackgroundGeneration_SuccessAsync()
        {
            // Arrange
            const bool sse = false;
            var testCase = Snapshots.Tasks.BackgroundGenerationSuccess;
            var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

            // Act
            var task = await client.GetTaskAsync<BackgroundGenerationOutput, BackgroundGenerationUsage>(
                testCase.ResponseModel.Output.TaskId);

            // Assert
            Assert.Equivalent(testCase.ResponseModel, task);
        }

        [Fact]
        public async Task GetTask_RunningTask_SuccessAsync()
        {
            // Arrange
            const bool sse = false;
            var testCase = Snapshots.Tasks.ImageSynthesisRunning;
            var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

            // Act
            var task = await client.GetTaskAsync<ImageSynthesisOutput, ImageSynthesisUsage>(
                testCase.ResponseModel.Output.TaskId);

            // Assert
            Assert.Equivalent(testCase.ResponseModel, task);
        }

        [Fact]
        public async Task ListTasks_Tasks_SuccessAsync()
        {
            // Arrange
            const bool sse = false;
            var testCase = Snapshots.Tasks.ListTasks;
            var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

            // Act
            var tasks = await client.ListTasksAsync(
                taskId: Cases.Uuid,
                startTime: DateTime.Today,
                endTime: DateTime.Now,
                pageNo: 1,
                pageSize: 10,
                modelName: "wanx-background-generation-v2",
                status: DashScopeTaskStatus.Succeeded);

            // Assert
            Assert.Equivalent(testCase.ResponseModel, tasks);
        }
    }
}
