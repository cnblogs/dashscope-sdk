namespace Cnblogs.DashScope.Core;

/// <summary>
/// The audio format of a live translation session. Server echo, nested in
/// <see cref="LiveTranslateAudioInput"/> and <see cref="LiveTranslateAudioOutput"/>.
/// </summary>
/// <param name="Type">The format of the audio: pcm.</param>
/// <param name="SampleRate">The sample rate of the audio in Hz: 16000 for the input,
/// 24000 for the output.</param>
public record LiveTranslateAudioFormat(
    string? Type = null,
    int? SampleRate = null);
