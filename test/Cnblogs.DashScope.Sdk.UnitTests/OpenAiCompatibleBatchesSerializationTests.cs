using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class OpenAiCompatibleBatchesSerializationTests
{
    [Fact]
    public async Task OpenAiBatch_Create_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.OpenAiCompatibleBatches.CreateBatchNoSse;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var batch = await client.OpenAiCompatibleCreateBatchAsync(
            Snapshots.OpenAiCompatibleBatches.CreateBatchNoSse.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(r
                => r.Method == testCase.GetRequestMethod(sse)
                   && r.RequestUri!.PathAndQuery == testCase.GetRequestPathAndQuery(sse)
                   && Checkers.IsJsonEquivalent(r.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, batch);
    }
}
