namespace Cnblogs.DashScope.Core;

/// <summary>
/// Result of one file in a speech transcription task.
/// </summary>
/// <param name="FileUrl">Original file URL.</param>
/// <param name="TranscriptionUrl">URL of the transcription JSON (valid for 24 hours).</param>
/// <param name="SubtaskStatus">Subtask status.</param>
/// <param name="Code">Error code when failed.</param>
/// <param name="Message">Error message when failed.</param>
public record SpeechTranscriptionSubtaskResult(
    string? FileUrl = null,
    string? TranscriptionUrl = null,
    DashScopeTaskStatus? SubtaskStatus = null,
    string? Code = null,
    string? Message = null);
