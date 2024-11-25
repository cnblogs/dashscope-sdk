namespace Cnblogs.DashScope.Core;

/// <summary>
/// The text generation options.
/// </summary>
public interface ITextGenerationParameters
    : IIncrementalOutputParameter, ISeedParameter, IProbabilityParameter, IPenaltyParameter, IMaxTokenParameter, IStopTokenParameter
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
