namespace Cnblogs.DashScope.Core;

/// <summary>
/// The output audio options of an OpenAI compatible chat completion request. Requires
/// <see cref="OpenAiCompatibleChatRequest.Modalities"/> to contain "audio".
/// </summary>
/// <param name="Voice">The voice of the output audio.</param>
/// <param name="Format">The format of the output audio, e.g. wav.</param>
public record OpenAiCompatibleChatAudioOptions(
    string? Voice = null,
    string? Format = null);
