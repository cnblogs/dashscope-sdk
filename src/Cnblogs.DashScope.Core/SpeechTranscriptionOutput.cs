namespace Cnblogs.DashScope.Core;

/// <summary>
/// Output for a speech transcription task.
/// </summary>
/// <param name="Result">The result of the task. qwen3-asr-flash-filetrans.</param>
/// <param name="Results">The per-file results of the task. Paraformer file transcription models.</param>
public record SpeechTranscriptionOutput(
    SpeechTranscriptionResult? Result,
    List<SpeechTranscriptionFileResult>? Results = null) : DashScopeTaskOutput;
