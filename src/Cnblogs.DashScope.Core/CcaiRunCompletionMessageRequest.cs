using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Native prompt / message-protocol request for RunCompletionMessage.
/// Uses PascalCase JSON names per official OpenAPI examples.
/// </summary>
public class CcaiRunCompletionMessageRequest
{
    /// <summary>
    /// Message list (system / user / agent / function).
    /// </summary>
    [JsonPropertyName("Messages")]
    public required List<CcaiCompletionMessage> Messages { get; set; }

    /// <summary>
    /// Whether to enable SSE streaming.
    /// </summary>
    [JsonPropertyName("Stream")]
    public bool Stream { get; set; }

    /// <summary>
    /// Model code, e.g. tyxmTurbo.
    /// </summary>
    [JsonPropertyName("ModelCode")]
    public string? ModelCode { get; set; }
}

/// <summary>
/// A single message for RunCompletionMessage.
/// </summary>
public class CcaiCompletionMessage
{
    /// <summary>
    /// Message role: user, agent, system, or function.
    /// </summary>
    [JsonPropertyName("Role")]
    public required string Role { get; set; }

    /// <summary>
    /// Prompt / message content.
    /// </summary>
    [JsonPropertyName("Content")]
    public required string Content { get; set; }
}
