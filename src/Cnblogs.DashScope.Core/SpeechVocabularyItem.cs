namespace Cnblogs.DashScope.Core;

/// <summary>
/// A hot word entry in a speech vocabulary list.
/// </summary>
/// <param name="Text">Hot word text.</param>
/// <param name="Weight">Weight in [1, 5]. Commonly 4.</param>
/// <param name="Lang">Optional language code (zh, en, ja, ...).</param>
public record SpeechVocabularyItem(string Text, int Weight, string? Lang = null);
