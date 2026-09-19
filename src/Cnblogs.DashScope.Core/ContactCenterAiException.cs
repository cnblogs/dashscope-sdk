namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents an error from LingQue CCAI Conversation Analysis AIO APIs.
/// </summary>
public class ContactCenterAiException : Exception
{
    /// <summary>
    /// Creates a new exception.
    /// </summary>
    /// <param name="apiUrl">Requested URL.</param>
    /// <param name="status">HTTP status code; 0 if no response.</param>
    /// <param name="errorCode">CCAI error code.</param>
    /// <param name="errorInfo">CCAI error info.</param>
    /// <param name="requestId">Request id when available.</param>
    /// <param name="message">Exception message.</param>
    public ContactCenterAiException(
        string? apiUrl,
        int status,
        string? errorCode,
        string? errorInfo,
        string? requestId,
        string message)
        : base(message)
    {
        ApiUrl = apiUrl;
        Status = status;
        ErrorCode = errorCode;
        ErrorInfo = errorInfo;
        RequestId = requestId;
    }

    /// <summary>
    /// Requested URL.
    /// </summary>
    public string? ApiUrl { get; }

    /// <summary>
    /// HTTP status code.
    /// </summary>
    public int Status { get; }

    /// <summary>
    /// CCAI error code.
    /// </summary>
    public string? ErrorCode { get; }

    /// <summary>
    /// CCAI error info.
    /// </summary>
    public string? ErrorInfo { get; }

    /// <summary>
    /// Request id.
    /// </summary>
    public string? RequestId { get; }
}
