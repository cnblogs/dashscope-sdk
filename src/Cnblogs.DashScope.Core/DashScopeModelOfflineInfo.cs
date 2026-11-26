namespace Cnblogs.DashScope.Core;

/// <summary>
/// The retirement schedule of a model.
/// </summary>
/// <param name="OfflineTime">When the model is going to be retired, in <c>yyyy-MM-dd HH:mm:ss</c>.</param>
public record DashScopeModelOfflineInfo(string OfflineTime);
