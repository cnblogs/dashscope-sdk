namespace Cnblogs.DashScope.Core;

/// <summary>
/// The annotations of a tool of a remote MCP server. Hints provided by the MCP server, not to be
/// relied on as a safety judgment.
/// </summary>
/// <param name="Title">The display name of the tool.</param>
/// <param name="ReadOnlyHint">Whether the tool only reads.</param>
/// <param name="DestructiveHint">Whether the tool may make destructive changes.</param>
/// <param name="IdempotentHint">Whether repeated calls have no additional effect.</param>
/// <param name="OpenWorldHint">Whether the tool may interact with external systems.</param>
public record OmniRealtimeMcpToolAnnotations(
    string? Title = null,
    bool? ReadOnlyHint = null,
    bool? DestructiveHint = null,
    bool? IdempotentHint = null,
    bool? OpenWorldHint = null);
