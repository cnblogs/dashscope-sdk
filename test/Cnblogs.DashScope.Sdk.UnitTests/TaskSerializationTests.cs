using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

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
        var task = await client.GetImageGenerationTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Theory]
    [MemberData(nameof(GetKlingViduImageTaskData))]
    public async Task GetTask_KlingViduImageGeneration_SuccessAsync(
        RequestSnapshot<DashScopeTask<ImageGenerationOutput, MultimodalTokenUsage>> testCase)
    {
        // Arrange
        const bool sse = false;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetImageGenerationTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    public static TheoryData<RequestSnapshot<DashScopeTask<ImageGenerationOutput, MultimodalTokenUsage>>>
        GetKlingViduImageTaskData
        => new()
        {
            Snapshots.Tasks.KlingT2iSuccess,
            Snapshots.Tasks.KlingOmniSeriesSuccess,
            Snapshots.Tasks.ViduR2iSuccess
        };

    [Fact]
    public async Task GetTask_VideoT2v_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.VideoT2vSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetVideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_VideoI2v_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.VideoI2vSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetVideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_StyleTransform_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.StyleTransformSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetVideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_Wan3T2v_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.Wan3T2vSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetVideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_Wan27T2v_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.Wan27T2vSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetVideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_Wan26I2v_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.Wan26I2vSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetVideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_VaceImageReference_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.VaceImageReferenceSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetVideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_Kf2v_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.Kf2vSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetImage2VideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_VaceVideoRepainting_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.VaceVideoRepaintingSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetVideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_PixverseT2v_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.PixverseT2vSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetVideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_PixverseI2v_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.PixverseI2vSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetVideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_PixverseKf2v_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.PixverseKf2vSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetVideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_PixverseR2vOmni_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.PixverseR2vOmniSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetVideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_PixverseLipsync_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.PixverseLipsyncSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetVideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_PixverseMotionControl_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.PixverseMotionControlSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetVideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_PixverseUpscale_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.PixverseUpscaleSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetVideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_AnimateMove_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.AnimateMoveSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetImage2VideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_S2v_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.S2vSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetImage2VideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_Videoretalk_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.VideoretalkSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetImage2VideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_ImageEditInstruction_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.ImageEditInstructionSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetImage2ImageSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_ImageEditFunction_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.ImageEditFunctionSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetImage2ImageSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_Wan26T2i_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.Wan26T2iSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetImageGenerationTaskAsync(testCase.ResponseModel.Output.TaskId);

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
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var tasks = await client.ListTasksAsync(
            taskId: Cases.Uuid,
            startTime: new DateTime(2026, 11, 18, 14, 30, 58),
            endTime: new DateTime(2026, 11, 18, 15, 30, 58),
            pageNo: 1,
            pageSize: 10,
            modelName: "wanx-background-generation-v2",
            status: DashScopeTaskStatus.Succeeded);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => m.RequestUri!.PathAndQuery.Contains("status=SUCCEEDED")
               && m.RequestUri!.PathAndQuery.Contains("start_time=20261118143058")
               && m.RequestUri!.PathAndQuery.Contains("end_time=20261118153058")),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, tasks);
    }

    [Fact]
    public async Task GetTask_Canceled_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.Canceled;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetVideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_KlingT2v_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.KlingT2vSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetVideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_KlingR2v_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.KlingR2vSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetVideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_ViduT2v_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.ViduT2vSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetVideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_ViduI2v_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.ViduI2vSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetVideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_ViduKf2v_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.ViduKf2vSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetVideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_ViduR2v_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.ViduR2vSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetVideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_MinimaxT2v_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.MinimaxT2vSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetVideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task GetTask_MinimaxR2v_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.MinimaxR2vSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetVideoSynthesisTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }
}
