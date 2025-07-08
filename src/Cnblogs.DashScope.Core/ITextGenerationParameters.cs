namespace Cnblogs.DashScope.Core;

/// <summary>
/// The text generation options.
/// </summary>
public interface ITextGenerationParameters
    : IIncrementalOutputParameter, ISeedParameter, IProbabilityParameter, IPenaltyParameter, IMaxTokenParameter,
        IStopTokenParameter
{
    /// <summary>
    /// The format of the result, must be <c>text</c> or <c>message</c>.
    /// </summary>
    /// <remarks>
    /// <c>text</c> - original text format.
    /// <para><c>message</c> - OpenAI compatible message format</para>
    /// </remarks>
    /// <example>
    /// Sets <c>result_format</c> to <c>message</c>
    /// <code>
    ///     parameter.ResultFormat = ResultFormats.Message;
    /// </code>
    /// </example>
    public string? ResultFormat { get; }

    /// <summary>
    /// The format of response message, must be <c>text</c> or <c>json_object</c>
    /// </summary>
    /// <remarks>
    /// This property is not <see cref="ResultFormat"/>. Be sure not to confuse them.
    /// </remarks>
    /// <example>
    /// Set response format to <c>json_object</c>.
    /// <code>
    ///     parameter.ResponseFormat = DashScopeResponseFormat.Json;
    /// </code>
    /// </example>
    public DashScopeResponseFormat? ResponseFormat { get; }

    /// <summary>
    /// Enable internet search when generation. Defaults to false.
    /// </summary>
    public bool? EnableSearch { get; }

    /// <summary>
    /// Search options. <see cref="EnableSearch"/> should set to true.
    /// </summary>
    public TextGenerationSearchOptions? SearchOptions { get; set; }

    /// <summary>
    /// Thinking option. Valid for supported models.(e.g. qwen3)
    /// </summary>
    public bool? EnableThinking { get; }

    /// <summary>
    /// Maximum length of thinking content. Valid for supported models.(e.g. qwen3)
    /// </summary>
    public int? ThinkingBudget { get; set; }

    /// <summary>
    /// Include log possibilities in response.
    /// </summary>
    public bool? Logprobs { get; set; }

    /// <summary>
    /// How many choices should be returned. Range: [0, 5]
    /// </summary>
    public int? TopLogprobs { get; set; }

    /// <summary>
    /// Available tools for model to call.
    /// </summary>
    public IEnumerable<ToolDefinition>? Tools { get; }

    /// <summary>
    /// Behavior when choosing tools.
    /// </summary>
    public ToolChoice? ToolChoice { get; }

    /// <summary>
    /// Whether to enable parallel tool calling
    /// </summary>
    public bool? ParallelToolCalls { get; }

    /// <summary>
    /// Options when using QWen-MT models.
    /// </summary>
    public TextGenerationTranslationOptions? TranslationOptions { get; set; }
}
