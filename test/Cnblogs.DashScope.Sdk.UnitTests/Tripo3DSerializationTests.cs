using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class Tripo3DSerializationTests
{
    [Fact]
    public async Task Tripo3D_CreateT2vTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tripo3D.CreateT2vTask;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateTripo3DTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task Tripo3D_CreateMi2vTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tripo3D.CreateMi2vTask;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateTripo3DTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task Tripo3D_CreateImageToSplatTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tripo3D.CreateImageToSplatTask;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateTripo3DTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task Tripo3D_CreateRigCheckTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tripo3D.CreateRigCheckTask;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateTripo3DTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task Tripo3D_CreateTextureTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tripo3D.CreateTextureTask;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateTripo3DTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task Tripo3D_CreateSegmentationTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tripo3D.CreateSegmentationTask;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateTripo3DTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task Tripo3D_CreateSmartSegmentationTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tripo3D.CreateSmartSegmentationTask;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateTripo3DTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task Tripo3D_CreateRetopologyTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tripo3D.CreateRetopologyTask;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateTripo3DTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task Tripo3D_CreateConvertFormatTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tripo3D.CreateConvertFormatTask;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateTripo3DTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task Tripo3D_CreateMeshCompletionTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tripo3D.CreateMeshCompletionTask;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateTripo3DTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task Tripo3D_CreateAutoRigTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tripo3D.CreateAutoRigTask;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateTripo3DTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task Tripo3D_CreateAnimationRetargetTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tripo3D.CreateAnimationRetargetTask;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateTripo3DTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task Tripo3D_GetT2vTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.Tripo3DT2vSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetTripo3DTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task Tripo3D_GetMi2vTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.Tripo3DMi2vSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetTripo3DTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task Tripo3D_GetImageToSplatTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.Tripo3DImageToSplatSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetTripo3DTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task Tripo3D_GetRigCheckTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.Tripo3DRigCheckSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetTripo3DTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task Tripo3D_GetTextureTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.Tripo3DTextureSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetTripo3DTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task Tripo3D_GetSegmentationTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.Tripo3DSegmentationSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetTripo3DTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task Tripo3D_GetSmartSegmentationTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.Tripo3DSmartSegmentationSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetTripo3DTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task Tripo3D_GetRetopologyTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.Tripo3DRetopologySuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetTripo3DTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task Tripo3D_GetConvertFormatTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.Tripo3DConvertFormatSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetTripo3DTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task Tripo3D_GetMeshCompletionTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.Tripo3DMeshCompletionSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetTripo3DTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task Tripo3D_GetAutoRigTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.Tripo3DAutoRigSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetTripo3DTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task Tripo3D_GetAnimationRetargetTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Tasks.Tripo3DAnimationRetargetSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetTripo3DTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }
}
