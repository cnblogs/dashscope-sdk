namespace Cnblogs.DashScope.Core;

/// <summary>
/// A message of the conversation context of a real-time speech recognition task. The context
/// helps the model recognize proper nouns, see <see cref="SpeechRecognitionInput.Context"/>.
/// </summary>
/// <param name="Role">The role of the message. user: the recognized text of the user's speech in
/// previous turns, or a word list of the domain; assistant: the replies of the LLM in previous turns.</param>
/// <param name="Content">The contents of the message.</param>
public record SpeechRecognitionContext(string Role, IReadOnlyList<SpeechRecognitionContextContent> Content);
