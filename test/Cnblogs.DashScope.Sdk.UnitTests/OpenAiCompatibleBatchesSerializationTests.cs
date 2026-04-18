using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class OpenAiCompatibleBatchesSerializationTests
{
    [Fact]
    public async Task CreateBatch_Created_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.OpenAiCompatibleBatches.CreateBatchNoSse;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var batch = await client.OpenAiCompatibleCreateBatchAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(r
                => IsMethodAndUrlEqual(r, testCase, sse)
                   && Checkers.IsJsonEquivalent(r.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, batch);
    }

    [Theory]
    [MemberData(nameof(GetBatchSnapshots))]
    public async Task GetBatch_DifferentStatus_SuccessAsync(RequestSnapshot<DashScopeBatch> testCase)
    {
        // Arrange
        const bool sse = false;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var batch = await client.OpenAiCompatibleGetBatchAsync(testCase.ResponseModel.Id);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(r => IsMethodAndUrlEqual(r, testCase, sse)),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, batch);
    }

    [Fact]
    public async Task ListBatches_NoResult_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.OpenAiCompatibleBatches.EmptyBatchListNoSse;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var batch = await client.OpenAiCompatibleListBatchesAsync(limit: 2, inputFileIds: "hello");

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(r => IsMethodAndUrlEqual(r, testCase, sse)),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, batch);
    }

    [Fact]
    public async Task ListBatches_SearchDsName_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.OpenAiCompatibleBatches.SearchByDsNameBatchListNoSse;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var batch = await client.OpenAiCompatibleListBatchesAsync(limit: 2, dsName: "任务");

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(r => IsMethodAndUrlEqual(r, testCase, sse)),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, batch);
    }

    [Fact]
    public async Task ListBatches_SearchDsNameSecondPage_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.OpenAiCompatibleBatches.SearchByDsNameSecondPageNoSse;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var batch = await client.OpenAiCompatibleListBatchesAsync(
            limit: 1,
            after: "batch_2ecfc43e-439e-4443-bfe8-9a36b89456d0",
            dsName: "任务");

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(r => IsMethodAndUrlEqual(r, testCase, sse)),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, batch);
    }

    [Fact]
    public async Task ListBatches_FilterByStatus_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.OpenAiCompatibleBatches.FilterBatchListByStatusNoSse;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var batch = await client.OpenAiCompatibleListBatchesAsync(
            limit: 2,
            status: "completed,cancelled",
            createAfter: "20260417180000");

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(r => IsMethodAndUrlEqual(r, testCase, sse)),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, batch);
    }

    public static TheoryData<RequestSnapshot<DashScopeBatch>> GetBatchSnapshots
        => new()
        {
            Snapshots.OpenAiCompatibleBatches.GetInvalidBatchNoSse,
            Snapshots.OpenAiCompatibleBatches.GetInProgressBatchNoSse,
            Snapshots.OpenAiCompatibleBatches.GetCancelledBatchNoSse,
            Snapshots.OpenAiCompatibleBatches.GetCompletedBatchNoSse
        };

    private bool IsMethodAndUrlEqual<T>(HttpRequestMessage r, RequestSnapshot<T> testCase, bool sse)
    {
        return r.Method == testCase.GetRequestMethod(sse)
               && r.RequestUri!.PathAndQuery.Equals(
                   testCase.GetRequestPathAndQuery(sse),
                   StringComparison.OrdinalIgnoreCase);
    }
}
