namespace Cnblogs.DashScope.Core;

/// <summary>
/// Input for real-time speech recognition task. The audio is always sent as binary websocket
/// frames after the task is started, the input only carries the conversation context.
/// </summary>
public class SpeechRecognitionInput
{
    /// <summary>
    /// The conversation context used to improve the recognition of proper nouns. qwen-audio-*-asr-flash
    /// models (qwen-audio-3.1-asr-flash-message, qwen-audio-3.0-asr-flash-streaming and
    /// qwen-audio-3.1-asr-flash-streaming). Messages must be ordered by conversation turns, each user
    /// message before the matching assistant message. At most 5 messages of each content type are
    /// kept (the latest ones), the text of a turn is at most 400 characters in total, the exceeding
    /// part is truncated from the end.
    /// </summary>
    public List<SpeechRecognitionContext>? Context { get; set; }
}
