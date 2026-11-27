using System.ComponentModel;
using System.Text;
using System.Text.Json;
using Cnblogs.DashScope.AI.Internals;
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
    public async Task ChatClient_TextCompletionWithRawRepresentation_DoesNotDuplicateMessage_SuccessAsync()
    {
        // Arrange
        var testCase = Snapshots.MicrosoftExtensionsAi.SingleChatClientMessage;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        var actualRequests = new List<ModelRequest<TextGenerationInput, ITextGenerationParameters>>();
        dashScopeClient.Configure()
            .GetTextCompletionAsync(
                Arg.Do<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(actualRequests.Add),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(testCase.ResponseModel));
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);
        var rawMessage = TextChatMessage.User("你好");
        var message = new ChatMessage(ChatRole.User, "你好") { RawRepresentation = rawMessage };

        // Act
        await client.GetResponseAsync([message]);

        // Assert
        var sentMessages = Assert.Single(actualRequests).Input.Messages!;
        Assert.Same(rawMessage, Assert.Single(sentMessages));
    }

    [Fact]
    public async Task ChatClient_TextCompletion_SuccessAsync()
    {
        // Arrange
        var testCase = Snapshots.MicrosoftExtensionsAi.SingleChatClientMessage;
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
    public async Task ChatClient_TextCompletionWithReasoningContent_SuccessAsync()
    {
        // Arrange
        var testCase = Snapshots.TextGeneration.MessageFormat.SingleMessageReasoningEffort;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        dashScopeClient.Configure()
            .GetTextCompletionAsync(
                Arg.Any<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(testCase.ResponseModel));
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);
        var content = testCase.RequestModel.Input.Messages!.First().Content;

        // Act
        var response = await client.GetResponseAsync(
            content,
            new ChatOptions
            {
                AdditionalProperties = new AdditionalPropertiesDictionary
                {
                    { "raw", testCase.RequestModel.Parameters }
                }
            });

        // Assert
        _ = dashScopeClient.Received().GetTextCompletionAsync(
            Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(m
                => m.IsEquivalent(testCase.RequestModel)),
            Arg.Any<CancellationToken>());
        var message = testCase.ResponseModel.Output.Choices![0].Message;
        Assert.Collection(
            response.Messages[0].Contents,
            c => Assert.Equal(message.ReasoningContent, Assert.IsType<TextReasoningContent>(c).Text),
            c => Assert.Equal(message.Content, Assert.IsType<TextContent>(c).Text));
    }

    [Fact]
    public async Task ChatClient_TextCompletionWithReasoningOnlyContent_SuccessAsync()
    {
        // Arrange
        var testCase = Snapshots.TextGeneration.MessageFormat.SingleMessageReasoning;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        dashScopeClient.Configure()
            .GetTextCompletionAsync(
                Arg.Any<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(testCase.ResponseModel));
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);
        var content = testCase.RequestModel.Input.Messages!.First().Content;

        // Act
        var response = await client.GetResponseAsync(
            content,
            new ChatOptions
            {
                AdditionalProperties = new AdditionalPropertiesDictionary
                {
                    { "raw", testCase.RequestModel.Parameters }
                }
            });

        // Assert
        _ = dashScopeClient.Received().GetTextCompletionAsync(
            Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(m
                => m.IsEquivalent(testCase.RequestModel)),
            Arg.Any<CancellationToken>());
        var reasoning = Assert.IsType<TextReasoningContent>(Assert.Single(response.Messages[0].Contents));
        Assert.Equal(testCase.ResponseModel.Output.Choices![0].Message.ReasoningContent, reasoning.Text);
        Assert.Equal(string.Empty, response.Messages[0].Text);
    }

    [Fact]
    public async Task ChatClient_TextCompletionStream_SuccessAsync()
    {
        // Arrange
        var testCase = Snapshots.MicrosoftExtensionsAi.SingleMessageChatClientIncremental;
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
        var contents = testCase.RequestModel.Input.Messages!.Last().Content;
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
                MaxOutputTokens = parameter?.MaxCompletionTokens,
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
    public async Task ChatClient_MultimodalGenerationWithRawRepresentation_PassesRawMessages_SuccessAsync()
    {
        // Arrange
        var testCase = Snapshots.MultimodalGeneration.VlChatClientNoSse;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        var actualRequests = new List<ModelRequest<MultimodalInput, IMultimodalParameters>>();
        dashScopeClient.Configure()
            .GetMultimodalGenerationAsync(
                Arg.Do<ModelRequest<MultimodalInput, IMultimodalParameters>>(actualRequests.Add),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(testCase.ResponseModel));
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);
        var rawMessages = new List<MultimodalMessage>
        {
            MultimodalMessage.System([MultimodalMessageContent.TextContent("You are a helpful assistant.")]),
            MultimodalMessage.User([MultimodalMessageContent.TextContent("这个图片是哪里，请用简短的语言回答")])
        };
        var messages = new List<ChatMessage>
        {
            new(ChatRole.System, "You are a helpful assistant.") { RawRepresentation = rawMessages[0] },
            new(ChatRole.User, "这个图片是哪里，请用简短的语言回答") { RawRepresentation = rawMessages[1] }
        };

        // Act
        await client.GetResponseAsync(messages);

        // Assert
        var sentMessages = Assert.Single(actualRequests).Input.Messages!;
        Assert.Collection(
            sentMessages,
            m => Assert.Same(rawMessages[0], m),
            m => Assert.Same(rawMessages[1], m));
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
        var contents = testCase.RequestModel.Input.Messages!.Last().Content;
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
                MaxOutputTokens = parameter?.MaxCompletionTokens,
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
    public async Task ChatClient_AudioBytes_MapsToAudioDataUri_SuccessAsync()
    {
        // Arrange
        var testCase = Snapshots.MultimodalGeneration.OmniAudioInputSse;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        var actualRequests = new List<ModelRequest<MultimodalInput, IMultimodalParameters>>();
        dashScopeClient.Configure()
            .GetMultimodalGenerationAsync(
                Arg.Do<ModelRequest<MultimodalInput, IMultimodalParameters>>(actualRequests.Add),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(testCase.ResponseModel));
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);
        var audio = new byte[] { 1, 2, 3, 4, 5 };
        var text = testCase.RequestModel.Input.Messages!.Last().Content[1].Text;
        var messages = new List<ChatMessage>
        {
            new(
                ChatRole.User,
                new List<AIContent> { new DataContent(audio, "audio/wav"), new TextContent(text!) }),
        };

        // Act
        var response = await client.GetResponseAsync(messages);

        // Assert
        var request = Assert.Single(actualRequests);
        // the local file travels as a data uri
        Assert.Equal("data:audio/wav;base64,AQIDBAU=", request.Input.Messages!.Last().Content[0].Audio);
        Assert.Equivalent(
            new ModelRequest<MultimodalInput, IMultimodalParameters>
            {
                Model = testCase.RequestModel.Model,
                Input = new MultimodalInput
                {
                    Messages = new List<MultimodalMessage>
                    {
                        MultimodalMessage.User(
                        [
                            MultimodalMessageContent.AudioContent(audio, "audio/wav"),
                            MultimodalMessageContent.TextContent(text!)
                        ])
                    }
                },
                // a complete request does not ask for the incremental output
                Parameters = new MultimodalParameters()
            },
            request);
        Assert.Equal(testCase.ResponseModel.Output.Choices[0].Message.Content[0].Text, response.Messages[0].Text);
    }

    [Fact]
    public async Task ChatClient_AudioUrl_MapsToAudioContent_SuccessAsync()
    {
        // Arrange
        var testCase = Snapshots.MultimodalGeneration.OmniMixedInputNoSse;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        dashScopeClient.Configure()
            .GetMultimodalGenerationAsync(
                Arg.Any<ModelRequest<MultimodalInput, IMultimodalParameters>>(),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(testCase.ResponseModel));
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);
        var contents = testCase.RequestModel.Input.Messages!.Last().Content;
        var messages = new List<ChatMessage>
        {
            new(
                ChatRole.User,
                new List<AIContent>
                {
                    // the media types follow the extensions of the urls
                    new UriContent(new Uri(contents[0].Image!)), new UriContent(new Uri(contents[1].Audio!)),
                    new TextContent(contents[2].Text!)
                }),
        };

        // Act
        var response = await client.GetResponseAsync(messages);

        // Assert
        await dashScopeClient.Received().GetMultimodalGenerationAsync(
            Arg.Is<ModelRequest<MultimodalInput, IMultimodalParameters>>(m => m.IsEquivalent(testCase.RequestModel)),
            Arg.Any<CancellationToken>());
        Assert.Equal(testCase.ResponseModel.Output.Choices[0].Message.Content[0].Text, response.Messages[0].Text);
    }

    [Fact]
    public async Task ChatClient_MultimodalStream_FrameWithoutOutput_SkipsFrameAsync()
    {
        // Arrange
        var testCase = Snapshots.MultimodalGeneration.OmniAudioInputSse;
        // the web search of qwen3.8-omni-flash opens the stream with a frame that carries no output
        var openingFrame = new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
        {
            RequestId = testCase.ResponseModel.RequestId
        };
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        dashScopeClient.Configure()
            .GetMultimodalGenerationStreamAsync(
                Arg.Any<ModelRequest<MultimodalInput, IMultimodalParameters>>(),
                Arg.Any<CancellationToken>())
            .Returns(new[] { openingFrame, testCase.ResponseModel }.ToAsyncEnumerable());
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);
        var contents = testCase.RequestModel.Input.Messages!.Last().Content;
        var messages = new List<ChatMessage>
        {
            new(
                ChatRole.User,
                new List<AIContent> { new UriContent(new Uri(contents[0].Audio!)), new TextContent(contents[1].Text!) }),
        };

        // Act
        var updates = await client.GetStreamingResponseAsync(messages).ToListAsync();

        // Assert
        var update = Assert.Single(updates);
        Assert.Equal(ChatFinishReason.Stop, update.FinishReason);
        Assert.Equal(testCase.ResponseModel.Output.Choices[0].Message.Content[0].Text, update.Text);
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
                MaxOutputTokens = parameter?.MaxCompletionTokens,
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
    public async Task ChatClient_TextCompletionWithToolChoiceNone_SuccessAsync()
    {
        // Arrange
        var testCase = Snapshots.MicrosoftExtensionsAi.SingleChatClientMessageWithToolChoiceNone;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        dashScopeClient.Configure()
            .GetTextCompletionAsync(
                Arg.Any<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(testCase.ResponseModel));
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);
        var content = testCase.RequestModel.Input.Messages!.First().Content;
        var weatherReporter = Substitute.For<IWeatherReporter>();
        weatherReporter.GetWeather(Arg.Any<string>()).Returns("大部多云");
        var tool = AIFunctionFactory
            .Create(
                ([Description("要获取天气的省市名称，例如浙江省杭州市")] string location) => weatherReporter.GetWeather(location),
                "GetWeather");

        // Act
        var response = await client.GetResponseAsync(
            content,
            new ChatOptions
            {
                ModelId = testCase.RequestModel.Model,
                ToolMode = ChatToolMode.None,
                Tools = new List<AITool> { tool }
            });

        // Assert
        _ = dashScopeClient.Received().GetTextCompletionAsync(
            Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(m
                => m.IsEquivalent(testCase.RequestModel)),
            Arg.Any<CancellationToken>());
        Assert.Equal(testCase.ResponseModel.Output.Choices![0].Message.Content, response.Messages[0].Text);
        weatherReporter.DidNotReceive().GetWeather(Arg.Any<string>());
    }

    [Fact]
    public async Task ChatClient_TextCompletionWithDeclarationOnlyTool_SuccessAsync()
    {
        // Arrange
        var testCase = Snapshots.MicrosoftExtensionsAi.SingleChatClientMessageWithToolChoiceNone;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        dashScopeClient.Configure()
            .GetTextCompletionAsync(
                Arg.Any<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(testCase.ResponseModel));
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);
        var content = testCase.RequestModel.Input.Messages!.First().Content;
        var tool = AIFunctionFactory.CreateDeclaration(
            "GetWeather",
            string.Empty,
            Snapshots.GetCurrentWeatherParametersSchema);

        // Act
        var response = await client.GetResponseAsync(
            content,
            new ChatOptions
            {
                ModelId = testCase.RequestModel.Model,
                ToolMode = ChatToolMode.None,
                Tools = new List<AITool> { tool }
            });

        // Assert
        _ = dashScopeClient.Received().GetTextCompletionAsync(
            Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(m
                => m.IsEquivalent(testCase.RequestModel)),
            Arg.Any<CancellationToken>());
        Assert.Equal(testCase.ResponseModel.Output.Choices![0].Message.Content, response.Messages[0].Text);
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
                MaxOutputTokens = parameter?.MaxCompletionTokens,
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
        var content = testCase.Request.Input.Messages!.First().Content[0].Text!;
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
                MaxOutputTokens = parameter?.MaxCompletionTokens,
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
        var content = firstRound.Request.Input.Messages!.First().Content[0].Text!;
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
                MaxOutputTokens = parameter?.MaxCompletionTokens,
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

    [Fact]
    public async Task ChatClient_TextCompletionWithJsonSchemaResponseFormat_OutsideWhitelist_FallsBackToJsonObjectAsync()
    {
        // Arrange
        var testCase = Snapshots.MicrosoftExtensionsAi.SingleChatClientMessage;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        var actualRequests = new List<ModelRequest<TextGenerationInput, ITextGenerationParameters>>();
        dashScopeClient.Configure()
            .GetTextCompletionAsync(
                Arg.Do<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(actualRequests.Add),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(testCase.ResponseModel));
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);
        var schema = JsonDocument.Parse("{\"type\":\"object\"}").RootElement;

        // Act
        await client.GetResponseAsync(
            [new ChatMessage(ChatRole.User, testCase.RequestModel.Input.Messages!.First().Content)],
            new ChatOptions { ResponseFormat = ChatResponseFormat.ForJsonSchema(schema, "calculation") });

        // Assert
        var parameters = Assert.Single(actualRequests).Parameters;
        Assert.NotNull(parameters);
        Assert.Equal(DashScopeResponseFormat.JsonObject, parameters.ResponseFormat);
    }

    [Fact]
    public async Task ChatClient_TextCompletionWithJsonSchemaResponseFormatAndUseJsonSchema_MapsToJsonObjectAsync()
    {
        // Arrange
        var testCase = Snapshots.MicrosoftExtensionsAi.SingleChatClientMessage;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        var actualRequests = new List<ModelRequest<TextGenerationInput, ITextGenerationParameters>>();
        dashScopeClient.Configure()
            .GetTextCompletionAsync(
                Arg.Do<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(actualRequests.Add),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(testCase.ResponseModel));
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);
        var schema = JsonDocument.Parse("{\"type\":\"object\"}").RootElement;

        // Act
        await client.GetResponseAsync(
            [new ChatMessage(ChatRole.User, testCase.RequestModel.Input.Messages!.First().Content)],
            new ChatOptions
            {
                ResponseFormat = ChatResponseFormat.ForJsonSchema(schema, "calculation"),
                AdditionalProperties = new AdditionalPropertiesDictionary { ["useJsonSchema"] = "true" }
            });

        // Assert
        var parameters = Assert.Single(actualRequests).Parameters;
        Assert.NotNull(parameters);
        Assert.Equal(
            DashScopeResponseFormat.JsonSchema(new DashScopeJsonSchema("calculation", schema)),
            parameters.ResponseFormat);
    }

    [Fact]
    public async Task ChatClient_MultimodalCompletionWithJsonSchemaResponseFormat_InsideWhitelist_MapsToJsonSchemaAsync()
    {
        // Arrange
        var testCase = Snapshots.MultimodalGeneration.JsonSchemaNoSse;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        var actualRequests = new List<ModelRequest<MultimodalInput, IMultimodalParameters>>();
        dashScopeClient.Configure()
            .GetMultimodalGenerationAsync(
                Arg.Do<ModelRequest<MultimodalInput, IMultimodalParameters>>(actualRequests.Add),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(testCase.ResponseModel));
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);
        var schema = JsonDocument.Parse("{\"type\":\"object\"}").RootElement;

        // Act
        await client.GetResponseAsync(
            [new ChatMessage(ChatRole.User, "请问 1+1 是多少？")],
            new ChatOptions { ResponseFormat = ChatResponseFormat.ForJsonSchema(schema, "calculation") });

        // Assert
        var parameters = Assert.Single(actualRequests).Parameters;
        Assert.NotNull(parameters);
        Assert.Equal(
            DashScopeResponseFormat.JsonSchema(new DashScopeJsonSchema("calculation", schema)),
            parameters.ResponseFormat);
    }

    [Fact]
    public async Task ChatClient_MultimodalCompletionWithJsonSchemaResponseFormatAndDisabledUseJsonSchema_MapsToJsonObjectAsync()
    {
        // Arrange
        var testCase = Snapshots.MultimodalGeneration.JsonSchemaNoSse;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        var actualRequests = new List<ModelRequest<MultimodalInput, IMultimodalParameters>>();
        dashScopeClient.Configure()
            .GetMultimodalGenerationAsync(
                Arg.Do<ModelRequest<MultimodalInput, IMultimodalParameters>>(actualRequests.Add),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(testCase.ResponseModel));
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);
        var schema = JsonDocument.Parse("{\"type\":\"object\"}").RootElement;

        // Act
        await client.GetResponseAsync(
            [new ChatMessage(ChatRole.User, "请问 1+1 是多少？")],
            new ChatOptions
            {
                ResponseFormat = ChatResponseFormat.ForJsonSchema(schema, "calculation"),
                AdditionalProperties = new AdditionalPropertiesDictionary { ["useJsonSchema"] = "false" }
            });

        // Assert
        var parameters = Assert.Single(actualRequests).Parameters;
        Assert.NotNull(parameters);
        Assert.Equal(DashScopeResponseFormat.JsonObject, parameters.ResponseFormat);
    }

    [Fact]
    public async Task ChatClient_TextCompletion_Usage_MapsAllTokenDetailsAsync()
    {
        // Arrange
        var testCase = Snapshots.MicrosoftExtensionsAi.TextMessageWithFullTokenUsage;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        dashScopeClient.Configure()
            .GetTextCompletionAsync(
                Arg.Any<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(testCase.ResponseModel));
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);

        // Act
        var response = await client.GetResponseAsync(
            testCase.RequestModel.Input.Messages!.Select(m => new ChatMessage { RawRepresentation = m }),
            new ChatOptions
            {
                AdditionalProperties = new AdditionalPropertiesDictionary { { "raw", testCase.RequestModel.Parameters } }
            });

        // Assert
        _ = dashScopeClient.Received().GetTextCompletionAsync(
            Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(m
                => m.IsEquivalent(testCase.RequestModel)),
            Arg.Any<CancellationToken>());
        var expected = testCase.ResponseModel.Usage!;
        var usage = response.Usage;
        Assert.NotNull(usage);
        Assert.Equal(expected.InputTokens, usage.InputTokenCount);
        Assert.Equal(expected.OutputTokens, usage.OutputTokenCount);
        Assert.Equal(expected.TotalTokens, usage.TotalTokenCount);
        Assert.Equal(expected.PromptTokensDetails!.CachedTokens, usage.CachedInputTokenCount);
        Assert.Equal(expected.OutputTokensDetails!.ReasoningTokens, usage.ReasoningTokenCount);
        Assert.Equal((long)expected.OutputTokensDetails.TextTokens!, usage.AdditionalCounts!["OutputTokensDetails.TextTokens"]);
        Assert.Equal(expected.Plugins!.Search!.Count, usage.AdditionalCounts!["Plugins.Search.Count"]);
    }

    [Fact]
    public async Task ChatClient_MultimodalCompletion_Usage_MapsAllTokenDetailsAsync()
    {
        // Arrange
        var testCase = Snapshots.MicrosoftExtensionsAi.MultimodalMessageWithFullTokenUsage;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        dashScopeClient.Configure()
            .GetMultimodalGenerationAsync(
                Arg.Any<ModelRequest<MultimodalInput, IMultimodalParameters>>(),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(testCase.ResponseModel));
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);

        // Act
        var response = await client.GetResponseAsync(
            testCase.RequestModel.Input.Messages!.Select(m => new ChatMessage { RawRepresentation = m }),
            new ChatOptions
            {
                AdditionalProperties = new AdditionalPropertiesDictionary
                {
                    { "raw", testCase.RequestModel.Parameters },
                    { "useVl", "true" }
                }
            });

        // Assert
        _ = dashScopeClient.Received().GetMultimodalGenerationAsync(
            Arg.Is<ModelRequest<MultimodalInput, IMultimodalParameters>>(m
                => m.IsEquivalent(testCase.RequestModel)),
            Arg.Any<CancellationToken>());
        var expected = testCase.ResponseModel.Usage!;
        var usage = response.Usage;
        Assert.NotNull(usage);
        Assert.Equal(expected.InputTokens, usage.InputTokenCount);
        Assert.Equal(expected.OutputTokens, usage.OutputTokenCount);
        Assert.Equal(expected.TotalTokens, usage.TotalTokenCount);
        Assert.Equal(expected.PromptTokensDetails!.CachedTokens, usage.CachedInputTokenCount);
        Assert.Equal(expected.OutputTokensDetails!.ReasoningTokens, usage.ReasoningTokenCount);
        Assert.Equal((long)expected.InputTokensDetails!.ImageTokens!, usage.AdditionalCounts!["InputTokensDetails.ImageTokens"]);
        Assert.Equal((long)expected.InputTokensDetails.TextTokens!, usage.AdditionalCounts!["InputTokensDetails.TextTokens"]);
        Assert.Equal((long)expected.OutputTokensDetails.TextTokens!, usage.AdditionalCounts!["OutputTokensDetails.TextTokens"]);
    }

    [Fact]
    public async Task ChatClient_TextCompletionStream_Usage_OnlyReportedOnFinalChunkAsync()
    {
        // Arrange
        var testCase = Snapshots.MicrosoftExtensionsAi.TextMessageIncrementalWithCumulativeUsage;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        dashScopeClient.Configure()
            .GetTextCompletionStreamAsync(
                Arg.Any<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(),
                Arg.Any<CancellationToken>())
            .Returns(testCase.Response.ToAsyncEnumerable());
        var client = dashScopeClient.AsChatClient(testCase.Request.Model);

        // Act
        var updates = await client.GetStreamingResponseAsync(
            testCase.Request.Input.Messages!.Select(m => new ChatMessage { RawRepresentation = m }),
            new ChatOptions
            {
                AdditionalProperties = new AdditionalPropertiesDictionary { { "raw", testCase.Request.Parameters } }
            }).ToListAsync();

        // Assert - DashScope reports cumulative usage per chunk, only the final chunk carries it.
        var usageContents = updates.SelectMany(u => u.Contents.OfType<UsageContent>()).ToList();
        var expected = testCase.Response.Last().Usage!;
        var usage = Assert.Single(usageContents).Details;
        Assert.Equal(expected.InputTokens, usage.InputTokenCount);
        Assert.Equal(expected.OutputTokens, usage.OutputTokenCount);
        Assert.Equal(expected.TotalTokens, usage.TotalTokenCount);
        Assert.Equal(expected.PromptTokensDetails!.CachedTokens, usage.CachedInputTokenCount);
        Assert.Equal(expected.OutputTokensDetails!.ReasoningTokens, usage.ReasoningTokenCount);
    }

    [Fact]
    public async Task ChatClient_MultimodalCompletionStream_Usage_OnlyReportedOnFinalChunkAsync()
    {
        // Arrange
        var testCase = Snapshots.MicrosoftExtensionsAi.MultimodalMessageIncrementalWithCumulativeUsage;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        dashScopeClient.Configure()
            .GetMultimodalGenerationStreamAsync(
                Arg.Any<ModelRequest<MultimodalInput, IMultimodalParameters>>(),
                Arg.Any<CancellationToken>())
            .Returns(testCase.Response.ToAsyncEnumerable());
        var client = dashScopeClient.AsChatClient(testCase.Request.Model);

        // Act
        var updates = await client.GetStreamingResponseAsync(
            testCase.Request.Input.Messages!.Select(m => new ChatMessage { RawRepresentation = m }),
            new ChatOptions
            {
                AdditionalProperties = new AdditionalPropertiesDictionary
                {
                    { "raw", testCase.Request.Parameters },
                    { "useVl", "true" }
                }
            }).ToListAsync();

        // Assert
        var usageContents = updates.SelectMany(u => u.Contents.OfType<UsageContent>()).ToList();
        var expected = testCase.Response.Last().Usage!;
        var usage = Assert.Single(usageContents).Details;
        Assert.Equal(expected.InputTokens, usage.InputTokenCount);
        Assert.Equal(expected.OutputTokens, usage.OutputTokenCount);
        Assert.Equal(expected.TotalTokens, usage.TotalTokenCount);
        Assert.Equal(expected.OutputTokensDetails!.ReasoningTokens, usage.ReasoningTokenCount);
        Assert.NotNull(updates[0].MessageId);
    }

    [Fact]
    public async Task ChatClient_TextCompletionWithInstructions_PrependsSystemMessageAsync()
    {
        // Arrange
        var testCase = Snapshots.MicrosoftExtensionsAi.TextMessageWithInstructions;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        dashScopeClient.Configure()
            .GetTextCompletionAsync(
                Arg.Any<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(testCase.ResponseModel));
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);

        // Act
        await client.GetResponseAsync(
            [new ChatMessage(ChatRole.User, "hello")],
            new ChatOptions { Instructions = "你是一个专业翻译助手" });

        // Assert
        _ = dashScopeClient.Received().GetTextCompletionAsync(
            Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(m
                => m.IsEquivalent(testCase.RequestModel)),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ChatClient_MultimodalCompletionWithInstructions_PrependsSystemMessageAsync()
    {
        // Arrange
        var testCase = Snapshots.MicrosoftExtensionsAi.MultimodalMessageWithInstructions;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        dashScopeClient.Configure()
            .GetMultimodalGenerationAsync(
                Arg.Any<ModelRequest<MultimodalInput, IMultimodalParameters>>(),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(testCase.ResponseModel));
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);

        // Act
        await client.GetResponseAsync(
            [new ChatMessage(ChatRole.User, "hello")],
            new ChatOptions { Instructions = "你是一个专业翻译助手" });

        // Assert
        _ = dashScopeClient.Received().GetMultimodalGenerationAsync(
            Arg.Is<ModelRequest<MultimodalInput, IMultimodalParameters>>(m
                => m.IsEquivalent(testCase.RequestModel)),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ChatClient_TextCompletionWithBlankInstructions_NoSystemMessageAsync()
    {
        // Arrange
        var testCase = Snapshots.MicrosoftExtensionsAi.TextMessageWithInstructions;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        var actualRequests = new List<ModelRequest<TextGenerationInput, ITextGenerationParameters>>();
        dashScopeClient.Configure()
            .GetTextCompletionAsync(
                Arg.Do<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(actualRequests.Add),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(testCase.ResponseModel));
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);

        // Act
        await client.GetResponseAsync(
            [new ChatMessage(ChatRole.User, "hello")],
            new ChatOptions { Instructions = "  " });

        // Assert
        var messages = Assert.Single(actualRequests).Input.Messages!;
        var message = Assert.Single(messages);
        Assert.Equal("user", message.Role);
    }

    [Theory]
    [InlineData("qwen3.5-plus")]
    [InlineData("qwen3.6-flash")]
    [InlineData("qwen3.7-plus")]
    [InlineData("qwen3.7-flash")]
    [InlineData("qwen3.7-max-2026-06-08")]
    [InlineData("qwen3.8-2.4t-a95b")]
    [InlineData("qwen3.8-max")]
    [InlineData("qwen3.8-omni-flash")]
    [InlineData("qwen4-max")]
    [InlineData("qwen3-vl-plus")]
    [InlineData("gui-plus")]
    [InlineData("deepseek-v4.1-flash")]
    [InlineData("kimi-k2.6")]
    [InlineData("kimi-k3")]
    public async Task ChatClient_MultimodalWhitelistedModel_CallsMultimodalEndpointAsync(string model)
    {
        // Arrange
        var dashScopeClient = CreateEndpointRecordingClient(out var textRequests, out var multimodalRequests);
        var client = dashScopeClient.AsChatClient(model);

        // Act
        await client.GetResponseAsync("hello");
        await client.GetStreamingResponseAsync("hello").ToListAsync();

        // Assert
        Assert.Empty(textRequests);
        Assert.Equal(2, multimodalRequests.Count);
        Assert.All(multimodalRequests, r => Assert.Equal(model, r.Model));
    }

    [Theory]
    [InlineData("qwen3.7-max")]
    [InlineData("qwen3.7-max-2026-05-20")]
    [InlineData("qwen-plus")]
    [InlineData("qwen-max")]
    [InlineData("qwen-doc-turbo")]
    [InlineData("deepseek-v4-pro")]
    [InlineData("glm-5.3")]
    [InlineData("ZHIPU/GLM-5.3")]
    [InlineData("xiaomi/mimo-v2.6-pro-ultraspeed")]
    public async Task ChatClient_TextOnlyModel_CallsTextGenerationEndpointAsync(string model)
    {
        // Arrange
        var dashScopeClient = CreateEndpointRecordingClient(out var textRequests, out var multimodalRequests);
        var client = dashScopeClient.AsChatClient(model);

        // Act
        await client.GetResponseAsync("hello");
        await client.GetStreamingResponseAsync("hello").ToListAsync();

        // Assert
        Assert.Equal(2, textRequests.Count);
        Assert.Empty(multimodalRequests);
        Assert.All(textRequests, r => Assert.Equal(model, r.Model));
    }

    [Theory]
    [InlineData("qwen-max", "qwen3.7-plus", true)]
    [InlineData("qwen3.7-plus", "qwen-max", false)]
    public async Task ChatClient_ModelIdInOptions_RoutesByTheModelOfTheRequestAsync(
        string clientModel,
        string requestModel,
        bool multimodal)
    {
        // Arrange
        var dashScopeClient = CreateEndpointRecordingClient(out var textRequests, out var multimodalRequests);
        var client = dashScopeClient.AsChatClient(clientModel);

        // Act
        await client.GetResponseAsync("hello", new ChatOptions { ModelId = requestModel });

        // Assert
        Assert.Equal(multimodal ? 0 : 1, textRequests.Count);
        Assert.Equal(multimodal ? 1 : 0, multimodalRequests.Count);
    }

    [Theory]
    [InlineData("qwen-max", true, null, true)]
    [InlineData("qwen3.7-plus", false, null, false)]
    [InlineData("qwen-max", null, "true", true)]
    [InlineData("qwen3.7-plus", null, "false", false)]
    [InlineData("qwen-max", true, "false", false)]
    public async Task ChatClient_UseVl_OverridesMultimodalWhitelistAsync(
        string model,
        bool? useVl,
        string? useVlProperty,
        bool multimodal)
    {
        // Arrange
        var dashScopeClient = CreateEndpointRecordingClient(out var textRequests, out var multimodalRequests);
        // null leaves the endpoint to the whitelist, the argument is left out then
        var client = useVl.HasValue
            ? dashScopeClient.AsChatClient(model, useVl.Value)
            : dashScopeClient.AsChatClient(model);
        var options = new ChatOptions();
        if (useVlProperty != null)
        {
            options.AdditionalProperties = new AdditionalPropertiesDictionary { ["useVl"] = useVlProperty };
        }

        // Act
        await client.GetResponseAsync("hello", options);

        // Assert
        Assert.Equal(multimodal ? 0 : 1, textRequests.Count);
        Assert.Equal(multimodal ? 1 : 0, multimodalRequests.Count);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ChatClient_NullOptions_DecidesTheEndpointByTheModelAsync(bool useConstructor)
    {
        // Arrange
        var dashScopeClient = CreateEndpointRecordingClient(out var textRequests, out var multimodalRequests);
        // the null literal goes to the options overload, it is no longer ambiguous with useVl
        IChatClient CreateClient(string model)
            => useConstructor
                ? new DashScopeChatClient(dashScopeClient, model)
                : dashScopeClient.AsChatClient(model);

        // Act
        await CreateClient("qwen3.8-max").GetResponseAsync("hello");
        await CreateClient("qwen3.7-max").GetResponseAsync("hello");

        // Assert
        // the defaults: the endpoint follows the whitelist and no model is looked up
        Assert.Single(textRequests);
        Assert.Single(multimodalRequests);
        _ = dashScopeClient.DidNotReceive().ListModelsAsync(Arg.Any<ModelListQuery?>(), Arg.Any<CancellationToken>());
    }

    [Theory]
    [InlineData("qwen3.7-max")]
    [InlineData("deepseek-v4-pro")]
    [InlineData("glm-5.3")]
    [InlineData("tongyi-xiaomi-analysis-pro")]
    [InlineData("qwen3.7-plus")]
    [InlineData("kimi-k2.6")]
    public async Task ChatClient_WhitelistedModel_MapsMaxOutputTokensToMaxCompletionTokensAsync(string model)
    {
        // Arrange
        var dashScopeClient = CreateEndpointRecordingClient(out var textRequests, out var multimodalRequests);
        var client = dashScopeClient.AsChatClient(model);
        var options = new ChatOptions { MaxOutputTokens = 1500 };

        // Act
        await client.GetResponseAsync("hello", options);
        await client.GetStreamingResponseAsync("hello", options).ToListAsync();

        // Assert
        var limits = textRequests.Select(r => (IMaxTokenParameter?)r.Parameters)
            .Concat(multimodalRequests.Select(r => (IMaxTokenParameter?)r.Parameters)).ToList();
        Assert.Equal(2, limits.Count);
        Assert.All(limits, limit =>
        {
            Assert.Equal(1500, limit!.MaxCompletionTokens);
            Assert.Null(limit.MaxTokens);
        });
    }

    [Theory]
    [InlineData("qwen-max")]
    [InlineData("qwen-plus")]
    [InlineData("qwen3-vl-plus")]
    [InlineData("gui-plus")]
    [InlineData("ZHIPU/GLM-5.3")]
    [InlineData("xiaomi/mimo-v2.6-pro-ultraspeed")]
    public async Task ChatClient_ModelOutsideWhitelist_MapsMaxOutputTokensToMaxTokensAsync(string model)
    {
        // Arrange
        var dashScopeClient = CreateEndpointRecordingClient(out var textRequests, out var multimodalRequests);
        var client = dashScopeClient.AsChatClient(model);
        var options = new ChatOptions { MaxOutputTokens = 1500 };

        // Act
        await client.GetResponseAsync("hello", options);
        await client.GetStreamingResponseAsync("hello", options).ToListAsync();

        // Assert
        var limits = textRequests.Select(r => (IMaxTokenParameter?)r.Parameters)
            .Concat(multimodalRequests.Select(r => (IMaxTokenParameter?)r.Parameters)).ToList();
        Assert.Equal(2, limits.Count);
        Assert.All(limits, limit =>
        {
            Assert.Equal(1500, limit!.MaxTokens);
            Assert.Null(limit.MaxCompletionTokens);
        });
    }

    [Theory]
    [InlineData("qwen-max", "true")]
    [InlineData("qwen3.7-plus", "false")]
    public async Task ChatClient_UseMaxCompletionTokens_OverridesWhitelistAsync(string model, string useMaxCompletionTokens)
    {
        // Arrange
        var dashScopeClient = CreateEndpointRecordingClient(out var textRequests, out var multimodalRequests);
        var client = dashScopeClient.AsChatClient(model);
        var options = new ChatOptions
        {
            MaxOutputTokens = 1500,
            AdditionalProperties = new AdditionalPropertiesDictionary { ["useMaxCompletionTokens"] = useMaxCompletionTokens }
        };

        // Act
        await client.GetResponseAsync("hello", options);

        // Assert
        var limit = Assert.Single(
            textRequests.Select(r => (IMaxTokenParameter?)r.Parameters)
                .Concat(multimodalRequests.Select(r => (IMaxTokenParameter?)r.Parameters)));
        Assert.Equal(useMaxCompletionTokens == "true" ? 1500 : null, limit!.MaxCompletionTokens);
        Assert.Equal(useMaxCompletionTokens == "true" ? null : 1500, limit.MaxTokens);
    }

    [Theory]
    [InlineData("qwen3.7-plus", new[] { "TG" }, "aliyun-bailian", false)]
    [InlineData("qwen9-plus", new[] { "TG", "VU" }, "aliyun-bailian", true)]
    [InlineData("qwen9-omni", new[] { "Multimodal-Omni" }, "aliyun-bailian", true)]
    [InlineData("xiaomi/mimo-v2.6-pro-ultraspeed", new[] { "TG", "VU" }, "xiaomi", false)]
    public async Task ChatClient_AutoSyncModelList_LookedUpModel_OverridesWhitelistAsync(
        string model,
        string[] capabilities,
        string inferenceProvider,
        bool multimodal)
    {
        // Arrange
        var dashScopeClient = CreateEndpointRecordingClient(out var textRequests, out var multimodalRequests);
        // qwen3.7-plus is a multimodal model for the whitelist and a text model for the list, the
        // other models are unknown to the whitelist
        ConfigureModelList(dashScopeClient, CreateModel(model, capabilities, inferenceProvider));
        var client = CreateAutoSyncClient(dashScopeClient, model);

        // Act
        await client.GetResponseAsync("hello");
        await client.GetStreamingResponseAsync("hello").ToListAsync();

        // Assert
        Assert.Equal(multimodal ? 0 : 2, textRequests.Count);
        Assert.Equal(multimodal ? 2 : 0, multimodalRequests.Count);
        // the model is looked up once, then it comes from the cache
        _ = dashScopeClient.Received(1).ListModelsAsync(
            Arg.Is<ModelListQuery?>(q => q!.Model == model), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ChatClient_AutoSyncModelList_ModelNotListed_FallsBackToWhitelistAsync()
    {
        // Arrange
        var dashScopeClient = CreateEndpointRecordingClient(out var textRequests, out var multimodalRequests);
        // qwen3.7-max is a text model for the whitelist, the page holds another (multimodal) model only
        ConfigureModelList(dashScopeClient, CreateModel("qwen4-max", ["TG", "VU"]));
        var client = CreateAutoSyncClient(dashScopeClient, "qwen3.7-max");

        // Act
        await client.GetResponseAsync("hello");

        // Assert
        Assert.Single(textRequests);
        Assert.Empty(multimodalRequests);
    }

    [Fact]
    public async Task ChatClient_AutoSyncModelList_LookupFails_FallsBackToWhitelistAsync()
    {
        // Arrange
        var dashScopeClient = CreateEndpointRecordingClient(out var textRequests, out var multimodalRequests);
        dashScopeClient.Configure()
            .ListModelsAsync(Arg.Any<ModelListQuery?>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromException<DashScopeModelList>(new HttpRequestException("the model list is down")));
        var client = CreateAutoSyncClient(dashScopeClient, "qwen3.7-plus");

        // Act
        var response = await client.GetResponseAsync("hello");

        // Assert
        // the failed lookup costs nothing but the fallback: the request goes on
        Assert.NotNull(response);
        Assert.Empty(textRequests);
        Assert.Single(multimodalRequests);
    }

    [Fact]
    public async Task ChatClient_AutoSyncModelList_Off_NeverLooksUpAsync()
    {
        // Arrange
        var dashScopeClient = CreateEndpointRecordingClient(out _, out _);
        var client = dashScopeClient.AsChatClient("qwen3.7-plus", new DashScopeChatClientOptions());

        // Act
        await client.GetResponseAsync("hello");

        // Assert
        _ = dashScopeClient.DidNotReceive().ListModelsAsync(Arg.Any<ModelListQuery?>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ChatClient_AutoSyncModelList_EachModelIsLookedUpOnceAsync()
    {
        // Arrange
        var dashScopeClient = CreateEndpointRecordingClient(out var textRequests, out var multimodalRequests);
        // the service answers every lookup with the same page, the model of the request is picked out of it
        ConfigureModelList(
            dashScopeClient,
            CreateModel("qwen3.7-plus", ["TG"]),
            CreateModel("qwen9-omni", ["Multimodal-Omni"]));
        var client = CreateAutoSyncClient(dashScopeClient, "qwen3.7-plus");

        // Act
        await client.GetResponseAsync("hello");
        await client.GetResponseAsync("hello");
        await client.GetResponseAsync("hello", new ChatOptions { ModelId = "qwen9-omni" });
        await client.GetResponseAsync("hello", new ChatOptions { ModelId = "qwen9-omni" });

        // Assert
        Assert.Equal(2, textRequests.Count);
        Assert.Equal(2, multimodalRequests.Count);
        _ = dashScopeClient.Received(1).ListModelsAsync(
            Arg.Is<ModelListQuery?>(q => q!.Model == "qwen3.7-plus"), Arg.Any<CancellationToken>());
        _ = dashScopeClient.Received(1).ListModelsAsync(
            Arg.Is<ModelListQuery?>(q => q!.Model == "qwen9-omni"), Arg.Any<CancellationToken>());
        _ = dashScopeClient.Received(2).ListModelsAsync(Arg.Any<ModelListQuery?>(), Arg.Any<CancellationToken>());
    }

    [Theory]
    [InlineData(true, null, true)]
    [InlineData(false, null, false)]
    [InlineData(null, "true", true)]
    [InlineData(null, "false", false)]
    [InlineData(true, "false", false)]
    [InlineData(false, "true", true)]
    public async Task ChatClient_AutoSyncModelList_UseVl_OverridesTheModelListAsync(
        bool? useVl,
        string? useVlProperty,
        bool multimodal)
    {
        // Arrange
        var dashScopeClient = CreateEndpointRecordingClient(out var textRequests, out var multimodalRequests);
        // the list says qwen3.7-plus is a text model, the whitelist and the overrides decide otherwise here
        ConfigureModelList(dashScopeClient, CreateModel("qwen3.7-plus", ["TG"]));
        var client = CreateAutoSyncClient(
            dashScopeClient,
            "qwen3.7-plus",
            new DashScopeChatClientOptions(useVl, AutoSyncModelList: true));
        var options = new ChatOptions();
        if (useVlProperty != null)
        {
            options.AdditionalProperties = new AdditionalPropertiesDictionary { ["useVl"] = useVlProperty };
        }

        // Act
        await client.GetResponseAsync("hello", options);

        // Assert
        Assert.Equal(multimodal ? 0 : 1, textRequests.Count);
        Assert.Equal(multimodal ? 1 : 0, multimodalRequests.Count);
        // the endpoint is already decided, no model is looked up
        _ = dashScopeClient.DidNotReceive().ListModelsAsync(Arg.Any<ModelListQuery?>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ChatClient_AutoSyncModelList_CorrectedModel_CallsMultimodalEndpointWithoutLookupAsync()
    {
        // Arrange
        var dashScopeClient = CreateEndpointRecordingClient(out var textRequests, out var multimodalRequests);
        // the service lists the model with the text capability only, the multimodal endpoint serves it
        ConfigureModelList(dashScopeClient, CreateModel("qwen3.8-2.4t-a95b", ["TG"]));
        var client = CreateAutoSyncClient(dashScopeClient, "qwen3.8-2.4t-a95b");

        // Act
        await client.GetResponseAsync("hello");

        // Assert
        Assert.Empty(textRequests);
        Assert.Single(multimodalRequests);
        _ = dashScopeClient.DidNotReceive().ListModelsAsync(Arg.Any<ModelListQuery?>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ChatClient_AutoSyncModelList_LookupsAreSharedByTheChatClientsOfTheProcessAsync()
    {
        // Arrange
        var first = CreateEndpointRecordingClient(out _, out var firstMultimodalRequests);
        ConfigureModelList(first, CreateModel("process-shared-model", ["Multimodal-Omni"]));
        // the second client knows nothing about the model, a lookup of its own would find nothing
        var second = CreateEndpointRecordingClient(out _, out var secondMultimodalRequests);

        // Act
        await first.AsChatClient("process-shared-model", new DashScopeChatClientOptions(AutoSyncModelList: true))
            .GetResponseAsync("hello");
        await second.AsChatClient("process-shared-model", new DashScopeChatClientOptions(AutoSyncModelList: true))
            .GetResponseAsync("hello");

        // Assert
        // both requests take the endpoint of the first lookup
        Assert.Single(firstMultimodalRequests);
        Assert.Single(secondMultimodalRequests);
        _ = first.Received(1).ListModelsAsync(Arg.Any<ModelListQuery?>(), Arg.Any<CancellationToken>());
        _ = second.DidNotReceive().ListModelsAsync(Arg.Any<ModelListQuery?>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Creates a client that answers on both endpoints and records the request each of them receives.
    /// </summary>
    private static IDashScopeClient CreateEndpointRecordingClient(
        out List<ModelRequest<TextGenerationInput, ITextGenerationParameters>> textRequests,
        out List<ModelRequest<MultimodalInput, IMultimodalParameters>> multimodalRequests)
    {
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        var textReply = Snapshots.MicrosoftExtensionsAi.SingleChatClientMessage.ResponseModel;
        var multimodalReply = Snapshots.MicrosoftExtensionsAi.MultimodalMessageWithFullTokenUsage.ResponseModel;
        textRequests = [];
        multimodalRequests = [];
        dashScopeClient.Configure()
            .GetTextCompletionAsync(
                Arg.Do<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(textRequests.Add),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(textReply));
        dashScopeClient.Configure()
            .GetTextCompletionStreamAsync(
                Arg.Do<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(textRequests.Add),
                Arg.Any<CancellationToken>())
            .Returns(AsyncEnumerable.Repeat(textReply, 1));
        dashScopeClient.Configure()
            .GetMultimodalGenerationAsync(
                Arg.Do<ModelRequest<MultimodalInput, IMultimodalParameters>>(multimodalRequests.Add),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(multimodalReply));
        dashScopeClient.Configure()
            .GetMultimodalGenerationStreamAsync(
                Arg.Do<ModelRequest<MultimodalInput, IMultimodalParameters>>(multimodalRequests.Add),
                Arg.Any<CancellationToken>())
            .Returns(AsyncEnumerable.Repeat(multimodalReply, 1));
        return dashScopeClient;
    }

    /// <summary>
    /// Makes the client answer the model list requests with the models.
    /// </summary>
    private static void ConfigureModelList(IDashScopeClient dashScopeClient, params DashScopeModel[] models)
        => dashScopeClient.Configure()
            .ListModelsAsync(Arg.Any<ModelListQuery?>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(CreateModelList(models)));

    /// <summary>
    /// A page of the model list holding the models.
    /// </summary>
    private static DashScopeModelList CreateModelList(params DashScopeModel[] models)
        => new("model-list-request", new DashScopeModelPage(models.Length, 1, 20, models.ToList()));

    /// <summary>
    /// A chat client with the model list on and a model cache of its own, so that the lookups of
    /// a test do not leak into the cache of the other tests.
    /// </summary>
    private static DashScopeChatClient CreateAutoSyncClient(
        IDashScopeClient dashScopeClient,
        string model,
        DashScopeChatClientOptions? options = null)
        => new(
            dashScopeClient,
            model,
            options ?? new DashScopeChatClientOptions(AutoSyncModelList: true),
            new DashScopeModelCache(new FakeTimeProvider()));

    /// <summary>
    /// A model of the model list, only the members the endpoint picking reads are filled in.
    /// </summary>
    private static DashScopeModel CreateModel(
        string model,
        string[] capabilities,
        string inferenceProvider = "aliyun-bailian")
        => new(
            model,
            model,
            string.Empty,
            "qwen",
            capabilities.ToList(),
            inferenceProvider,
            [],
            [],
            null,
            new DashScopeModelInferenceMetadata(["Text"], ["Text"], null),
            new DashScopeModelInfo(null, null, null, null, null, null),
            null,
            null);
}
