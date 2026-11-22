namespace Cnblogs.DashScope.Core;

/// <summary>
/// The error detail of an error event of the ASR realtime API.
/// </summary>
public class AsrRealtimeError
{
    /// <summary>
    /// The type of the error, e.g. invalid_request_error.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// The error code, e.g. invalid_value.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// The error message.
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// The parameter the error relates to, e.g. session.input_audio_transcription.model.
    /// </summary>
    public string? Param { get; set; }
}
