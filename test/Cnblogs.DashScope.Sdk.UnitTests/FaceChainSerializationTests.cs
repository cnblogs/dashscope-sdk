using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class FaceChainSerializationTests
{
    [Fact]
    public async Task FaceChain_DetectFaces_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.FaceChain.DetectFacesNoSse;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.DetectFacesAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task FaceChain_CreateFineTuneJob_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.FaceChain.CreateFineTuneJobNoSse;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var job = await client.CreateFineTuneJobAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, job);
    }

    [Fact]
    public async Task FaceChain_GetFineTuneJob_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.FaceChain.GetFineTuneJobNoSse;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var job = await client.GetFineTuneJobAsync(testCase.ResponseModel.Output.JobId);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(r
                => r.Method == testCase.GetRequestMethod(sse)
                   && ("/api/v1" + r.RequestUri!.PathAndQuery) == testCase.GetRequestPathAndQuery(sse)),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, job);
    }
}
