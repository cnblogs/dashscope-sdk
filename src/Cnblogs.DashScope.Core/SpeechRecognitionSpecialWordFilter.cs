namespace Cnblogs.DashScope.Core;

/// <summary>
/// The sensitive word filtering of a real-time speech recognition result, see
/// <see cref="SpeechRecognitionParameters.SpecialWordFilter"/>. At most 32 sensitive words in total.
/// </summary>
public class SpeechRecognitionSpecialWordFilter
{
    /// <summary>
    /// The sensitive words to replace with an equal number of * in the result.
    /// </summary>
    public SpeechRecognitionSpecialWordList? FilterWithSigned { get; set; }

    /// <summary>
    /// The sensitive words to remove from the result.
    /// </summary>
    public SpeechRecognitionSpecialWordList? FilterWithEmpty { get; set; }

    /// <summary>
    /// Whether to enable the sensitive word filtering, defaults to false.
    /// </summary>
    public bool? SystemReservedFilter { get; set; }
}
