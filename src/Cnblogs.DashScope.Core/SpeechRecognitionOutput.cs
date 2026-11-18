namespace Cnblogs.DashScope.Core;

/// <summary>
/// Output for real-time speech recognition task.
/// </summary>
/// <param name="Sentence">The recognized sentence.</param>
public record SpeechRecognitionOutput(SpeechRecognitionSentence? Sentence);
