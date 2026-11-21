namespace Cnblogs.DashScope.Core;

/// <summary>
/// A synthesis response of the TTS realtime API, returned in response.created and response.done events.
/// </summary>
/// <param name="Id">The id of the response, e.g. resp_xxx.</param>
/// <param name="Object">The object type, always realtime.response.</param>
/// <param name="ConversationId">The conversation id, currently empty.</param>
/// <param name="Status">The status of the response: in_progress (response.created) or completed / failed / incomplete (response.done).</param>
/// <param name="Modalities">The output modalities, e.g. ["text", "audio"]. Present in response.done events.</param>
/// <param name="Voice">The voice used by the response.</param>
/// <param name="Output">The output items. Empty in response.created events.</param>
/// <param name="Usage">The billing usage. Present in response.done events.</param>
public record TtsRealtimeResponse(
    string? Id = null,
    string? Object = null,
    string? ConversationId = null,
    string? Status = null,
    List<string>? Modalities = null,
    string? Voice = null,
    List<TtsRealtimeItem>? Output = null,
    TtsRealtimeUsage? Usage = null);
