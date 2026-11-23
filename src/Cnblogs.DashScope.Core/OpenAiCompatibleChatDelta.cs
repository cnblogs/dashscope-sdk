namespace Cnblogs.DashScope.Core;

/// <summary>
/// The incremental content of a streamed OpenAI compatible chat completion chunk.
/// </summary>
public class OpenAiCompatibleChatDelta
{
    /// <summary>
    /// The role of the message, set in the first chunk only.
    /// </summary>
    public string? Role { get; set; }

    /// <summary>
    /// The incremental text.
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// The incremental audio, present in the chunks of models with audio output.
    /// </summary>
    public OpenAiCompatibleChatDeltaAudio? Audio { get; set; }
}
