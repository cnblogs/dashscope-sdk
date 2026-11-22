using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// The per-file result of a multi-file speech transcription task. Fun-ASR / Qwen-Audio-3.x-ASR-Flash-Filetrans
/// and Paraformer file transcription models.
/// </summary>
/// <param name="FileUrl">The url of the transcribed file.</param>
/// <param name="TranscriptionUrl">The url of the transcription result file (JSON), valid for
/// 24 hours. Download it in time and persist it.</param>
/// <param name="SubTaskStatus">The status of the subtask of this file.</param>
/// <param name="Code">The error code of the subtask, not null when it failed.</param>
/// <param name="Message">The error message of the subtask, not null when it failed.</param>
public record SpeechTranscriptionFileResult(
    string FileUrl,
    string? TranscriptionUrl = null,
    [property: JsonPropertyName("subtask_status")] string? SubTaskStatus = null,
    string? Code = null,
    string? Message = null);
