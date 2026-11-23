namespace Cnblogs.DashScope.Core;

/// <summary>
/// The token details of a live translation usage.
/// </summary>
public class LiveTranslateTokenDetails
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
