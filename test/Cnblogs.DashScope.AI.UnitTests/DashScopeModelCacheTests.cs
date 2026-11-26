using Cnblogs.DashScope.AI.Internals;
using Cnblogs.DashScope.Core;
using NSubstitute;
using NSubstitute.Extensions;

namespace Cnblogs.DashScope.AI.UnitTests;

public class DashScopeModelCacheTests
{
    [Fact]
    public async Task GetModelAsync_UncachedModel_LooksItUpOnceAndReturnsItAsync()
    {
        // Arrange
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        var model = CreateModel("qwen9-plus", "TG", "VU");
        ConfigureModelList(dashScopeClient, model);
        var cache = CreateCache();

        // Act
        var first = await cache.GetModelAsync(dashScopeClient, "qwen9-plus");
        var second = await cache.GetModelAsync(dashScopeClient, "qwen9-plus");

        // Assert
        Assert.Same(model, first);
        Assert.Same(model, second);
        _ = dashScopeClient.Received(1).ListModelsAsync(
            Arg.Is<ModelListQuery?>(q => q!.Model == "qwen9-plus" && q.PageNo == null && q.PageSize == null),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetModelAsync_ModelNotOnThePage_ReturnsNullAsync()
    {
        // Arrange
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        // the service answers an empty model filter with the whole list, the first model is not the requested one
        ConfigureModelList(dashScopeClient, CreateModel("qwen4-max", "TG", "VU"));
        var cache = CreateCache();

        // Act
        var model = await cache.GetModelAsync(dashScopeClient, "qwen3.7-max");

        // Assert
        Assert.Null(model);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public async Task GetModelAsync_EmptyModelId_SendsNoRequestAsync(string modelId)
    {
        // Arrange
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        var cache = CreateCache();

        // Act
        var model = await cache.GetModelAsync(dashScopeClient, modelId);

        // Assert
        Assert.Null(model);
        _ = dashScopeClient.DidNotReceive().ListModelsAsync(Arg.Any<ModelListQuery?>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetModelAsync_ModelNotListed_LooksItUpAgainAfterTheRetryIntervalAsync()
    {
        // Arrange
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        var model = CreateModel("qwen9-plus", "TG", "VU");
        // the model is listed by the time of the second lookup
        dashScopeClient.Configure()
            .ListModelsAsync(Arg.Any<ModelListQuery?>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(CreateModelList()), Task.FromResult(CreateModelList(model)));
        var clock = new FakeTimeProvider();
        var cache = CreateCache(clock);

        // Act
        var first = await cache.GetModelAsync(dashScopeClient, "qwen9-plus");
        var duringRetryInterval = await cache.GetModelAsync(dashScopeClient, "qwen9-plus");
        clock.Advance(TimeSpan.FromMinutes(2));
        var afterRetryInterval = await cache.GetModelAsync(dashScopeClient, "qwen9-plus");

        // Assert
        Assert.Null(first);
        Assert.Null(duringRetryInterval);
        Assert.Same(model, afterRetryInterval);
        _ = dashScopeClient.Received(2).ListModelsAsync(Arg.Any<ModelListQuery?>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetModelAsync_LookupFails_LooksItUpAgainAfterTheRetryIntervalAsync()
    {
        // Arrange
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        var model = CreateModel("qwen9-plus", "TG", "VU");
        dashScopeClient.Configure()
            .ListModelsAsync(Arg.Any<ModelListQuery?>(), Arg.Any<CancellationToken>())
            .Returns(
                Task.FromException<DashScopeModelList>(new HttpRequestException("the model list is down")),
                Task.FromResult(CreateModelList(model)));
        var clock = new FakeTimeProvider();
        var cache = CreateCache(clock);

        // Act
        var first = await cache.GetModelAsync(dashScopeClient, "qwen9-plus");
        var duringRetryInterval = await cache.GetModelAsync(dashScopeClient, "qwen9-plus");
        clock.Advance(TimeSpan.FromMinutes(2));
        var afterRetryInterval = await cache.GetModelAsync(dashScopeClient, "qwen9-plus");

        // Assert
        Assert.Null(first);
        Assert.Null(duringRetryInterval);
        Assert.Same(model, afterRetryInterval);
        _ = dashScopeClient.Received(2).ListModelsAsync(Arg.Any<ModelListQuery?>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetModelAsync_LookupFailsForAModel_DoesNotDelayTheLookupsOfTheOtherModelsAsync()
    {
        // Arrange
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        var model = CreateModel("qwen9-plus", "TG", "VU");
        dashScopeClient.Configure()
            .ListModelsAsync(Arg.Any<ModelListQuery?>(), Arg.Any<CancellationToken>())
            .Returns(ci => ci.Arg<ModelListQuery?>()!.Model == "broken-model"
                ? Task.FromException<DashScopeModelList>(new HttpRequestException("the model list is down"))
                : Task.FromResult(CreateModelList(model)));
        var cache = CreateCache();

        // Act
        var broken = await cache.GetModelAsync(dashScopeClient, "broken-model");
        var other = await cache.GetModelAsync(dashScopeClient, "qwen9-plus");

        // Assert
        Assert.Null(broken);
        Assert.Same(model, other);
    }

    [Fact]
    public async Task GetModelAsync_ConcurrentRequests_LookTheModelUpOnceAsync()
    {
        // Arrange
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        var model = CreateModel("qwen9-plus", "TG", "VU");
        var lookup = new TaskCompletionSource<DashScopeModelList>(TaskCreationOptions.RunContinuationsAsynchronously);
        dashScopeClient.Configure()
            .ListModelsAsync(Arg.Any<ModelListQuery?>(), Arg.Any<CancellationToken>())
            .Returns(lookup.Task);
        var cache = CreateCache();

        // Act
        var requests = Enumerable.Range(0, 5).Select(_ => cache.GetModelAsync(dashScopeClient, "qwen9-plus")).ToList();
        lookup.SetResult(CreateModelList(model));
        var models = await Task.WhenAll(requests);

        // Assert
        Assert.All(models, m => Assert.Same(model, m));
        _ = dashScopeClient.Received(1).ListModelsAsync(Arg.Any<ModelListQuery?>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetModelAsync_CorrectedModel_ReturnsItWithoutLookupAsync()
    {
        // Arrange
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        // what the service answers: the model has the text capability only
        ConfigureModelList(dashScopeClient, CreateModel("qwen3.8-2.4t-a95b", "TG"));
        var cache = CreateCache();

        // Act
        var model = await cache.GetModelAsync(dashScopeClient, "qwen3.8-2.4t-a95b");

        // Assert
        Assert.Equal("aliyun-bailian", model!.InferenceProvider);
        Assert.Contains("VU", model.Capabilities);
        _ = dashScopeClient.DidNotReceive().ListModelsAsync(Arg.Any<ModelListQuery?>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetModelAsync_LookupHangs_GivesUpAfterTheTimeoutAsync()
    {
        // Arrange
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        var clock = new FakeTimeProvider();
        CancellationToken token = default;
        // the lookup hangs until its token is cancelled
        dashScopeClient.Configure()
            .ListModelsAsync(Arg.Any<ModelListQuery?>(), Arg.Do<CancellationToken>(t => token = t))
            .Returns(async ci =>
            {
                await Task.Delay(Timeout.InfiniteTimeSpan, ci.Arg<CancellationToken>());
                return CreateModelList();
            });
        var cache = CreateCache(clock);

        // Act
        var lookup = cache.GetModelAsync(dashScopeClient, "qwen9-plus");
        Assert.False(lookup.IsCompleted);
        clock.Advance(TimeSpan.FromSeconds(10));

        // Assert
        Assert.Null(await lookup);
        Assert.True(token.IsCancellationRequested);
    }

    private static DashScopeModelCache CreateCache(FakeTimeProvider? clock = null)
        => new(clock ?? new FakeTimeProvider());

    /// <summary>
    /// Makes the client answer every model list request with the models.
    /// </summary>
    private static void ConfigureModelList(IDashScopeClient dashScopeClient, params DashScopeModel[] models)
        => dashScopeClient.Configure()
            .ListModelsAsync(Arg.Any<ModelListQuery?>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(CreateModelList(models)));

    private static DashScopeModelList CreateModelList(params DashScopeModel[] models)
        => new("model-list-request", new DashScopeModelPage(models.Length, 1, 20, models.ToList()));

    /// <summary>
    /// A model of the model list, only the members the endpoint picking reads are filled in.
    /// </summary>
    private static DashScopeModel CreateModel(string model, params string[] capabilities)
        => new(
            model,
            model,
            string.Empty,
            "qwen",
            capabilities.ToList(),
            "aliyun-bailian",
            [],
            [],
            null,
            new DashScopeModelInferenceMetadata(["Text"], ["Text"], null),
            new DashScopeModelInfo(null, null, null, null, null, null),
            null,
            null);
}
