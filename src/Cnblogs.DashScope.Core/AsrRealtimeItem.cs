namespace Cnblogs.DashScope.Core;

/// <summary>
/// A conversation item created by the server when speech is detected (VAD mode) or the audio
/// buffer is committed (manual mode). The transcription result of the item is delivered in the
/// conversation.item.input_audio_transcription.text / completed events.
/// </summary>
public class AsrRealtimeItem
{
    /// <summary>
    /// The id of the item, e.g. item_xxx.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// The object type, always realtime.item.
    /// </summary>
    public string? Object { get; set; }

    /// <summary>
    /// The type of the item, message so far.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// The status of the item, in_progress when created and completed when the transcription
    /// finishes.
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// The role of the item, user so far.
    /// </summary>
    public string? Role { get; set; }

    /// <summary>
    /// The contents of the item.
    /// </summary>
    public List<AsrRealtimeItemContent>? Content { get; set; }
}
