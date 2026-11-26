using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class ModelListSerializationTests
{
    [Theory]
    [MemberData(nameof(NoSseData))]
    public async Task ListModels_NoSse_SuccessAsync(
        RequestSnapshot<DashScopeModelList> testCase,
        ModelListQuery query)
    {
        // Arrange
        const bool sse = false;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var list = await client.ListModelsAsync(query);

        // Assert
        // the request equivalence also tells that only the set filters go to the query
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(r
                => r.Method == testCase.GetRequestMethod(sse)
                   && ("/api/v1" + r.RequestUri!.PathAndQuery) == testCase.GetRequestPathAndQuery(sse)),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, list);
    }

    public static TheoryData<RequestSnapshot<DashScopeModelList>, ModelListQuery> NoSseData
        => new()
        {
            { Snapshots.ModelList.FirstNoSse, new ModelListQuery { PageSize = 1 } },
            { Snapshots.ModelList.PageNoSse, new ModelListQuery { PageNo = 2, PageSize = 2 } },
            {
                Snapshots.ModelList.ByModelNoSse,
                new ModelListQuery { Model = "qwen3.7-max", PageNo = 1, PageSize = 20 }
            },
            {
                Snapshots.ModelList.FilteredNoSse,
                new ModelListQuery
                {
                    Name = "flash",
                    Language = "en-US",
                    PageNo = 1,
                    PageSize = 2,
                    Providers = new List<string> { "deepseek" },
                    InferenceProviders = new List<string> { "aliyun-bailian" },
                    Capabilities = new List<string> { "TG", "Reasoning" },
                    Features = new List<string> { "function-calling" },
                    ContextWindow = 2000000
                }
            },
            {
                Snapshots.ModelList.EmptyNoSse,
                new ModelListQuery
                {
                    PageNo = 1,
                    PageSize = 2,
                    Providers = new List<string> { "deepseek" },
                    ServiceSite = "international",
                    Supports = new List<string> { "deploy" },
                    DeploymentMethods = new List<string> { "ptu" },
                    DeploymentPtuServiceTiers = new List<string> { "default" }
                }
            }
        };

    [Fact]
    public async Task ListModels_NoQuery_SendsNoQueryStringAsync()
    {
        // Arrange
        const bool sse = false;
        // the response is the captured body of another case, only the request matters here
        var (client, handler) = await Sut.GetTestClientAsync(await Snapshots.ModelList.ByModelNoSse.ToResponseMessageAsync(sse));

        // Act
        await client.ListModelsAsync();

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Get
                                           && ("/api/v1" + r.RequestUri!.PathAndQuery) == "/api/v1/models"),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ListModels_InvalidPageNoNoSse_ExceptionAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.ModelList.InvalidPageNoNoSse;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var act = async () => await client.ListModelsAsync(new ModelListQuery { PageNo = 0 });

        // Assert
        // the service answers with status 200 and success false, the client has to turn it into
        // an exception that keeps the error detail
        var ex = await Assert.ThrowsAsync<DashScopeException>(act);
        Assert.Equal(200, ex.Status);
        Assert.Equivalent(testCase.ResponseModel, ex.Error);
    }

    [Fact]
    public async Task ListModels_InvalidPageSizeNoSse_ExceptionAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.ModelList.InvalidPageSizeNoSse;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var act = async () => await client.ListModelsAsync(new ModelListQuery { PageSize = 1 });

        // Assert
        var ex = await Assert.ThrowsAsync<DashScopeException>(act);
        Assert.Equal(400, ex.Status);
        Assert.Equivalent(testCase.ResponseModel, ex.Error);
    }
}
