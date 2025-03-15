namespace Cnblogs.DashScope.Core;

/// <summary>
/// Inputs for application call.
/// </summary>
public class ApplicationInput
{
    /// <summary>
    /// The prompt for model to generate response upon. Optional when <see cref="Messages"/> has been set.
    /// </summary>
    /// <remarks>
    /// Prompt will be appended to <see cref="Messages"/> when both set.
    /// </remarks>
    public string? Prompt { get; set; } = null;

    /// <summary>
    /// The session id for conversation history. This will be ignored if <see cref="Messages"/> has been set.
    /// </summary>
    public string? SessionId { get; set; } = null;

    /// <summary>
    /// The conversation history.
    /// </summary>
    public IEnumerable<ApplicationMessage>? Messages { get; set; } = null;

    /// <summary>
    /// The id of memory when enabled.
    /// </summary>
    public string? MemoryId { get; set; } = null;

    /// <summary>
    /// List of image urls for inputs.
    /// </summary>
    public IEnumerable<string>? ImageList { get; set; }
}

/// <summary>
/// Inputs for application call.
/// </summary>
/// <typeparam name="TBizContent">User defined custom content.</typeparam>
public class ApplicationInput<TBizContent> : ApplicationInput
    where TBizContent : class
{
    /// <summary>
    /// User defined content.
    /// </summary>
    public TBizContent? Content { get; set; } = null;
}
