namespace Cnblogs.DashScope.Core;

/// <summary>
/// The prompt sample of a MiniMax voice clone request, improves the similarity and
/// stability of the cloned voice.
/// </summary>
public class MinimaxClonePrompt
{
    /// <summary>
    /// The url of the sample audio: mp3, m4a or wav, shorter than 8 seconds, at most
    /// 20 MB.
    /// </summary>
    public string? PromptAudio { get; set; }

    /// <summary>
    /// The text of the sample audio, must match its content and end with punctuation.
    /// </summary>
    public string? PromptText { get; set; }
}
