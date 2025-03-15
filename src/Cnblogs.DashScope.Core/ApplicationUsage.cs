namespace Cnblogs.DashScope.Core;

/// <summary>
/// Total token usages of this application call.
/// </summary>
/// <param name="Models">All models been used and their token usages.</param>
public record ApplicationUsage(List<ApplicationModelUsage> Models);
