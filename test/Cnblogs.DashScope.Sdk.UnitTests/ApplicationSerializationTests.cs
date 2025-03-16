using Cnblogs.DashScope.Sdk.UnitTests.Utils;
using FluentAssertions;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class ApplicationSerializationTests
{
    [Fact]
    public async Task SingleCompletion_TextNoSse_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Application.SinglePromptNoSse;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.GetApplicationResponseAsync("anyId", testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        response.Should().BeEquivalentTo(testCase.ResponseModel);
    }

    [Fact]
    public async Task SingleCompletion_ThoughtNoSse_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Application.SinglePromptWithThoughtsNoSse;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.GetApplicationResponseAsync("anyId", testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        response.Should().BeEquivalentTo(testCase.ResponseModel);
    }

    [Fact]
    public async Task SingleCompletion_TextSse_SuccessAsync()
    {
        // Arrange
        const bool sse = true;
        var testCase = Snapshots.Application.SinglePromptSse;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var outputs = await client.GetApplicationResponseStreamAsync("anyId", testCase.RequestModel).ToListAsync();
        var text = string.Join(string.Empty, outputs.Select(o => o.Output.Text));

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        outputs.SkipLast(1).Should().AllSatisfy(x => x.Output.FinishReason.Should().Be("null"));
        outputs.Last().Should().BeEquivalentTo(
            testCase.ResponseModel,
            o => o.Excluding(y => y.Output.Text).Excluding(x => x.Output.Thoughts));
        text.Should().Be(testCase.ResponseModel.Output.Text);
    }
}
