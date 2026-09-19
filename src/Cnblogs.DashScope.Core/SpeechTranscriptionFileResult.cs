namespace Cnblogs.DashScope.Core;

/// <summary>
/// Transcription JSON downloaded from <see cref="SpeechTranscriptionSubtaskResult.TranscriptionUrl"/>.
/// </summary>
/// <param name="FileUrl">Original audio file URL.</param>
/// <param name="Properties">Audio properties.</param>
/// <param name="Transcripts">Per-channel transcripts.</param>
public record SpeechTranscriptionFileResult(
    string? FileUrl = null,
    SpeechTranscriptionFileProperties? Properties = null,
    List<SpeechTranscriptionTranscript>? Transcripts = null);

/// <summary>
/// Properties of the source audio file.
/// </summary>
/// <param name="AudioFormat">Audio format.</param>
/// <param name="Channels">Channel indexes present in the file.</param>
/// <param name="OriginalSamplingRate">Original sampling rate in Hz.</param>
/// <param name="OriginalDurationInMilliseconds">Original duration in milliseconds.</param>
public record SpeechTranscriptionFileProperties(
    string? AudioFormat = null,
    List<int>? Channels = null,
    int? OriginalSamplingRate = null,
    int? OriginalDurationInMilliseconds = null);

/// <summary>
/// Transcript of one audio channel.
/// </summary>
/// <param name="ChannelId">Channel index starting from 0.</param>
/// <param name="ContentDurationInMilliseconds">Speech content duration in milliseconds.</param>
/// <param name="Text">Full transcript text.</param>
/// <param name="Sentences">Sentence-level results.</param>
public record SpeechTranscriptionTranscript(
    int? ChannelId = null,
    int? ContentDurationInMilliseconds = null,
    string? Text = null,
    List<SpeechTranscriptionSentence>? Sentences = null);

/// <summary>
/// Sentence-level transcription result.
/// </summary>
/// <param name="BeginTime">Begin timestamp in milliseconds.</param>
/// <param name="EndTime">End timestamp in milliseconds.</param>
/// <param name="Text">Sentence text.</param>
/// <param name="SentenceId">Sentence id.</param>
/// <param name="SpeakerId">Speaker index when diarization is enabled.</param>
/// <param name="Words">Word-level results.</param>
public record SpeechTranscriptionSentence(
    int? BeginTime = null,
    int? EndTime = null,
    string? Text = null,
    int? SentenceId = null,
    int? SpeakerId = null,
    List<SpeechTranscriptionWord>? Words = null);

/// <summary>
/// Word-level transcription result.
/// </summary>
/// <param name="BeginTime">Begin timestamp in milliseconds.</param>
/// <param name="EndTime">End timestamp in milliseconds.</param>
/// <param name="Text">Word text.</param>
/// <param name="Punctuation">Punctuation after the word.</param>
public record SpeechTranscriptionWord(
    int? BeginTime = null,
    int? EndTime = null,
    string? Text = null,
    string? Punctuation = null);
