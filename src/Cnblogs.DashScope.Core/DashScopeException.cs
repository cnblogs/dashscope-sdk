namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents error detail for DashScope API calls.
/// </summary>
public class DashScopeException : Exception
{
    /// <summary>
    /// Represents error detail for DashScope API calls.
    /// </summary>
    /// <param name="apiUrl">The requested api url.</param>
    /// <param name="status">The status code of response. Would be 0 if no response is received.</param>
    /// <param name="error">The error detail returned by server.</param>
    /// <param name="message">The error message.</param>
    public DashScopeException(string? apiUrl, int status, DashScopeError? error, string message)
        : base(message)
    {
        ApiUrl = apiUrl;
        Error = error;
        Status = status;
    }

    /// <summary>
    /// The requested api url.
    /// </summary>
    public string? ApiUrl { get; }

    /// <summary>
    /// The error detail returned by server.
    /// </summary>
    public DashScopeError? Error { get; }

    /// <summary>
    /// The status code of response. Would be 0 if no response is received.
    /// </summary>
    public int Status { get; }
}
