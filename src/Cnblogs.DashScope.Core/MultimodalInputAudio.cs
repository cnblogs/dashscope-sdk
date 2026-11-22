namespace Cnblogs.DashScope.Core;

/// <summary>
/// The input_audio content of a recorded speech recognition request
/// (Fun-ASR-Flash / Qwen-Audio-3.x-ASR-Flash models).
/// </summary>
/// <param name="Data">The audio to recognize: a publicly accessible url, or a base64 data uri
/// (<c>data:{MIME_TYPE};base64,{BASE64_ENCODED_DATA}</c>, e.g. <c>data:audio/wav;base64,...</c>).</param>
public record MultimodalInputAudio(string Data);
