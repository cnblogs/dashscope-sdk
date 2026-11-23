namespace Cnblogs.DashScope.Core;

/// <summary>
/// The token details of an audio chat usage.
/// </summary>
public class AudioChatTokenDetails
{
    /// <summary>
    /// The token count of the text.
    /// </summary>
    public int? TextTokens { get; set; }

    /// <summary>
    /// The token count of the audio.
    /// </summary>
    public int? AudioTokens { get; set; }
}
