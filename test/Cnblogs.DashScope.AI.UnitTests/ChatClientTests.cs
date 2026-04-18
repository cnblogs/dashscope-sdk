using System.ComponentModel;
using System.Text;
using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using Microsoft.Extensions.AI;
using NSubstitute;
using NSubstitute.Extensions;

namespace Cnblogs.DashScope.AI.UnitTests;

public class ChatClientTests
{
    [Fact]
    public async Task ChatClient_TextCompletionRaw_SuccessAsync()
    {
        // Arrange
        var testCase = Snapshots.TextGeneration.MessageFormat.ConversationMessageWithDocUrlsIncremental;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        dashScopeClient.Configure()
            .GetTextCompletionStreamAsync(
                Arg.Any<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(),
                Arg.Any<CancellationToken>())
            .Returns(AsyncEnumerable.Repeat(testCase.ResponseModel, 1));
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);

        // Act
        var response = client.GetStreamingResponseAsync(
            testCase.RequestModel.Input.Messages!.Select(m => new ChatMessage { RawRepresentation = m }),
            new ChatOptions
            {
                AdditionalProperties = new AdditionalPropertiesDictionary
                {
                    { "raw", testCase.RequestModel.Parameters }
                }
            });
        var responseText = new StringBuilder();
        await foreach (var chatResponseUpdate in response)
        {
            responseText.Append(chatResponseUpdate.Text);
        }

