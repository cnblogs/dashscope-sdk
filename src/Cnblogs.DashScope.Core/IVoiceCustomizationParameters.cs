namespace Cnblogs.DashScope.Core;

/// <summary>
/// Optional parameters of a voice customization request: preview audio settings of
/// voice designing (create_voice with <see cref="VoiceCustomizationInput.VoicePrompt"/>).
/// </summary>
public interface IVoiceCustomizationParameters
{
    /// <summary>
    /// The sample rate (Hz) of the preview audio: 16000, 24000 or 48000, defaults to 24000.
    /// </summary>
    int? SampleRate { get; set; }

    /// <summary>
    /// The format of the preview audio: pcm, wav or mp3, defaults to wav.
    /// </summary>
    string? ResponseFormat { get; set; }
}
