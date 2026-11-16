namespace Cnblogs.DashScope.Core;

/// <summary>
/// Available values for <see cref="ITextGenerationParameters.OutputFormat"/>, only available for qwen-deep-research models.
/// </summary>
public static class DeepResearchOutputFormats
{
    /// <summary>
    /// Generate a detailed research report of about 6000 tokens, this is the default value.
    /// </summary>
    public const string ModelDetailedReport = "model_detailed_report";

    /// <summary>
    /// Generate a summary-style research report of about 1500-2000 tokens.
    /// </summary>
    public const string ModelSummaryReport = "model_summary_report";
}
