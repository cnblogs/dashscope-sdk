namespace Cnblogs.DashScope.Core;

/// <summary>
/// The token details of an ASR realtime usage.
/// </summary>
public class AsrRealtimeTokenDetails
{
    /// <summary>
    /// The token count of the text.
    /// </summary>
    public int? TextTokens { get; set; }

    /// <summary>
    /// The token count of the audio (25 tokens per second, a partial second counts as one).
    /// Input only.
    /// </summary>
    public int? AudioTokens { get; set; }
}
