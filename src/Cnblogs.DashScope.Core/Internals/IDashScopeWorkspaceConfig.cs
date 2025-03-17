namespace Cnblogs.DashScope.Core.Internals;

/// <summary>
/// Workspace configuration.
/// </summary>
internal interface IDashScopeWorkspaceConfig
{
    /// <summary>
    /// Unique id of workspace to use.
    /// </summary>
    public string? WorkspaceId { get; }
}
