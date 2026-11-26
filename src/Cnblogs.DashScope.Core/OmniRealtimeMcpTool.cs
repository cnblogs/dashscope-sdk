using System.Text.Json;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// A tool of a remote MCP server, listed in the mcp_list_tools item of a conversation.item.created
/// event once the server discovered the tools of the MCP server configured in the session.
/// </summary>
/// <param name="Name">The name of the tool.</param>
/// <param name="Description">The description of the tool.</param>
/// <param name="InputSchema">The JSON schema of the tool arguments, as provided by the MCP server.</param>
/// <param name="Annotations">The hints of the tool, as provided by the MCP server.</param>
public record OmniRealtimeMcpTool(
    string? Name = null,
    string? Description = null,
    JsonElement? InputSchema = null,
    OmniRealtimeMcpToolAnnotations? Annotations = null);
