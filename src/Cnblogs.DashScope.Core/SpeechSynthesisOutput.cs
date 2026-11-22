namespace Cnblogs.DashScope.Core;

/// <summary>
/// The output of a speech synthesis request of qwen-audio-tts models.
/// </summary>
/// <param name="FinishReason">The synthesis finish reason: "null" while synthesizing,
/// "stop" when the synthesis is finished.</param>
/// <param name="Type">The sub-event type, streaming mode only: sentence-begin (with the
/// sentence text), sentence-synthesis (one audio chunk each, in order) or sentence-end
/// (with the sentence text and usage).</param>
/// <param name="OriginalText">The sentence text after splitting the input text. May be
/// absent for the last sentence.</param>
/// <param name="NormalizedText">The sentence text after normalization (e.g. digits
/// converted to their spoken form). Undocumented field, present in sentence events.</param>
/// <param name="Sentence">The sentence info with word timestamps.</param>
/// <param name="Audio">The synthesized audio: base64 chunks in <see cref="SpeechSynthesisAudio.Data"/>
/// while streaming, the complete file url in the final frame.</param>
public record SpeechSynthesisOutput(
    string FinishReason,
    string? Type = null,
    string? OriginalText = null,
    string? NormalizedText = null,
    SpeechSynthesisSentence? Sentence = null,
    SpeechSynthesisAudio? Audio = null);
