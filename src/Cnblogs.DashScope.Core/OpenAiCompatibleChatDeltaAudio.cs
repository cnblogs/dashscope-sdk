namespace Cnblogs.DashScope.Core;

/// <summary>
/// The incremental audio of a streamed OpenAI compatible chat completion chunk.
/// </summary>
/// <param name="Data">The base64 encoded incremental audio.</param>
/// <param name="Id">The id of the output audio.</param>
/// <param name="ExpiresAt">The creation timestamp of the request.</param>
public record OpenAiCompatibleChatDeltaAudio(
    string? Data = null,
    string? Id = null,
    int? ExpiresAt = null);
