namespace Cnblogs.DashScope.Core;

/// <summary>
/// Available values for <see cref="TextGenerationParameters.ResultFormat"/>.<see cref="TextGenerationParameters"/>.
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
