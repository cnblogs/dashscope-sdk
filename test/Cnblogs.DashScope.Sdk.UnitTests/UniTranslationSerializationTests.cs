using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class UniTranslationSerializationTests
{
    [Theory]
    [MemberData(nameof(NoSseData))]
    public async Task GetUniTranslation_TextsAndImage_SuccessAsync(
        RequestSnapshot<ModelRequest<UniTranslationInput>, ModelResponse<UniTranslationOutput, UniTranslationUsage>> testCase)
    {
        // Arrange
        const bool sse = false;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.GetUniTranslationAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m
                => m.Headers.Contains("X-DashScope-Async") == false
                   && ("/api/v1" + m.RequestUri!.PathAndQuery) == testCase.GetRequestPathAndQuery(sse)
                   && Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Theory]
    [MemberData(nameof(CreateTaskData))]
    public async Task CreateUniTranslationTask_File_SuccessAsync(
        RequestSnapshot<ModelRequest<UniTranslationInput>, ModelResponse<UniTranslationOutput, UniTranslationUsage>> testCase)
    {
        // Arrange
        const bool sse = false;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateUniTranslationTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m
                => m.Headers.GetValues("X-DashScope-Async").First() == "enable"
                   && ("/api/v1" + m.RequestUri!.PathAndQuery) == testCase.GetRequestPathAndQuery(sse)
                   && Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Theory]
    [MemberData(nameof(GetTaskData))]
    public async Task GetUniTranslationTask_FinishedTask_SuccessAsync(
        RequestSnapshot<DashScopeTask<UniTranslationOutput, UniTranslationUsage>> testCase)
    {
        // Arrange
        const bool sse = false;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetUniTranslationTaskAsync(testCase.ResponseModel.Output.TaskId!);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m
                => m.Method == HttpMethod.Get
                   && ("/api/v1" + m.RequestUri!.PathAndQuery) == testCase.GetRequestPathAndQuery(sse)),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    public static TheoryData<RequestSnapshot<ModelRequest<UniTranslationInput>, ModelResponse<UniTranslationOutput, UniTranslationUsage>>>
        NoSseData
        => new()
        {
            Snapshots.UniTranslation.Texts,
            Snapshots.UniTranslation.Image
        };

    public static TheoryData<RequestSnapshot<ModelRequest<UniTranslationInput>, ModelResponse<UniTranslationOutput, UniTranslationUsage>>>
        CreateTaskData
        => new()
        {
            Snapshots.UniTranslation.CreateTask,
            Snapshots.UniTranslation.CreateDownloadFailureTask
        };

    public static TheoryData<RequestSnapshot<DashScopeTask<UniTranslationOutput, UniTranslationUsage>>> GetTaskData
        => new()
        {
            Snapshots.UniTranslation.GetTaskSuccess,
            Snapshots.UniTranslation.GetTaskFailed
        };
}
