namespace Cnblogs.DashScope.Core;

/// <summary>
/// A list of sensitive words of a <see cref="SpeechRecognitionSpecialWordFilter"/>.
/// </summary>
public class SpeechRecognitionSpecialWordList
{
    /// <summary>
    /// The sensitive words.
    /// </summary>
    public List<string> WordList { get; set; } = new();
}
