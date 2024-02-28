using System.Text;
using FluentAssertions;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class MultimodalGenerationSerializationTests
{
    [Theory]
    [MemberData(nameof(NoSseData))]
    public async Task MultimodalGeneration_NoSse_SuccessAsync(
        RequestSnapshot<ModelRequest<MultimodalInput, MultimodalParameters>,
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
        response.Should().BeEquivalentTo(testCase.ResponseModel);
    }

    [Theory]
    [MemberData(nameof(SseData))]
    public async Task MultimodalGeneration_Sse_SuccessAsync(
        RequestSnapshot<ModelRequest<MultimodalInput, MultimodalParameters>,
            ModelResponse<MultimodalOutput, MultimodalTokenUsage>> testCase)
    {
        // Arrange
        const bool sse = true;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var message = new StringBuilder();
        var outputs = await client.GetMultimodalGenerationStreamAsync(testCase.RequestModel).ToListAsync();
        outputs.ForEach(x => message.Append(x.Output.Choices[0].Message.Content[0].Text));

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        outputs.SkipLast(1).Should().AllSatisfy(x => x.Output.Choices[0].FinishReason.Should().Be("null"));
        outputs.Last().Should().BeEquivalentTo(
            testCase.ResponseModel,
            o => o.Excluding(y => y.Output.Choices[0].Message.Content[0].Text));
        message.ToString().Should().Be(testCase.ResponseModel.Output.Choices[0].Message.Content[0].Text);
    }

    public static TheoryData<RequestSnapshot<ModelRequest<MultimodalInput, MultimodalParameters>,
        ModelResponse<MultimodalOutput, MultimodalTokenUsage>>> NoSseData
        => new() { Snapshots.MultimodalGeneration.VlNoSse };

    public static TheoryData<RequestSnapshot<ModelRequest<MultimodalInput, MultimodalParameters>,
        ModelResponse<MultimodalOutput, MultimodalTokenUsage>>> SseData
        => new() { Snapshots.MultimodalGeneration.VlSse };
}
