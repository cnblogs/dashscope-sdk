namespace Cnblogs.DashScope.Core;

/// <summary>
/// The streaming output options of a MiniMax speech synthesis request, only takes effect
/// when the SSE header is enabled.
/// </summary>
public class MinimaxStreamOptions
{
    /// <summary>
    /// Whether the audio field of the final streaming frame omits the aggregated audio of
    /// the whole synthesis, defaults to false. When true, clients need to concatenate the
    /// audio chunks of all frames themselves; useful when playing or saving chunks on
    /// arrival to reduce the tail package size.
    /// </summary>
    public bool? ExcludeAggregatedAudio { get; set; }
}
