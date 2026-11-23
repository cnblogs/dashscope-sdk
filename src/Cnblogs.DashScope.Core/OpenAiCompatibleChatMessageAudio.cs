namespace Cnblogs.DashScope.Core;

/// <summary>
/// The audio of a complete OpenAI compatible chat completion message.
/// </summary>
/// <param name="Id">The id of the audio.</param>
/// <param name="Data">The base64 encoded audio.</param>
/// <param name="ExpiresAt">The timestamp the audio expires at.</param>
/// <param name="Transcript">The transcript of the audio, when the model returns one.</param>
public record OpenAiCompatibleChatMessageAudio(
    string? Id = null,
    string? Data = null,
    int? ExpiresAt = null,
    string? Transcript = null);
