using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.AI.Internals;

/// <summary>
/// One lookup of a model in the model list API.
/// </summary>
/// <param name="Model">The model the service lists under the id, null when it lists none or the
/// lookup failed. The task never fails: the model only improves the endpoint picking, a failed
/// lookup must not fail the chat requests.</param>
/// <param name="RetryAfter">When a lookup that ended without a model is repeated. It is
/// <c>default</c> while the lookup is in progress and after it found the model, which is kept for
/// the lifetime of the process.</param>
internal sealed record DashScopeModelLookup(Task<DashScopeModel?> Model, DateTimeOffset RetryAfter = default);
