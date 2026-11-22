namespace Cnblogs.DashScope.Core;

/// <summary>
/// Output for real-time speech recognition task. The qwen-audio-asr-flash-streaming models also
/// send the sentence text, a request id and a nested copy of the output next to
/// <paramref name="Sentence"/>, which is ignored.
/// </summary>
/// <param name="Sentence">The recognized sentence.</param>
public record SpeechRecognitionOutput(SpeechRecognitionSentence? Sentence);
