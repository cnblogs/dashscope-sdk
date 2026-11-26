namespace Cnblogs.DashScope.Core;

/// <summary>
/// One page of the model list, returned by <see cref="IDashScopeClient.ListModelsAsync"/>.
/// </summary>
/// <param name="RequestId">The unique id of this request.</param>
/// <param name="Output">The page: the total number of matching models, the page index and the models.</param>
public record DashScopeModelList(string RequestId, DashScopeModelPage Output);
