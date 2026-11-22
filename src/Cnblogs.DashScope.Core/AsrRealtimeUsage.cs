namespace Cnblogs.DashScope.Core;

/// <summary>
/// The usage of a completed transcription of an ASR realtime session.
/// </summary>
public class AsrRealtimeUsage
{
    /// <summary>
    /// The duration of the transcribed audio, in seconds.
    /// </summary>
    public int? Duration { get; set; }

    /// <summary>
    /// The total token count, the sum of <see cref="InputTokens"/> and <see cref="OutputTokens"/>.
    /// </summary>
    public int? TotalTokens { get; set; }

    /// <summary>
    /// The token count of the input.
    /// </summary>
    public int? InputTokens { get; set; }

    /// <summary>
    /// The token count of the output.
    /// </summary>
    public int? OutputTokens { get; set; }

    /// <summary>
    /// The token details of the input.
    /// </summary>
    public AsrRealtimeTokenDetails? InputTokensDetails { get; set; }

    /// <summary>
    /// The token details of the output.
    /// </summary>
    public AsrRealtimeTokenDetails? OutputTokensDetails { get; set; }
}