        // Assert
        _ = dashScopeClient.Received().GetTextCompletionStreamAsync(
            Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(m
                => m.IsEquivalent(testCase.RequestModel)),
            Arg.Any<CancellationToken>());
        Assert.Equal(testCase.ResponseModel.Output.Choices![0].Message.Content, responseText.ToString());
    }

    [Fact]
    public async Task ChatClient_TextCompletion_SuccessAsync()
    {
        // Arrange
        var testCase = Snapshots.TextGeneration.MessageFormat.SingleChatClientMessage;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        dashScopeClient.Configure()
            .GetTextCompletionAsync(
                Arg.Any<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(testCase.ResponseModel));
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);
        var content = testCase.RequestModel.Input.Messages!.First().Content;
        var parameter = testCase.RequestModel.Parameters;

        // Act
        var response = await client.GetResponseAsync(
            content,
            new ChatOptions
            {
                FrequencyPenalty = parameter?.RepetitionPenalty,
                PresencePenalty = parameter?.PresencePenalty,
                ModelId = testCase.RequestModel.Model,
                MaxOutputTokens = parameter?.MaxTokens,
                Seed = (long?)parameter?.Seed,
                Temperature = parameter?.Temperature,
                TopK = parameter?.TopK,
                TopP = parameter?.TopP,
                ToolMode = ChatToolMode.Auto
            });

        // Assert
        _ = dashScopeClient.Received().GetTextCompletionAsync(
            Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(m
                => m.IsEquivalent(testCase.RequestModel)),
            Arg.Any<CancellationToken>());
        Assert.Equal(testCase.ResponseModel.Output.Choices![0].Message.Content, response.Messages[0].Text);
    }

    [Fact]
    public async Task ChatClient_TextCompletionStream_SuccessAsync()
    {
        // Arrange
        var testCase = Snapshots.TextGeneration.MessageFormat.SingleMessageChatClientIncremental;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        var returnThis = new[] { testCase.ResponseModel }.ToAsyncEnumerable();
        dashScopeClient
            .Configure()
            .GetTextCompletionStreamAsync(
                Arg.Any<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(),
                Arg.Any<CancellationToken>())
            .Returns(returnThis);
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);
        var content = testCase.RequestModel.Input.Messages!.First().Content;
        var parameter = testCase.RequestModel.Parameters;

        // Act
        var response = client.GetStreamingResponseAsync(
            content,
            new ChatOptions
            {
                FrequencyPenalty = parameter?.RepetitionPenalty,
                PresencePenalty = parameter?.PresencePenalty,
                ModelId = testCase.RequestModel.Model,
                MaxOutputTokens = parameter?.MaxTokens,
                Seed = (long?)parameter?.Seed,
                Temperature = parameter?.Temperature,
                TopK = parameter?.TopK,
                TopP = parameter?.TopP,
                StopSequences = new List<string> { "你好" },
                ToolMode = ChatToolMode.Auto
            });
        var text = new StringBuilder();
        await foreach (var update in response)
        {
            text.Append(update.Text);
        }

        // Assert
        _ = dashScopeClient.Received().GetTextCompletionStreamAsync(
            Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(m
                => m.IsEquivalent(testCase.RequestModel)),
            Arg.Any<CancellationToken>());
        Assert.Equal(testCase.ResponseModel.Output.Choices![0].Message.Content, text.ToString());
    }

    [Fact]
    public async Task ChatClient_ImageRecognition_SuccessAsync()
    {
        // Arrange
        var testCase = Snapshots.MultimodalGeneration.VlChatClientNoSse;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        dashScopeClient.Configure()
            .GetMultimodalGenerationAsync(
                Arg.Any<ModelRequest<MultimodalInput, IMultimodalParameters>>(),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(testCase.ResponseModel));
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);
        var contents = testCase.RequestModel.Input.Messages.Last().Content;
        var messages = new List<ChatMessage>
        {
            new(
                ChatRole.User,
                new List<AIContent>
                {
                    new DataContent(contents[0].Image!, "image/png"), new TextContent(contents[1].Text)
                }),
        };
        var parameter = testCase.RequestModel.Parameters;

        // Act
        var response = await client.GetResponseAsync(
            messages,
            new ChatOptions
            {
                FrequencyPenalty = parameter?.RepetitionPenalty,
                PresencePenalty = parameter?.PresencePenalty,
                ModelId = testCase.RequestModel.Model,
                MaxOutputTokens = parameter?.MaxTokens,
                Seed = (long?)parameter?.Seed,
                Temperature = parameter?.Temperature,
                TopK = parameter?.TopK,
                TopP = parameter?.TopP,
            });

        // Assert
        await dashScopeClient.Received().GetMultimodalGenerationAsync(
            Arg.Is<ModelRequest<MultimodalInput, IMultimodalParameters>>(m => m.IsEquivalent(testCase.RequestModel)),
            Arg.Any<CancellationToken>());
        Assert.Equal(testCase.ResponseModel.Output.Choices[0].Message.Content[0].Text, response.Messages[0].Text);
    }

    [Fact]
    public async Task ChatClient_ImageRecognitionStream_SuccessAsync()
    {
        // Arrange
        var testCase = Snapshots.MultimodalGeneration.VlChatClientSse;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        dashScopeClient.Configure()
            .GetMultimodalGenerationStreamAsync(
                Arg.Any<ModelRequest<MultimodalInput, IMultimodalParameters>>(),
                Arg.Any<CancellationToken>())
            .Returns(new[] { testCase.ResponseModel }.ToAsyncEnumerable());
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);
        var contents = testCase.RequestModel.Input.Messages.Last().Content;
        var messages = new List<ChatMessage>
        {
            new(
                ChatRole.User,
                new List<AIContent>
                {
                    new DataContent(contents[0].Image!, "image/png"), new TextContent(contents[1].Text)
                })
        };
        var parameter = testCase.RequestModel.Parameters;

        // Act
        var response = client.GetStreamingResponseAsync(
            messages,
            new ChatOptions
            {
                FrequencyPenalty = parameter?.RepetitionPenalty,
                PresencePenalty = parameter?.PresencePenalty,
                ModelId = testCase.RequestModel.Model,
                MaxOutputTokens = parameter?.MaxTokens,
                Seed = (long?)parameter?.Seed,
                Temperature = parameter?.Temperature,
                TopK = parameter?.TopK,
                TopP = parameter?.TopP,
            });
        var text = new StringBuilder();
        await foreach (var update in response)
        {
            text.Append(update.Text);
        }

        // Assert
        _ = dashScopeClient.Received().GetMultimodalGenerationStreamAsync(
            Arg.Is<ModelRequest<MultimodalInput, IMultimodalParameters>>(m => m.IsEquivalent(testCase.RequestModel)),
            Arg.Any<CancellationToken>());
        Assert.Equal(testCase.ResponseModel.Output.Choices.First().Message.Content[0].Text, text.ToString());
    }

    [Fact]
    public async Task ChatClient_TextCompletionStreamWithToolCalls_SuccessAsync()
    {
        // Arrange
        var testCase = Snapshots.MicrosoftExtensionsAi.ToolCallFirstRound;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        var returnThis = testCase.Response.ToAsyncEnumerable();
        dashScopeClient
            .Configure()
            .GetTextCompletionStreamAsync(
                Arg.Any<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(),
                Arg.Any<CancellationToken>())
            .Returns(returnThis);
        var client = dashScopeClient.AsChatClient(testCase.Request.Model);
        var content = testCase.Request.Input.Messages!.First().Content;
        var parameter = testCase.Request.Parameters;
        var weatherReporter = Substitute.For<IWeatherReporter>();
        weatherReporter.GetWeather(Arg.Any<string>()).Returns("大部多云");

        // Act
        var tool = AIFunctionFactory
            .Create(
                ([Description("要获取天气的省市名称，例如浙江省杭州市")] string location) => weatherReporter.GetWeather(location),
                "GetWeather");
        var response = client.GetStreamingResponseAsync(
            content,
            new ChatOptions
            {
                FrequencyPenalty = parameter?.RepetitionPenalty,
                PresencePenalty = parameter?.PresencePenalty,
                ModelId = testCase.Request.Model,
                MaxOutputTokens = parameter?.MaxTokens,
                Seed = (long?)parameter?.Seed,
                Temperature = parameter?.Temperature,
                TopK = parameter?.TopK,
                TopP = parameter?.TopP,
                ToolMode = ChatToolMode.Auto,
                Tools = new List<AITool> { tool },
                AllowMultipleToolCalls = parameter?.ParallelToolCalls
            });
        var functionContents = await response
            .SelectMany(c => c.Contents)
            .Where(x => x is FunctionCallContent)
            .Select(x => (FunctionCallContent)x)
            .ToListAsync();

        // Assert
        _ = dashScopeClient.Received().GetTextCompletionStreamAsync(
            Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(m
                => m.IsEquivalent(testCase.Request)),
            Arg.Any<CancellationToken>());
        Assert.Equal(2, functionContents.Count);
        Assert.Collection(
            functionContents,
            f => Assert.Equal("call_29a870e7106f45deb8add3", f.CallId),
            f => Assert.Equal("call_026e44bb31a74266949a20", f.CallId));
        weatherReporter.DidNotReceive().GetWeather(Arg.Any<string>());
    }

    [Fact]
    public async Task ChatClient_TextCompletionStreamWithToolMessages_SuccessAsync()
    {
        // Arrange
        var firstRound = Snapshots.MicrosoftExtensionsAi.ToolCallFirstRound;
        var secondRound = Snapshots.MicrosoftExtensionsAi.ToolCallSecondRound;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        var firstReply = firstRound.Response.ToAsyncEnumerable();
        var secondReply = secondRound.Response.ToAsyncEnumerable();
        dashScopeClient
            .Configure()
            .GetTextCompletionStreamAsync(
                Arg.Any<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(),
                Arg.Any<CancellationToken>())
            .Returns(firstReply, secondReply);
        var client = dashScopeClient.AsChatClient(firstRound.Request.Model).AsBuilder().UseFunctionInvocation().Build();
        var content = firstRound.Request.Input.Messages!.First().Content;
        var parameter = firstRound.Request.Parameters;
        var weatherReporter = Substitute.For<IWeatherReporter>();
        weatherReporter.GetWeather(Arg.Any<string>()).Returns("大部多云");

        // Act
        var tool = AIFunctionFactory
            .Create(
                ([Description("要获取天气的省市名称，例如浙江省杭州市")] string location) => weatherReporter.GetWeather(location),
                "GetWeather");
        var response = client.GetStreamingResponseAsync(
            content,
            new ChatOptions
            {
                FrequencyPenalty = parameter?.RepetitionPenalty,
                PresencePenalty = parameter?.PresencePenalty,
                ModelId = firstRound.Request.Model,
                MaxOutputTokens = parameter?.MaxTokens,
                Seed = (long?)parameter?.Seed,
                Temperature = parameter?.Temperature,
                TopK = parameter?.TopK,
                TopP = parameter?.TopP,
                ToolMode = ChatToolMode.Auto,
                Tools = new List<AITool> { tool },
                AllowMultipleToolCalls = parameter?.ParallelToolCalls
            });
        var text = new StringBuilder();
        var functionContents = new List<FunctionCallContent>();
        await foreach (var update in response)
        {
            foreach (var updateContent in update.Contents)
            {
                if (updateContent is FunctionCallContent f)
                {
                    functionContents.Add(f);
                }
            }

            text.Append(update.Text);
        }

        // Assert
        _ = dashScopeClient.Received().GetTextCompletionStreamAsync(
            Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(m
                => m.IsEquivalent(firstRound.Request)),
            Arg.Any<CancellationToken>());
        Assert.Collection(
            functionContents,
            f => Assert.Equal("call_29a870e7106f45deb8add3", f.CallId),
            f => Assert.Equal("call_026e44bb31a74266949a20", f.CallId));
        weatherReporter.Received().GetWeather(Arg.Is("浙江省杭州市"));
        weatherReporter.Received().GetWeather(Arg.Is("上海市"));
        _ = dashScopeClient.Received()
            .GetTextCompletionStreamAsync(
                Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(m
                    => m.IsEquivalent(secondRound.Request)),
                Arg.Any<CancellationToken>());
        Assert.Equal(secondRound.Response[0].Output.Choices?.First().Message.Content.Text, text.ToString());
    }

    [Fact]
    public async Task ChatClient_MultimodalStreamWithToolCalls_SuccessAsync()
    {
        // Arrange
        var testCase = Snapshots.MicrosoftExtensionsAi.MultimodalToolCallFirstRound;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        var returnThis = testCase.Response.ToAsyncEnumerable();
        dashScopeClient
            .Configure()
            .GetMultimodalGenerationStreamAsync(
                Arg.Any<ModelRequest<MultimodalInput, IMultimodalParameters>>(),
                Arg.Any<CancellationToken>())
            .Returns(returnThis);
        var client = dashScopeClient.AsChatClient(testCase.Request.Model);
        var content = testCase.Request.Input.Messages.First().Content[0].Text!;
        var parameter = testCase.Request.Parameters;
        var weatherReporter = Substitute.For<IWeatherReporter>();
        weatherReporter.GetWeather(Arg.Any<string>()).Returns("大部多云");

        // Act
        var tool = AIFunctionFactory
            .Create(
                ([Description("要获取天气的省市名称，例如浙江省杭州市")] string location) => weatherReporter.GetWeather(location),
                "GetWeather");
        var response = client.GetStreamingResponseAsync(
            content,
            new ChatOptions
            {
                FrequencyPenalty = parameter?.RepetitionPenalty,
                PresencePenalty = parameter?.PresencePenalty,
                ModelId = testCase.Request.Model,
                MaxOutputTokens = parameter?.MaxTokens,
                Seed = (long?)parameter?.Seed,
                Temperature = parameter?.Temperature,
                TopK = parameter?.TopK,
                TopP = parameter?.TopP,
                ToolMode = ChatToolMode.Auto,
                Tools = new List<AITool> { tool },
                AllowMultipleToolCalls = parameter?.ParallelToolCalls
            });
        var functionContents = await response
            .SelectMany(c => c.Contents)
            .Where(x => x is FunctionCallContent)
            .Select(x => (FunctionCallContent)x)
            .ToListAsync();

        // Assert
        _ = dashScopeClient.Received().GetMultimodalGenerationStreamAsync(
            Arg.Is<ModelRequest<MultimodalInput, IMultimodalParameters>>(m
                => m.IsEquivalent(testCase.Request)),
            Arg.Any<CancellationToken>());
        Assert.Equal(2, functionContents.Count);
        Assert.Collection(
            functionContents,
            f => Assert.Equal("call_aa99ad078f294a3d81da41d7", f.CallId),
            f => Assert.Equal("call_aa8b6311567847e197e6ca7f", f.CallId));
        weatherReporter.DidNotReceive().GetWeather(Arg.Any<string>());
    }

    [Fact]
    public async Task ChatClient_MultimodalStreamWithToolMessages_SuccessAsync()
    {
        // Arrange
        var firstRound = Snapshots.MicrosoftExtensionsAi.MultimodalToolCallFirstRound;
        var secondRound = Snapshots.MicrosoftExtensionsAi.MultimodalToolCallSecondRound;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        var firstReply = firstRound.Response.ToAsyncEnumerable();
        var secondReply = secondRound.Response.ToAsyncEnumerable();
        dashScopeClient
            .Configure()
            .GetMultimodalGenerationStreamAsync(
                Arg.Any<ModelRequest<MultimodalInput, IMultimodalParameters>>(),
                Arg.Any<CancellationToken>())
            .Returns(firstReply, secondReply);
        var client = dashScopeClient.AsChatClient(firstRound.Request.Model).AsBuilder().UseFunctionInvocation().Build();
        var content = firstRound.Request.Input.Messages.First().Content[0].Text!;
        var parameter = firstRound.Request.Parameters;
        var weatherReporter = Substitute.For<IWeatherReporter>();
        weatherReporter.GetWeather(Arg.Any<string>()).Returns("大部多云");
        var finalText = secondRound.Response
            .Select(c => c.Output.Choices[0].Message.Content.FirstOrDefault()?.Text)
            .Aggregate(new StringBuilder(), (sb, text) => sb.Append(text))
            .ToString();
        var finalReasoning = secondRound.Response
            .Select(c => c.Output.Choices[0].Message.ReasoningContent)
            .Aggregate(new StringBuilder(), (sb, text) => sb.Append(text))
            .ToString();

        // Act
        var tool = AIFunctionFactory
            .Create(
                ([Description("要获取天气的省市名称，例如浙江省杭州市")] string location) => weatherReporter.GetWeather(location),
                "GetWeather");
        var response = client.GetStreamingResponseAsync(
            content,
            new ChatOptions
            {
                FrequencyPenalty = parameter?.RepetitionPenalty,
                PresencePenalty = parameter?.PresencePenalty,
                ModelId = firstRound.Request.Model,
                MaxOutputTokens = parameter?.MaxTokens,
                Seed = (long?)parameter?.Seed,
                Temperature = parameter?.Temperature,
                TopK = parameter?.TopK,
                TopP = parameter?.TopP,
                ToolMode = ChatToolMode.Auto,
                Tools = new List<AITool> { tool },
                AllowMultipleToolCalls = parameter?.ParallelToolCalls
            });
        var functionContents = new List<FunctionCallContent>();
        var text = new StringBuilder();
        var reasoning = new StringBuilder();
        var isSecondRound = false;
        await foreach (var update in response)
        {
            foreach (var updateContent in update.Contents)
            {
                switch (updateContent)
                {
                    case FunctionCallContent f:
                        functionContents.Add(f);
                        isSecondRound = true;
                        break;
                    case TextReasoningContent r:
                        if (isSecondRound)
                        {
                            reasoning.Append(r.Text);
                        }

                        break;
                    case TextContent t:
                        if (isSecondRound)
                        {
                            text.Append(t.Text);
                        }

                        break;
                }
            }
        }

        // Assert
        _ = dashScopeClient.Received().GetMultimodalGenerationStreamAsync(
            Arg.Is<ModelRequest<MultimodalInput, IMultimodalParameters>>(m
                => m.IsEquivalent(firstRound.Request)),
            Arg.Any<CancellationToken>());
        Assert.Equal(2, functionContents.Count);
        Assert.Collection(
            functionContents,
            f => Assert.Equal("call_aa99ad078f294a3d81da41d7", f.CallId),
            f => Assert.Equal("call_aa8b6311567847e197e6ca7f", f.CallId));
        _ = dashScopeClient.Received().GetMultimodalGenerationStreamAsync(
            Arg.Is<ModelRequest<MultimodalInput, IMultimodalParameters>>(m
                => m.IsEquivalent(secondRound.Request)),
            Arg.Any<CancellationToken>());
        Assert.Equal(finalText, text.ToString());
        Assert.Equal(finalReasoning, reasoning.ToString());
    }
}
