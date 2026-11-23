namespace Cnblogs.DashScope.Core;

/// <summary>
/// A model response of an audio chat session, carried by the response.created / response.done
/// events. Usage is only present when the status is completed.
/// </summary>
public class AudioChatResponse
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
    /// The status of the response: in_progress, completed, cancelled (interrupted by new speech
    /// or response.cancel) or failed.
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// The details of a cancelled or failed response.
    /// </summary>
    public AudioChatStatusDetails? StatusDetails { get; set; }

    /// <summary>
    /// The output modalities of the response.
    /// </summary>
    public List<string>? Modalities { get; set; }

    /// <summary>
    /// The voice of the synthesized audio.
    /// </summary>
    public string? Voice { get; set; }

    /// <summary>
    /// The output items of the response: message items and / or function_call items. Empty in
    /// response.created, complete in response.done.
    /// </summary>
    public List<AudioChatItem>? Output { get; set; }

    /// <summary>
    /// The token usage of the response.
    /// </summary>
    public AudioChatUsage? Usage { get; set; }
}
