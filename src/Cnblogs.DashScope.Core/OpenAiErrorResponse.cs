namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Represents an error response from DashScope compatible-mode API.
    /// </summary>
    /// <param name="Error">Error detail.</param>
    public record OpenAiErrorResponse(OpenAiError Error);
}
