namespace Cnblogs.DashScope.Core;

/// <summary>
/// Input for a speech transcription task (qwen3-asr-flash-filetrans, Fun-ASR /
/// Qwen-Audio-3.x-ASR-Flash-Filetrans, paraformer-v2 and other file transcription models).
/// </summary>
public class SpeechTranscriptionInput
{
    /// <summary>
    /// The url of the audio file to transcribe. The url must be publicly accessible.
    /// qwen3-asr-flash-filetrans only.
    /// </summary>
    public string? FileUrl { get; set; }

    /// <summary>
    /// The urls of the audio files to transcribe. The urls must be publicly accessible.
    /// Fun-ASR / Qwen-Audio-3.x-ASR-Flash-Filetrans models accept exactly one url in the list,
    /// Paraformer file transcription models accept multiple ones.
    /// </summary>
    public List<string>? FileUrls { get; set; }

    /// <summary>
    /// The conversation context used to improve the recognition of proper nouns: the recognized
    /// text of the user's speech in previous turns (input_text) and the replies of the LLM
    /// (text). Fun-ASR / Qwen-Audio-3.x-ASR-Flash-Filetrans models only. Messages must be
    /// ordered by conversation turns, each user message before the matching assistant message.
    /// At most 5 messages of each content type are kept (the latest ones), the text of a turn
    /// is at most 400 characters in total.
    /// </summary>
    public List<SpeechRecognitionContext>? Context { get; set; }
}
