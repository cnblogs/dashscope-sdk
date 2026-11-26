namespace Cnblogs.DashScope.Core;

/// <summary>
/// The token details of an omni realtime usage.
/// </summary>
public class OmniRealtimeTokenDetails
{
    /// <summary>
    /// The token count of the text.
    /// </summary>
    public int? TextTokens { get; set; }

    /// <summary>
    /// The token count of the audio (7 tokens per second of input, 12.5 per second of output,
    /// a partial second counts as one).
    /// </summary>
    public int? AudioTokens { get; set; }

    /// <summary>
    /// The token count of the images sent with input_image_buffer.append (one per 32x32 pixels,
    /// at least 4 and at most 1280 per image). Input only, present when images were sent.
    /// </summary>
    public int? VideoTokens { get; set; }
}
