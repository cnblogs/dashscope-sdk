namespace Cnblogs.DashScope.Core;

/// <summary>
/// One voice of a mixed-timbre MiniMax speech synthesis request.
/// </summary>
public class MinimaxTimbreWeight
{
    /// <summary>
    /// The voice id.
    /// </summary>
    public string VoiceId { get; set; } = string.Empty;

    /// <summary>
    /// The weight of the voice, range [1, 100]. The higher the weight, the more similar
    /// the synthesized voice is to this voice.
    /// </summary>
    public int Weight { get; set; }
}
