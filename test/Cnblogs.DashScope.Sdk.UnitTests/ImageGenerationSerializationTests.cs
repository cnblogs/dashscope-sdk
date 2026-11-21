using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;

using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class ImageGenerationSerializationTests
{
    [Fact]
    public async Task ImageGeneration_CreateTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.ImageGeneration.CreateTaskNoSse;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateImageGenerationTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Theory]
    [MemberData(nameof(CreateKlingViduTaskData))]
    public async Task ImageGeneration_CreateKlingViduTask_SuccessAsync(
        RequestSnapshot<ModelRequest<ImageGenerationInput, IMultimodalParameters>,
            ModelResponse<ImageGenerationOutput, MultimodalTokenUsage>> testCase)
    {
        // Arrange
        const bool sse = false;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateImageGenerationTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m
                => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))
                   && m.Headers.GetValues("X-DashScope-Async").First() == "enable"),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    public static TheoryData<RequestSnapshot<ModelRequest<ImageGenerationInput, IMultimodalParameters>,
        ModelResponse<ImageGenerationOutput, MultimodalTokenUsage>>> CreateKlingViduTaskData
        => new()
        {
            Snapshots.ImageGeneration.KlingT2iTaskNoSse,
            Snapshots.ImageGeneration.KlingOmniSeriesTaskNoSse,
            Snapshots.ImageGeneration.ViduR2iTaskNoSse
        };
}
