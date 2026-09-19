using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class SpeechTranscriptionSerializationTests
{
    [Fact]
    public async Task SpeechTranscription_CreateTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.SpeechTranscription.CreateTask;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateSpeechTranscriptionTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m
                => m.Headers.Contains("X-DashScope-Async")
                   && Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task SpeechTranscription_CreateTaskWithOss_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.SpeechTranscription.CreateTaskWithOss;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateSpeechTranscriptionTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m
                => m.Headers.Contains("X-DashScope-Async")
                   && m.Headers.GetValues("X-DashScope-OssResourceResolve").First() == "enable"
                   && Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task SpeechTranscription_GetTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.SpeechTranscription.GetTaskSuccess;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetSpeechTranscriptionTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task SpeechTranscription_DownloadResult_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.SpeechTranscription.DownloadResult;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var result = await client.GetSpeechTranscriptionResultAsync(
            "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/result.json");

        // Assert
        Assert.Equivalent(testCase.ResponseModel, result);
    }
}
