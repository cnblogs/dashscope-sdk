namespace Cnblogs.DashScope.Core;

/// <summary>
/// The error detail of an omni realtime session. Carried by the error events, by a failed input
/// audio transcription (conversation.item.input_audio_transcription.failed), by a failed MCP
/// tool discovery (the mcp_list_tools item) and by a failed MCP tool call (the mcp_call item).
/// Which members are present depends on the source, e.g. the error events carry the type and the
/// message, a rejected MCP call the type tool_execution_error.
/// </summary>
public class OmniRealtimeError
{
    /// <summary>
    /// The type of the error, e.g. invalid_request_error or tool_execution_error.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// The code of the error, e.g. invalid_value.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// The message of the error.
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// The parameter related to the error, e.g. session.modalities.
    /// </summary>
    public string? Param { get; set; }
}
