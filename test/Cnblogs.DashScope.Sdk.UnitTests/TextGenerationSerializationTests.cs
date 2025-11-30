using System.Text;
using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class TextGenerationSerializationTests
{
    [Fact]
    public async Task SingleCompletion_TextFormatNoSse_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.TextGeneration.TextFormat.SinglePrompt;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.GetTextCompletionAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task SingleCompletion_TextFormatSse_SuccessAsync()
    {
        // Arrange
        const bool sse = true;
        var testCase = Snapshots.TextGeneration.TextFormat.SinglePromptIncremental;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var message = new StringBuilder();
        var outputs = await client.GetTextCompletionStreamAsync(testCase.RequestModel).ToListAsync();
        outputs.ForEach(x => message.Append(x.Output.Text));

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.All(outputs.SkipLast(1), x => Assert.Equal("null", x.Output.FinishReason));
        Assert.Equal(testCase.ResponseModel.Output.Text, message.ToString());
        var last = outputs.Last();
        last = last with
        {
            Output = new TextGenerationOutput()
            {
                Text = testCase.ResponseModel.Output.Text,
                Choices = last.Output.Choices,
                FinishReason = last.Output.FinishReason,
                SearchInfo = last.Output.SearchInfo
            }
        };
        Assert.Equivalent(testCase.ResponseModel, last);
    }

    [Theory]
    [MemberData(nameof(SingleGenerationMessageFormatData))]
    public async Task SingleCompletion_MessageFormatNoSse_SuccessAsync(
        RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
            ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> testCase)
    {
        // Arrange
        const bool sse = false;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.GetTextCompletionAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Theory]
    [MemberData(nameof(SingleGenerationMessageSseFormatData))]
    public async Task SingleCompletion_MessageFormatSse_SuccessAsync(
        RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
            ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> snapshot)
    {
        // Arrange
        const bool sse = true;
        var testCase = snapshot;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var message = new StringBuilder();
        var reasoning = new StringBuilder();
        var outputs = await client.GetTextCompletionStreamAsync(testCase.RequestModel).ToListAsync();
        outputs.ForEach(x =>
        {
            message.Append(x.Output.Choices![0].Message.Content);
            reasoning.Append(x.Output.Choices![0].Message.ReasoningContent ?? string.Empty);
        });

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.All(outputs.SkipLast(1), x => Assert.Equal("null", x.Output.Choices![0].FinishReason));
        Assert.Equal(testCase.ResponseModel.Output.Choices![0].Message.Content, message.ToString());
        Assert.Equal(
            testCase.ResponseModel.Output.Choices![0].Message.ReasoningContent ?? string.Empty,
            reasoning.ToString());
        var last = outputs.Last();
        last.Output.Choices = new List<TextGenerationChoice>
        {
            new()
            {
                Message = last.Output.Choices![0].Message with
                {
                    Content = testCase.ResponseModel.Output.Choices[0].Message.Content,
                    ReasoningContent = testCase.ResponseModel.Output.Choices[0].Message.ReasoningContent
                }
            }
        };
    }

    [Theory]
    [MemberData(nameof(ConversationMessageFormatNoSseData))]
    public async Task ConversationCompletion_MessageFormatNoSse_SuccessAsync(
        RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
            ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> testCase)
    {
        // Arrange
        const bool sse = false;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.GetTextCompletionAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Theory]
    [MemberData(nameof(ConversationMessageFormatSseData))]
    public async Task ConversationCompletion_MessageFormatSse_SuccessAsync(
        RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
            ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> testCase)
    {
        // Arrange
        const bool sse = true;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var message = new StringBuilder();
        var outputs = await client.GetTextCompletionStreamAsync(testCase.RequestModel).ToListAsync();
        outputs.ForEach(x => message.Append(x.Output.Choices![0].Message.Content));

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.All(outputs.SkipLast(1), x => Assert.Equal("null", x.Output.Choices![0].FinishReason));
        Assert.Equal(testCase.ResponseModel.Output.Choices![0].Message.Content, message.ToString());
        var last = outputs.Last();
        last.Output.Choices![0].Message =
            TextChatMessage.Assistant(testCase.ResponseModel.Output.Choices[0].Message.Content);
        Assert.Equivalent(testCase.ResponseModel, last);
    }

    [Fact]
    public async Task ConversationCompletion_DeepResearchSse_ValidateRequestAsync()
    {
        // Arrange
        const bool sse = true;
        var testCase = Snapshots.TextGeneration.MessageFormat.DeepResearchTypingIncremental;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        await client.GetTextCompletionStreamAsync(testCase.RequestModel).ToListAsync();

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
    }

    [Theory]
    [MemberData(nameof(DeepResearchSseData))]
    public async Task ConversationCompletion_DeepResearchSse_SuccessAsync(
        RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
            ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> testCase)
    {
        // Arrange
        const bool sse = true;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var outputs = await client.GetTextCompletionStreamAsync(testCase.RequestModel).ToListAsync();
        var response = outputs.First();

        // Assert
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    public static readonly TheoryData<RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
        ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>> SingleGenerationMessageFormatData = new(
        Snapshots.TextGeneration.MessageFormat.SingleMessage,
        Snapshots.TextGeneration.MessageFormat.SingleMessageReasoning,
        Snapshots.TextGeneration.MessageFormat.SingleMessageWithTools,
        Snapshots.TextGeneration.MessageFormat.SingleMessageJson,
        Snapshots.TextGeneration.MessageFormat.SingleMessageLogprobs,
        Snapshots.TextGeneration.MessageFormat.SingleMessageTranslation,
        Snapshots.TextGeneration.MessageFormat.SingleMessageRolePlay,
        Snapshots.TextGeneration.MessageFormat.SingleMessageWebSearchNoSse);

    public static readonly TheoryData<RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
        ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>> SingleGenerationMessageSseFormatData = new(
        Snapshots.TextGeneration.MessageFormat.SingleMessageIncremental,
        Snapshots.TextGeneration.MessageFormat.SingleMessageReasoningIncremental,
        Snapshots.TextGeneration.MessageFormat.SingleMessageWebSearchIncremental,
        Snapshots.TextGeneration.MessageFormat.SingleMessageWithToolsIncremental);

    public static readonly TheoryData<RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
        ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>> ConversationMessageFormatSseData = new(
        Snapshots.TextGeneration.MessageFormat.ConversationMessageIncremental,
        Snapshots.TextGeneration.MessageFormat.ConversationMessageWithFilesIncremental,
        Snapshots.TextGeneration.MessageFormat.ConversationMessageWithDocUrlsIncremental);

    public static readonly TheoryData<RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
        ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>> ConversationMessageFormatNoSseData = new(
        Snapshots.TextGeneration.MessageFormat.ConversationPartialMessageNoSse);

    public static readonly TheoryData<RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
        ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>> DeepResearchSseData = new(
        Snapshots.TextGeneration.MessageFormat.DeepResearchTypingIncremental,
        Snapshots.TextGeneration.MessageFormat.DeepResearchWebResearchStreamingQueriesIncremental,
        Snapshots.TextGeneration.MessageFormat.DeepResearchWebResearchStreamingQueriesResearchGoalIncremental,
        Snapshots.TextGeneration.MessageFormat.DeepResearchWebResearchStreamingWebResultsIncremental,
        Snapshots.TextGeneration.MessageFormat.DeepResearchWebResearchStreamingWebResultsLearningMapIncremental,
        Snapshots.TextGeneration.MessageFormat.DeepResearchWebResearchWebResultFinishedIncremental,
        Snapshots.TextGeneration.MessageFormat.DeepResearchAnswerReferenceIncremental,
        Snapshots.TextGeneration.MessageFormat.DeepResearchAnswerFinishedIncremental,
        Snapshots.TextGeneration.MessageFormat.DeepResearchKeepAliveIncremental);
}
