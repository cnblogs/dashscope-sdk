namespace Cnblogs.DashScope.Core;

/// <summary>
/// The result of one image synthesis subtask.
/// </summary>
/// <param name="Url">The url of generated image.</param>
/// <param name="Code">The error code.</param>
/// <param name="Message">The error message.</param>
/// <param name="OrigPrompt">The prompt before rewriting, present when the model rewrites the prompt.</param>
/// <param name="ActualPrompt">The actual prompt used by the model after rewriting.</param>
public record ImageSynthesisResult(
    string? Url = null,
    string? Code = null,
    string? Message = null,
    string? OrigPrompt = null,
    string? ActualPrompt = null);
