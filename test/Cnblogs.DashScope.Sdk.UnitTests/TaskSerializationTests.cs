using Cnblogs.DashScope.Sdk.UnitTests.Utils;
using FluentAssertions;

namespace Cnblogs.DashScope.Sdk.UnitTests;

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
        task.Should().BeEquivalentTo(testCase.ResponseModel);
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
        (await act.Should().ThrowAsync<DashScopeException>()).And.Error.Should().BeEquivalentTo(testCase.ResponseModel);
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
        task.Should().BeEquivalentTo(testCase.ResponseModel);
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
        task.Should().BeEquivalentTo(testCase.ResponseModel);
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
        task.Should().BeEquivalentTo(testCase.ResponseModel);
    }

    [Fact]
    public async Task ListTasks_Tasks_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.ListTasks;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var tasks = await client.ListTasksAsync();

        // Assert
        tasks.Should().BeEquivalentTo(testCase.ResponseModel);
    }
}
