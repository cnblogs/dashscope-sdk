using System.Collections.Concurrent;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.AI.Internals;

/// <summary>
/// The models looked up in the model list API, shared by every <see cref="DashScopeChatClient"/> of
/// the process. A model is looked up (<c>GET /api/v1/models?model=...</c>) when it is used the first
/// time and is kept for the lifetime of the process, so the lookup costs one round trip per model.
/// </summary>
internal sealed class DashScopeModelCache
{
    /// <summary>
    /// How long to wait before a model that was not found is looked up again, so that a model the
    /// service does not list, or a failing model list, does not cost a round trip on every chat request.
    /// </summary>
    private static readonly TimeSpan RetryInterval = TimeSpan.FromMinutes(1);

    /// <summary>
    /// The timeout of a lookup: it delays the chat request that triggered it, so a model list that
    /// hangs must not hold the request as long as the timeout of the HTTP client does.
    /// </summary>
    private static readonly TimeSpan LookupTimeout = TimeSpan.FromSeconds(10);

    private readonly TimeProvider _timeProvider;
    private readonly object _lookupLock = new();

    // The models the service reports wrongly, keyed by id. qwen3.8-2.4t-a95b is listed with the
    // text capability only although the multimodal endpoint serves it: the chat client would pick
    // the text endpoint according to the list and get 400 "url error".
    private readonly ConcurrentDictionary<string, DashScopeModelLookup> _lookups = new()
    {
        ["qwen3.8-2.4t-a95b"] = new(Task.FromResult<DashScopeModel?>(CreateModel("qwen3.8-2.4t-a95b", "TG", "VU")))
    };

    internal DashScopeModelCache(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    /// <summary>
    /// The cache of the whole process.
    /// </summary>
    public static DashScopeModelCache Instance { get; } = new(TimeProvider.System);

    /// <summary>
    /// Gets a model from the model list API, looking it up when it is not cached yet.
    /// </summary>
    /// <param name="dashScopeClient">The client that sends the lookup, any client works since the
    /// cache is shared by all of them.</param>
    /// <param name="modelId">The id of the model.</param>
    /// <returns>The model the service lists under <paramref name="modelId"/>, or null when the
    /// service does not list it or the lookup failed.</returns>
    public async Task<DashScopeModel?> GetModelAsync(IDashScopeClient dashScopeClient, string modelId)
    {
        // the service ignores an empty model filter and would answer the whole list
        if (string.IsNullOrWhiteSpace(modelId))
        {
            return null;
        }

        var lookup = GetLookup(dashScopeClient, modelId);
        var model = await lookup.Model;
        if (model == null && lookup.RetryAfter == default)
        {
            // the lookup found nothing: keep that until the retry interval passes, the requests
            // in between go on without paying for the lookup again
            _lookups.TryUpdate(
                modelId,
                lookup with { RetryAfter = _timeProvider.GetUtcNow() + RetryInterval },
                lookup);
        }

        return model;
    }

    /// <summary>
    /// Gets the lookup of a model, starting it when the model was not looked up yet or its last
    /// lookup found nothing and the retry interval passed.
    /// </summary>
    private DashScopeModelLookup GetLookup(IDashScopeClient dashScopeClient, string modelId)
    {
        // a cached lookup costs no lock: the lock only lines up the requests of a model that is
        // not cached yet, all of them wait for the one lookup made by the first of them
        if (_lookups.TryGetValue(modelId, out var lookup) && IsCurrent(lookup))
        {
            return lookup;
        }

        lock (_lookupLock)
        {
            if (_lookups.TryGetValue(modelId, out lookup) && IsCurrent(lookup))
            {
                return lookup;
            }

            lookup = new DashScopeModelLookup(LookUpAsync(dashScopeClient, modelId));
            _lookups[modelId] = lookup;
            return lookup;
        }
    }

    /// <summary>
    /// Whether a lookup is used as it is: it is in progress, it found the model, or its retry
    /// interval has not passed yet.
    /// </summary>
    private bool IsCurrent(DashScopeModelLookup lookup)
        => lookup.RetryAfter == default || lookup.RetryAfter > _timeProvider.GetUtcNow();

    /// <summary>
    /// Looks a model up in the model list API.
    /// </summary>
    /// <returns>The model the service lists under the id, or null when it lists none or the lookup
    /// failed: the model only improves the endpoint picking, a failed lookup must not fail the chat
    /// requests.</returns>
    private async Task<DashScopeModel?> LookUpAsync(IDashScopeClient dashScopeClient, string modelId)
    {
        try
        {
            // the lookup is shared by the requests waiting for it, so it is bound to its own timeout
            // instead of the cancellation token of the request that started it
            using var timeout = new CancellationTokenSource(LookupTimeout, _timeProvider);
            var list = await dashScopeClient.ListModelsAsync(new ModelListQuery { Model = modelId }, timeout.Token);

            // the service matches the id exactly today, do not depend on it: pick the model out of
            // the page instead of taking the first one
            return list.Output.Models.FirstOrDefault(m => m.Model == modelId);
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// A model with only the members the endpoint picking reads, for the models put into the cache
    /// without a lookup.
    /// </summary>
    private static DashScopeModel CreateModel(string modelId, params string[] capabilities)
        => new(
            modelId,
            string.Empty,
            string.Empty,
            string.Empty,
            capabilities.ToList(),
            "aliyun-bailian",
            [],
            [],
            null,
            new DashScopeModelInferenceMetadata([], [], null),
            new DashScopeModelInfo(null, null, null, null, null, null),
            null,
            null);
}
