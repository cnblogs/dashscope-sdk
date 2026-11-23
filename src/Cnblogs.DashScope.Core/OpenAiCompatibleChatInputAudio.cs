namespace Cnblogs.DashScope.Core;

/// <summary>
/// The audio of an input_audio content part.
/// </summary>
/// <param name="Data">The url of the audio, or a base64 data uri of a local file.</param>
/// <param name="Format">The format of the audio, e.g. wav, mp3.</param>
public record OpenAiCompatibleChatInputAudio(
    string? Data = null,
    string? Format = null);
