namespace Cnblogs.DashScope.Core;

/// <summary>
/// The text generation options.
/// </summary>
public class TextGenerationParameters : ITextGenerationParameters
{
    /// <inheritdoc />
    public string? ResultFormat { get; set; }

    /// <inheritdoc />
    public DashScopeResponseFormat? ResponseFormat { get; set; }

    /// <inheritdoc />
    public ulong? Seed { get; set; }

    /// <inheritdoc />
    public int? MaxTokens { get; set; }

    /// <inheritdoc />
    public float? TopP { get; set; }

    /// <inheritdoc />
    public int? TopK { get; set; }

    /// <inheritdoc />
    public float? RepetitionPenalty { get; set; }

    /// <inheritdoc />
    public float? PresencePenalty { get; set; }

    /// <inheritdoc />
    public float? Temperature { get; set; }

    /// <inheritdoc />
    public TextGenerationStop? Stop { get; set; }

    /// <inheritdoc />
    public bool? EnableSearch { get; set; }

    /// <inheritdoc />
    public TextGenerationSearchOptions? SearchOptions { get; set; }

    /// <inheritdoc />
    public bool? EnableThinking { get; set; }

    /// <inheritdoc />
    public int? ThinkingBudget { get; set; }

    /// <inheritdoc />
    public bool? Logprobs { get; set; }

    /// <inheritdoc />
    public int? TopLogprobs { get; set; }

    /// <inheritdoc />
    public IEnumerable<ToolDefinition>? Tools { get; set; }

    /// <inheritdoc />
    public ToolChoice? ToolChoice { get; set; }

    /// <inheritdoc />
    public bool? ParallelToolCalls { get; set; }

    /// <inheritdoc />
    public TextGenerationTranslationOptions? TranslationOptions { get; set; }

    /// <inheritdoc />
    public CacheControlOptions? CacheControl { get; set; }

    /// <inheritdoc />
    public int? N { get; set; }

    /// <inheritdoc />
    public Dictionary<string, int>? LogitBias { get; set; }

    /// <inheritdoc />
    public bool? IncrementalOutput { get; set; }
}
