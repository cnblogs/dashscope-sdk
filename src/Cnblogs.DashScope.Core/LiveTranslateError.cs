namespace Cnblogs.DashScope.Core;

/// <summary>
/// The error of a live translation session.
/// </summary>
public class LiveTranslateError
{
    /// <summary>
    /// The type of the error.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// The code of the error.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// The message of the error.
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// The parameter related to the error.
    /// </summary>
    public string? Param { get; set; }
}
