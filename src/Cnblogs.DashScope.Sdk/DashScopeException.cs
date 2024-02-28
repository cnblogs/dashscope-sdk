namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// Represents error detail for DashScope API calls.
/// </summary>
/// <param name="apiUrl">The requested api url.</param>
/// <param name="status">The status code of response. Would be 0 if no response is received.</param>
/// <param name="error">The error detail returned by server.</param>
/// <param name="message">The error message.</param>
public class DashScopeException(string? apiUrl, int status, DashScopeError? error, string message) : Exception(message)
{
    /// <summary>
    /// The requested api url.
    /// </summary>
    public string? ApiUrl { get; } = apiUrl;

    /// <summary>
    /// The error detail returned by server.
    /// </summary>
    public DashScopeError? Error { get; } = error;

    /// <summary>
    /// The status code of response. Would be 0 if no response is received.
    /// </summary>
    public int Status { get; } = status;
}
