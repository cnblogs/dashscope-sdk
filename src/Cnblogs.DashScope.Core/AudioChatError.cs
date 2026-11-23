namespace Cnblogs.DashScope.Core;

/// <summary>
/// The error of an audio chat session. Client errors (invalid_request_error) keep the
/// connection, server errors (server_error) terminate it.
/// </summary>
public class AudioChatError
{
    /// <summary>
    /// The type of the error: invalid_request_error or server_error.
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
    /// The parameter related to the error, e.g. response.create.
    /// </summary>
    public string? Param { get; set; }
}
