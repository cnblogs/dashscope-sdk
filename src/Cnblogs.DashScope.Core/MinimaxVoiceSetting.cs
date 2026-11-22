namespace Cnblogs.DashScope.Core;

/// <summary>
/// The voice settings of a MiniMax speech synthesis request.
/// </summary>
public class MinimaxVoiceSetting
{
    /// <summary>
    /// The voice id. Leave it empty when <see cref="MultimodalInput.TimbreWeights"/> is set.
    /// </summary>
    public string? VoiceId { get; set; }

    /// <summary>
    /// The speech speed, range [0.5, 2.0], defaults to 1.0.
    /// </summary>
    public float? Speed { get; set; }

    /// <summary>
    /// The volume, range (0.0, 10.0], defaults to 1.0.
    /// </summary>
    public float? Vol { get; set; }

    /// <summary>
    /// The pitch, range [-12, 12], defaults to 0.
    /// </summary>
    public int? Pitch { get; set; }

    /// <summary>
    /// The emotion, the model picks one matching the text when omitted: happy, sad, angry,
    /// fearful, disgusted, surprised, calm or whisper (whisper is not supported by
    /// speech-2.8-hd / speech-2.8-turbo).
    /// </summary>
    public string? Emotion { get; set; }

    /// <summary>
    /// Whether to normalize Chinese and English text, improves number reading at a small
    /// latency cost, defaults to false.
    /// </summary>
    public bool? TextNormalization { get; set; }

    /// <summary>
    /// Whether to read out LaTeX formulas (Chinese only, formulas wrapped in $$),
    /// defaults to false.
    /// </summary>
    public bool? LatexRead { get; set; }
}
