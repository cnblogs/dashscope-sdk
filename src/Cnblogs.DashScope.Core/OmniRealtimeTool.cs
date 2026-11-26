namespace Cnblogs.DashScope.Core;

/// <summary>
/// A tool of an omni realtime session, an entry of the tools of <see cref="OmniRealtimeSession"/>.
/// A function tool (Type function, <see cref="Function"/> set) is executed by the client, a MCP
/// tool (Type mcp, <see cref="ServerLabel"/> and <see cref="ServerUrl"/> set) is a remote MCP
/// server whose tools are discovered and executed by the server. Both kinds can be configured in
/// one session, and neither can be combined with the web search
/// (<see cref="OmniRealtimeSession.EnableSearch"/>).
/// </summary>
/// <param name="Type">The type of the tool: function or mcp.</param>
/// <param name="Function">The function definition. Function tools.</param>
/// <param name="ServerLabel">The label identifying the MCP server within the session, 1~64
/// letters, digits, underscores and hyphens. MCP tools.</param>
/// <param name="ServerUrl">The public https (port 443) url of the MCP server, streamable HTTP.
/// MCP tools. Not echoed by the server.</param>
/// <param name="Authorization">The value of the Authorization header sent to the MCP server.
/// MCP tools. Not echoed by the server.</param>
/// <param name="Headers">The additional headers sent to the MCP server, at most 16. MCP tools.
/// Not echoed by the server.</param>
/// <param name="AllowedTools">The whitelist applied to the discovered tools, all when unset, none
/// when empty. MCP tools.</param>
/// <param name="RequireApproval">Whether the client has to approve each call: always (default) or
/// never. MCP tools.</param>
public record OmniRealtimeTool(
    string? Type = null,
    OmniRealtimeFunction? Function = null,
    string? ServerLabel = null,
    string? ServerUrl = null,
    string? Authorization = null,
    Dictionary<string, string>? Headers = null,
    List<string>? AllowedTools = null,
    string? RequireApproval = null);
