namespace Cnblogs.DashScope.Core;

/// <summary>
/// The synthesized data of a MiniMax speech synthesis response.
/// </summary>
public class MinimaxSpeechData
{
    /// <summary>
    /// The synthesized audio, hex encoded (or a download url when the request asked for
    /// the url output format).
    /// </summary>
    public string? Audio { get; set; }

    /// <summary>
    /// Undocumented field, observed as an empty string on the wire.
    /// </summary>
    public string? Ced { get; set; }

    /// <summary>
    /// The audio stream status: 1 for synthesizing, 2 for finished.
    /// </summary>
    public int? Status { get; set; }
}
