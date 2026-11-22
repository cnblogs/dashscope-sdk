namespace Cnblogs.DashScope.Core;

/// <summary>
/// The recognition configuration of an ASR realtime session.
/// </summary>
public class AsrRealtimeTranscription
{
    /// <summary>
    /// The language of the audio: zh, yue, en, ja, de, ko, ru, fr, pt, ar, it, es, hi, id, th,
    /// tr, uk, vi, cs, da, fil, fi, is, ms, no, pl or sv. Leave it unset to let the model detect
    /// automatically.
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// The model echoed by the server. Server side only.
    /// </summary>
    public string? Model { get; set; }
}
