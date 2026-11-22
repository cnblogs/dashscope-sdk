namespace Cnblogs.DashScope.Core;

/// <summary>
/// A reference audio of an audio generation request (qwen-audio-*-tts-next models).
/// </summary>
public class SpeechSynthesisReference
{
    /// <summary>
    /// The public url of the reference audio, mutually exclusive with
    /// <see cref="AudioData"/>.
    /// </summary>
    public string? AudioUrl { get; set; }

    /// <summary>
    /// The base64 data uri of the reference audio
    /// (<c>data:{mime_type};base64,{base64_encoded_data}</c>), mutually exclusive with
    /// <see cref="AudioUrl"/>.
    /// </summary>
    public string? AudioData { get; set; }
}
