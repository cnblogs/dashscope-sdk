namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents a list of DashScope files.
/// </summary>
/// <param name="Object">Always be "list".</param>
/// <param name="HasMore">True if not reached last page.</param>
/// <param name="Data">Items of current page.</param>
public record DashScopeOpenAiCompatibleFileList(string Object, bool HasMore, List<DashScopeFile> Data);
