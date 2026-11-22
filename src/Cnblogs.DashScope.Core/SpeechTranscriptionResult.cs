namespace Cnblogs.DashScope.Core;

/// <summary>
/// The result of a speech transcription task.
/// </summary>
/// <param name="TranscriptionUrl">The url of the transcription result file (JSON), valid for
/// 24 hours. Download it in time and persist it.</param>
public record SpeechTranscriptionResult(string TranscriptionUrl);
