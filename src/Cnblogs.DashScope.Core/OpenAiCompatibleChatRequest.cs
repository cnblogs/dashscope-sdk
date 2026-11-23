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
    /// The model to use, e.g. qwen3-livetranslate-flash.
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
    /// The streaming options. Streaming only.
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
}
