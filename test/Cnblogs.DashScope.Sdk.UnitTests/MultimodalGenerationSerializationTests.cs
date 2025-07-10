﻿using System.Text;
using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class MultimodalGenerationSerializationTests
{
    [Theory]
    [MemberData(nameof(NoSseData))]
    public async Task MultimodalGeneration_NoSse_SuccessAsync(
        RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
            ModelResponse<MultimodalOutput, MultimodalTokenUsage>> testCase)
    {
        // Arrange
        const bool sse = false;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.GetMultimodalGenerationAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Theory]
    [MemberData(nameof(SseData))]
    public async Task MultimodalGeneration_Sse_SuccessAsync(
        RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
            ModelResponse<MultimodalOutput, MultimodalTokenUsage>> testCase)
    {
        // Arrange
        const bool sse = true;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var message = new StringBuilder();
        var outputs = await client.GetMultimodalGenerationStreamAsync(testCase.RequestModel).ToListAsync();
        outputs.ForEach(x
            => message.Append(x.Output.Choices[0].Message.Content.FirstOrDefault()?.Text ?? string.Empty));

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.All(outputs.SkipLast(1), x => Assert.Equal("null", x.Output.Choices[0].FinishReason));
        Assert.Equal(testCase.ResponseModel.Output.Choices[0].Message.Content[0].Text, message.ToString());
        var last = outputs.Last();
        last = last with
        {
            Output = new MultimodalOutput(
                new List<MultimodalChoice>
                {
                    last.Output.Choices[0] with
                    {
                        Message = last.Output.Choices[0].Message with
                        {
                            Content = testCase.ResponseModel.Output.Choices[0].Message.Content
                        }
                    }
                })
        };
        Assert.Equivalent(last, testCase.ResponseModel);
    }

    public static TheoryData<RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
        ModelResponse<MultimodalOutput, MultimodalTokenUsage>>> NoSseData
        => new()
        {
            Snapshots.MultimodalGeneration.VlNoSse,
            Snapshots.MultimodalGeneration.AudioNoSse,
            Snapshots.MultimodalGeneration.OcrNoSse,
            Snapshots.MultimodalGeneration.VideoNoSse
        };

    public static TheoryData<RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
        ModelResponse<MultimodalOutput, MultimodalTokenUsage>>> SseData
        => new()
        {
            Snapshots.MultimodalGeneration.VlSse,
            Snapshots.MultimodalGeneration.AudioSse,
            Snapshots.MultimodalGeneration.OcrSse,
            Snapshots.MultimodalGeneration.VideoSse
        };
}
