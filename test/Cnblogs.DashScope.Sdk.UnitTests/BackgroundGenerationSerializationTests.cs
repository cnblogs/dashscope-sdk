using Cnblogs.DashScope.Tests.Shared.Utils;
using FluentAssertions;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class BackgroundGenerationSerializationTests
{
    [Fact]
    public async Task BackgroundGeneration_CreateTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.BackgroundGeneration.CreateTaskNoSse;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateBackgroundGenerationTaskAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        response.Should().BeEquivalentTo(testCase.ResponseModel);
    }
}
