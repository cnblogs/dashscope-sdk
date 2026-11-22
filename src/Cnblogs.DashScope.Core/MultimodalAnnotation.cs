namespace Cnblogs.DashScope.Core;

/// <summary>
/// Language annotation of the input file.
/// </summary>
/// <param name="Language">The language of the file.</param>
/// <param name="Type">The type of the file, fixed to audio_info for qwen3-asr-flash models.</param>
/// <param name="Emotion">The emotion of the audio: surprised, neutral, happy, sad, disgusted,
/// angry or fearful. qwen3-asr-flash models.</param>
public record MultimodalAnnotation(string Language, string Type, string? Emotion = null);
