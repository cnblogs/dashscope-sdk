namespace Cnblogs.DashScope.Core;

/// <summary>
/// The output of one text completion request.
/// </summary>
public class TextGenerationOutput
{
    /// <summary>
    /// Not null when <see cref="TextGenerationParameters"/>.<see cref="TextGenerationParameters.ResultFormat"/> is "text".
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// Not null when <see cref="TextGenerationParameters"/>.<see cref="TextGenerationParameters.ResultFormat"/> is "text". An string "null" will be applied if generation is not finished yet.
    /// </summary>
    public string? FinishReason { get; set; }

    /// <summary>
    /// Not null when <see cref="TextGenerationParameters"/>.<see cref="TextGenerationParameters.ResultFormat"/> is "message".
    /// </summary>
    public List<TextGenerationChoice>? Choices { get; set; }
}
