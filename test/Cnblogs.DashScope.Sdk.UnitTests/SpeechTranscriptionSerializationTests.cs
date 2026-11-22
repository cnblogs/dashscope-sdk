using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class SpeechTranscriptionSerializationTests
{
    [Theory]
    [MemberData(nameof(CreateTaskData))]
    public async Task SpeechTranscription_CreateTask_SuccessAsync(
        RequestSnapshot<ModelRequest<SpeechTranscriptionInput, SpeechTranscriptionParameters>,
            ModelResponse<SpeechTranscriptionOutput, SpeechTranscriptionUsage>> testCase)
    {
        // Arrange
        const bool sse = false;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateSpeechTranscriptionTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m
                => m.Headers.GetValues("X-DashScope-Async").First() == "enable"
                   && Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Theory]
    [MemberData(nameof(GetTaskData))]
    public async Task SpeechTranscription_GetTask_SuccessAsync(
        RequestSnapshot<DashScopeTask<SpeechTranscriptionOutput, SpeechTranscriptionUsage>> testCase)
    {
        // Arrange
        const bool sse = false;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetSpeechTranscriptionTaskAsync(testCase.ResponseModel.Output.TaskId);

        // Assert
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    public static TheoryData<RequestSnapshot<ModelRequest<SpeechTranscriptionInput, SpeechTranscriptionParameters>,
        ModelResponse<SpeechTranscriptionOutput, SpeechTranscriptionUsage>>> CreateTaskData
        => new()
        {
            Snapshots.SpeechTranscription.CreateTask,
            Snapshots.SpeechTranscription.QwenAudioFiletransCreateTask,
            Snapshots.SpeechTranscription.FunAsrCreateTask,
            Snapshots.SpeechTranscription.ParaformerCreateTask
        };

    public static TheoryData<RequestSnapshot<DashScopeTask<SpeechTranscriptionOutput, SpeechTranscriptionUsage>>> GetTaskData
        => new()
        {
            Snapshots.SpeechTranscription.GetTaskSuccess,
            Snapshots.SpeechTranscription.QwenAudioFiletransGetTaskSuccess,
            Snapshots.SpeechTranscription.FunAsrGetTaskSuccess,
            Snapshots.SpeechTranscription.ParaformerGetTaskSuccess
        };
}
