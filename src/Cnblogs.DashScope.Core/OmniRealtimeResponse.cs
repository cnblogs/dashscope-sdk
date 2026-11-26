namespace Cnblogs.DashScope.Core;

/// <summary>
/// A model response of an omni realtime session, carried by the response.created / response.done
/// events. The output items are empty in response.created and complete in response.done: message
/// items (the reply text in the transcript of the audio content, or in the text content when the
/// modalities are text only), function_call items the client has to execute, and mcp_call items
/// the server executed.
/// </summary>
public class OmniRealtimeResponse
{
    /// <summary>
    /// The id of the response.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Always realtime.response.
    /// </summary>
    public string? Object { get; set; }

    /// <summary>
    /// The id of the conversation the response belongs to.
    /// </summary>
    public string? ConversationId { get; set; }

    /// <summary>
    /// The status of the response: in_progress, completed, cancelled (interrupted by new speech
    /// or by response.cancel), failed or incomplete.
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// The details of a cancelled response.
    /// </summary>
    public OmniRealtimeStatusDetails? StatusDetails { get; set; }

    /// <summary>
    /// The modalities of the response: ["text"] or ["text", "audio"].
    /// </summary>
    public List<string>? Modalities { get; set; }

    /// <summary>
    /// The voice of the synthesized audio.
    /// </summary>
    public string? Voice { get; set; }

    /// <summary>
    /// The format of the output audio, pcm or wav.
    /// </summary>
    public string? OutputAudioFormat { get; set; }

    /// <summary>
    /// The output items of the response. Empty in response.created, complete in response.done.
    /// </summary>
    public List<OmniRealtimeItem>? Output { get; set; }

    /// <summary>
    /// The token usage of the response. Present in response.done of a completed response, absent
    /// when it was cancelled.
    /// </summary>
    public OmniRealtimeUsage? Usage { get; set; }
}
