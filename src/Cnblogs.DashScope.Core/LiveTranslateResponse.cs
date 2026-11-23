namespace Cnblogs.DashScope.Core;

/// <summary>
/// A model response of a live translation session, carried by the response.created /
/// response.done events. The output items of response.done carry the translated text without
/// the raw audio.
/// </summary>
public class LiveTranslateResponse
{
    /// <summary>
    /// The id of the response.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Always realtime.response. Server echo.
    /// </summary>
    public string? Object { get; set; }

    /// <summary>
    /// The id of the conversation.
    /// </summary>
    public string? ConversationId { get; set; }

    /// <summary>
    /// The status of the response: in_progress, completed, incomplete or failed.
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// The modalities of the response.
    /// </summary>
    public List<string>? Modalities { get; set; }

    /// <summary>
    /// The voice of the synthesized audio.
    /// </summary>
    public string? Voice { get; set; }

    /// <summary>
    /// The format of the output audio.
    /// </summary>
    public string? OutputAudioFormat { get; set; }

    /// <summary>
    /// The output items of the response. Empty in response.created, complete in
    /// response.done.
    /// </summary>
    public List<LiveTranslateItem>? Output { get; set; }

    /// <summary>
    /// The token usage of the response.
    /// </summary>
    public LiveTranslateUsage? Usage { get; set; }
}
