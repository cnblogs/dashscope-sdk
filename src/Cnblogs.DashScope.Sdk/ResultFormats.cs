namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// Available values for <see cref="TextGenerationParameters"/>.<see cref="TextGenerationParameters.ResultFormat"/>.
/// </summary>
public static class ResultFormats
{
    /// <summary>
    /// DashScope format, this is the default value.
    /// </summary>
    public const string Text = "text";

    /// <summary>
    /// OpenAI compatible format.
    /// </summary>
    public const string Message = "message";
}
