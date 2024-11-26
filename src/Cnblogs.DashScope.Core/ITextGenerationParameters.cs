namespace Cnblogs.DashScope.Core;

/// <summary>
/// The text generation options.
/// </summary>
public interface ITextGenerationParameters
    : IIncrementalOutputParameter, ISeedParameter, IProbabilityParameter, IPenaltyParameter, IMaxTokenParameter, IStopTokenParameter
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
    /// Available tools for model to call.
    /// </summary>
    public IEnumerable<ToolDefinition>? Tools { get; }

    /// <summary>
    /// Behavior when choosing tools.
    /// </summary>
    public ToolChoice? ToolChoice { get; }
}
