namespace Cnblogs.DashScope.Core;

/// <summary>
/// The request of an OpenAI compatible chat completion. The stream flag is controlled by
/// the client method - <see cref="IDashScopeClient.OpenAiCompatibleGetChatCompletionAsync"/>
/// sends stream false, <see cref="IDashScopeClient.OpenAiCompatibleGetChatCompletionStreamAsync"/>
/// sends stream true.
/// </summary>
public class OpenAiCompatibleChatRequest
{
    /// <summary>
    /// The model to use, e.g. qwen3-livetranslate-flash, qwen3.8-omni-flash.
    /// </summary>
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// The messages. Most audio / video translation models accept a single user message.
    /// </summary>
    public List<OpenAiCompatibleChatMessage> Messages { get; set; } = new();

    /// <summary>
    /// The output modalities: ["text"] (default) or ["text", "audio"].
    /// </summary>
    public List<string>? Modalities { get; set; }

    /// <summary>
    /// The output audio options, requires "audio" in <see cref="Modalities"/>.
    /// </summary>
    public OpenAiCompatibleChatAudioOptions? Audio { get; set; }

    /// <summary>
    /// The streaming options. Streaming only, a non-streaming request fails when it
    /// carries them (the client leaves them out there).
    /// </summary>
    public OpenAiCompatibleChatStreamOptions? StreamOptions { get; set; }

    /// <summary>
    /// The maximal token count of the output.
    /// </summary>
    public int? MaxTokens { get; set; }

    /// <summary>
    /// The random seed for reproducible outputs, range [0, 2^31 - 1].
    /// </summary>
    public int? Seed { get; set; }

    /// <summary>
    /// The sampling temperature, range [0, 2). Not recommended to change for translation
    /// accuracy.
    /// </summary>
    public float? Temperature { get; set; }

    /// <summary>
    /// The nucleus sampling threshold, range (0, 1.0]. Not recommended to change for
    /// translation accuracy.
    /// </summary>
    public float? TopP { get; set; }

    /// <summary>
    /// The size of the candidate set of the sampling, disabled when unset or above 100.
    /// A DashScope extension, not part of the OpenAI schema.
    /// </summary>
    public int? TopK { get; set; }

    /// <summary>
    /// The presence penalty, range [-2.0, 2.0]. Not recommended to change for translation
    /// accuracy.
    /// </summary>
    public float? PresencePenalty { get; set; }

    /// <summary>
    /// The repetition penalty of consecutive sequences, 1.0 for no penalty. A DashScope
    /// extension, not part of the OpenAI schema.
    /// </summary>
    public float? RepetitionPenalty { get; set; }

    /// <summary>
    /// The speech rate of the output audio, range [0.5, 2.0], defaults to 1.0. A DashScope
    /// extension of the qwen3-livetranslate-flash models.
    /// </summary>
    public float? SpeechRate { get; set; }

    /// <summary>
    /// The translation options, required by the qwen3-livetranslate-flash models. A
    /// DashScope extension, not part of the OpenAI schema.
    /// </summary>
    public OpenAiCompatibleTranslationOptions? TranslationOptions { get; set; }

    /// <summary>
    /// The effort of the thinking. qwen3.8-omni-flash thinks by default and takes none
    /// (turns the thinking off), minimal, low, medium, high, xhigh or max (the default);
    /// the reasoning comes back in the reasoning_content of the message / the deltas.
    /// Cannot be set together with <see cref="ThinkingBudget"/>.
    /// </summary>
    public string? ReasoningEffort { get; set; }

    /// <summary>
    /// The maximal token count of the thinking of qwen3.8-omni-flash. Cannot be set
    /// together with <see cref="ReasoningEffort"/>.
    /// </summary>
    public int? ThinkingBudget { get; set; }

    /// <summary>
    /// Whether to turn the thinking on. qwen3-omni-flash thinks when the flag is set (it is
    /// off by default) and does not output audio while thinking; qwen3.8-omni-flash reads the
    /// flag as well, set it to false to turn its thinking off.
    /// </summary>
    public bool? EnableThinking { get; set; }

    /// <summary>
    /// Whether to let the model search the web before answering: qwen3.8-omni-flash and
    /// the qwen3.5-omni models. Streaming only. qwen3.8-omni-flash additionally requires
    /// <see cref="SearchOptions.SearchStrategy"/> to be "agent" and is served on the
    /// workspace domain only (configure <see cref="DashScopeOptions.WorkspaceId"/>).
    /// </summary>
    public bool? EnableSearch { get; set; }

    /// <summary>
    /// The options of the web search, requires <see cref="EnableSearch"/>.
    /// </summary>
    public SearchOptions? SearchOptions { get; set; }

    /// <summary>
    /// Whether to keep the channels of a stereo or a four channel (FOA) input audio, so
    /// that the model can locate the sound. qwen3.8-omni-flash only, off by default.
    /// </summary>
    public bool? UseMultichannel { get; set; }

    /// <summary>
    /// The format of the output, e.g. <see cref="DashScopeResponseFormat.JsonObject"/>
    /// makes the model answer with a JSON object.
    /// </summary>
    public DashScopeResponseFormat? ResponseFormat { get; set; }

    /// <summary>
    /// The tools the model may call, e.g. the function calling of qwen3.8-omni-flash. The
    /// calls come back in the tool_calls of the message / of the deltas.
    /// </summary>
    public List<ToolDefinition>? Tools { get; set; }

    /// <summary>
    /// How the model picks a tool, auto by default.
    /// </summary>
    public ToolChoice? ToolChoice { get; set; }
}
