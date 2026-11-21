using System.Text;
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
        var reasoning = new StringBuilder();
        var outputs = await client.GetMultimodalGenerationStreamAsync(testCase.RequestModel).ToListAsync();
        outputs.ForEach(x
            =>
        {
            message.Append(x.Output.Choices[0].Message.Content.FirstOrDefault()?.Text ?? string.Empty);
            reasoning.Append(x.Output.Choices[0].Message.ReasoningContent);
        });

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m
                => m.Headers.Accept.ToString() == "text/event-stream"
                   && m.Headers.GetValues("X-DashScope-SSE").First() == "enable"
                   && Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.All(outputs.SkipLast(1), x => Assert.Equal("null", x.Output.Choices[0].FinishReason));
        Assert.Equal(testCase.ResponseModel.Output.Choices[0].Message.Content[0].Text, message.ToString());
        Assert.Equal(testCase.ResponseModel.Output.Choices[0].Message.ReasoningContent, reasoning.Length > 0 ? reasoning.ToString() : null);
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
                            Content = testCase.ResponseModel.Output.Choices[0].Message.Content,
                            ReasoningContent = testCase.ResponseModel.Output.Choices[0].Message.ReasoningContent
                        }
                    }
                })
        };
        Assert.Equivalent(testCase.ResponseModel, last);
    }

    [Fact]
    public async Task MultimodalGeneration_FunctionCall_SuccessAsync()
    {
        // Arrange
        var testCase = Snapshots.MultimodalGeneration.FunctionCallSse;
        const bool sse = true;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var message = new StringBuilder();
        var reasoning = new StringBuilder();
        var toolCalls = new Dictionary<int, ToolCall>();
        var outputs = await client.GetMultimodalGenerationStreamAsync(testCase.RequestModel).ToListAsync();
        outputs.ForEach(x =>
        {
            message.Append(x.Output.Choices[0].Message.Content.FirstOrDefault()?.Text ?? string.Empty);
            reasoning.Append(x.Output.Choices[0].Message.ReasoningContent);
            var messageToolCalls = x.Output.Choices[0].Message.ToolCalls;

            if (messageToolCalls == null)
            {
                return;
            }

            foreach (var toolCall in messageToolCalls)
            {
                var isNew = toolCalls.TryAdd(toolCall.Index, toolCall);
                if (!isNew)
                {
                    toolCalls[toolCall.Index].Function.Arguments += toolCall.Function.Arguments;
                }
            }
        });
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
                            Content = new List<MultimodalMessageContent>()
                            {
                                MultimodalMessageContent.TextContent(message.ToString())
                            },
                            ReasoningContent = reasoning.ToString(),
                            ToolCalls = toolCalls.Values.ToList()
                        }
                    }
                })
        };

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m
                => m.Headers.Accept.ToString() == "text/event-stream"
                   && m.Headers.GetValues("X-DashScope-SSE").First() == "enable"
                   && Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.All(outputs.SkipLast(1), x => Assert.Equal("null", x.Output.Choices[0].FinishReason));

        Assert.Equal("tool_calls", last.Output.Choices.First().FinishReason);
        Assert.Equivalent(testCase.ResponseModel, last);
    }

    [Fact]
    public async Task ImageGeneration_CreateWan26T2iTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.MultimodalGeneration.Wan26T2iTaskNoSse;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateImageGenerationTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task MultimodalGeneration_QwenTtsSse_SuccessAsync()
    {
        // Arrange
        var testCase = Snapshots.MultimodalGeneration.QwenTtsSse;
        const bool sse = true;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var outputs = await client.GetMultimodalGenerationStreamAsync(testCase.RequestModel).ToListAsync();

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m
                => m.Headers.Accept.ToString() == "text/event-stream"
                   && m.Headers.GetValues("X-DashScope-SSE").First() == "enable"
                   && Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        // Intermediate frames stream base64 wav chunks, the url is only filled in the last frame.
        Assert.All(outputs.SkipLast(1), x => Assert.Equal("null", x.Output.FinishReason));
        Assert.All(outputs.SkipLast(1), x => Assert.True(string.IsNullOrEmpty(x.Output.Audio!.Url)));
        Assert.Contains(outputs.SkipLast(1), x => x.Output.Audio!.Data?.Length > 0);
        Assert.Equivalent(testCase.ResponseModel, outputs.Last());
    }

    [Fact]
    public async Task MultimodalGeneration_MinimaxSpeechNoSse_SuccessAsync()
    {
        // Arrange
        var testCase = Snapshots.MultimodalGeneration.MinimaxSpeechNoSse;
        const bool sse = false;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.GetMultimodalGenerationAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        // Data.Audio is a placeholder in the snapshot, the hex audio itself stays in the raw
        // fixture; assert its wire invariants (valid hex, byte count matches ExtraInfo) instead.
        var expected = testCase.ResponseModel with
        {
            Output = testCase.ResponseModel.Output with { Data = response.Output.Data }
        };
        Assert.Equivalent(expected, response);
        var audio = Convert.FromHexString(response.Output.Data!.Audio!);
        Assert.Equal(response.Output.ExtraInfo!.AudioSize, audio.Length);
    }

    [Fact]
    public async Task MultimodalGeneration_MinimaxSpeechSse_SuccessAsync()
    {
        // Arrange
        var testCase = Snapshots.MultimodalGeneration.MinimaxSpeechSse;
        const bool sse = true;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var outputs = await client.GetMultimodalGenerationStreamAsync(testCase.RequestModel).ToListAsync();

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m
                => m.Headers.Accept.ToString() == "text/event-stream"
                   && m.Headers.GetValues("X-DashScope-SSE").First() == "enable"
                   && Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.All(outputs, x => Assert.Equal(0, x.Output.BaseResp!.StatusCode));
        // Intermediate frames stream hex chunks with status 1, the last frame carries the
        // aggregated audio of the whole synthesis with status 2.
        Assert.All(outputs.SkipLast(1), x => Assert.Equal(1, x.Output.Data!.Status));
        Assert.Contains(outputs.SkipLast(1), x => x.Output.Data!.Audio?.Length > 0);
        var last = outputs.Last();
        var expected = testCase.ResponseModel with
        {
            Output = testCase.ResponseModel.Output with { Data = last.Output.Data }
        };
        Assert.Equivalent(expected, last);
        var audio = Convert.FromHexString(last.Output.Data!.Audio!);
        Assert.Equal(last.Output.ExtraInfo!.AudioSize, audio.Length);
    }

    public static TheoryData<RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
        ModelResponse<MultimodalOutput, MultimodalTokenUsage>>> NoSseData
        => new()
        {
            Snapshots.MultimodalGeneration.VlNoSse,
            Snapshots.MultimodalGeneration.AudioNoSse,
            Snapshots.MultimodalGeneration.OcrNoSse,
            Snapshots.MultimodalGeneration.OcrAdvancedRecognitionNoSse,
            Snapshots.MultimodalGeneration.OcrKeyInformationExtractionNoSse,
            Snapshots.MultimodalGeneration.VideoNoSse,
            Snapshots.MultimodalGeneration.WebSearchNoSse,
            Snapshots.MultimodalGeneration.ImageGenerationNoSse,
            Snapshots.MultimodalGeneration.QwenImageEditNoSse,
            Snapshots.MultimodalGeneration.ZImageTurboNoSse,
            Snapshots.MultimodalGeneration.Wan26T2iNoSse,
            Snapshots.MultimodalGeneration.Wan26ImageEditNoSse,
            Snapshots.MultimodalGeneration.Wan27T2iNoSse,
            Snapshots.MultimodalGeneration.Wan27InteractiveEditNoSse,
            Snapshots.MultimodalGeneration.Wan27SequentialNoSse,
            Snapshots.MultimodalGeneration.JsonSchemaNoSse,
            Snapshots.MultimodalGeneration.QwenTtsNoSse
        };

    public static TheoryData<RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
        ModelResponse<MultimodalOutput, MultimodalTokenUsage>>> SseData
        => new()
        {
            Snapshots.MultimodalGeneration.VlSse,
            Snapshots.MultimodalGeneration.VlImageHwOutputSse,
            Snapshots.MultimodalGeneration.AudioSse,
            Snapshots.MultimodalGeneration.AudioCaptionSse,
            Snapshots.MultimodalGeneration.OcrSse,
            Snapshots.MultimodalGeneration.VideoSse,
            Snapshots.MultimodalGeneration.VultureSse,
            Snapshots.MultimodalGeneration.VultureLimitsSse,
            Snapshots.MultimodalGeneration.OssVideoSse,
            Snapshots.MultimodalGeneration.ToolUseSse,
            Snapshots.MultimodalGeneration.JsonSchemaSse
        };
}
