namespace Cnblogs.DashScope.Core;

/// <summary>
/// The incremental content of a streamed OpenAI compatible chat completion chunk. A thinking
/// model sends the reasoning before the text; a model with audio output streams a part of the
/// text, then the audio speaking it, then the next part of the text. A chunk often repeats the
/// key of the other kinds of content with an empty value.
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
    /// The incremental reasoning of the thinking models, absent when the thinking is off.
    /// </summary>
    public string? ReasoningContent { get; set; }

    /// <summary>
    /// The incremental audio, present in the chunks of models with audio output.
    /// </summary>
    public OpenAiCompatibleChatDeltaAudio? Audio { get; set; }

    /// <summary>
    /// The incremental tool calls: the first piece of a call carries its id and name, the
    /// following ones only add up the arguments of the function.
    /// </summary>
    public List<ToolCall>? ToolCalls { get; set; }
}
