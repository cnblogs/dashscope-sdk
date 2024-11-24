namespace Cnblogs.DashScope.Core;

/// <summary>
/// The text generation options.
/// </summary>
public interface ITextGenerationParameters : IIncrementalOutputParameter, ISeedParameter, IProbabilityParameter
{
    /// <summary>
    /// The format of the result message, must be <c>text</c> or <c>message</c>.
    /// </summary>
    /// <remarks>
    /// <c>text</c> - original text format.
    /// <para><c>message</c> - OpenAI compatible message format</para>
    /// </remarks>
    public string? ResultFormat { get; }

    /// <summary>
    /// The maximum number of tokens the model can generate.
    /// </summary>
    /// <remarks>
    /// Default and maximum number of tokens is 1500(qwen-turbo) or 2000(qwen-max, qwen-max-1201, qwen-max-longcontext, qwen-plus).
    /// </remarks>
    public int? MaxTokens { get; }

    /// <summary>
    /// Increasing the repetition penalty can reduce the amount of repetition in the model’s output. A value of 1.0 indicates no penalty, with the default set at 1.1.
    /// </summary>
    public float? RepetitionPenalty { get; }

    /// <summary>
    /// Controls the diversity of generations. Lower temperature leads to more consistent result.
    /// </summary>
    /// <remarks>Must be in [0,2), defaults to 0.85.</remarks>
    public float? Temperature { get; }

    /// <summary>
    /// Stop generation when next token or string is in given range.
    /// </summary>
    public TextGenerationStop? Stop { get; }

    /// <summary>
    /// Enable internet search when generation. Defaults to false.
    /// </summary>
    public bool? EnableSearch { get; }

    /// <summary>
    /// Available tools for model to call.
    /// </summary>
    public IEnumerable<ToolDefinition>? Tools { get; }

    /// <summary>
    ///
    /// </summary>
    public ToolChoice? ToolChoice { get; }
}
