using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class VideoSynthesisSerializationTests
{
    [Fact]
    public async Task VideoSynthesis_CreateT2vTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.VideoSynthesis.CreateT2vTask;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateVideoSynthesisTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task VideoSynthesis_CreateI2vTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.VideoSynthesis.CreateI2vTask;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateVideoSynthesisTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task VideoSynthesis_CreateWan3T2vTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.VideoSynthesis.CreateWan3T2vTask;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateVideoSynthesisTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task VideoSynthesis_CreateWan27T2vTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.VideoSynthesis.CreateWan27T2vTask;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateVideoSynthesisTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task VideoSynthesis_CreateWan27I2vTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.VideoSynthesis.CreateWan27I2vTask;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateVideoSynthesisTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task VideoSynthesis_CreateWan27R2vTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.VideoSynthesis.CreateWan27R2vTask;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateVideoSynthesisTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task VideoSynthesis_CreateWan27VideoEditTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.VideoSynthesis.CreateWan27VideoEditTask;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateVideoSynthesisTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task VideoSynthesis_CreateWan26T2vTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.VideoSynthesis.CreateWan26T2vTask;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateVideoSynthesisTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task VideoSynthesis_CreateWan26I2vTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.VideoSynthesis.CreateWan26I2vTask;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateVideoSynthesisTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task VideoSynthesis_CreateWan26R2vTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.VideoSynthesis.CreateWan26R2vTask;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateVideoSynthesisTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task VideoSynthesis_CreateVaceImageReferenceTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.VideoSynthesis.CreateVaceImageReferenceTask;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateVideoSynthesisTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task VideoSynthesis_CreateVaceVideoRepaintingTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.VideoSynthesis.CreateVaceVideoRepaintingTask;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateVideoSynthesisTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }
}
