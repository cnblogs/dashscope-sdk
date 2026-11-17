namespace Cnblogs.DashScope.Core;

/// <summary>
/// Text hot fix configuration for TTS.
/// </summary>
/// <param name="Pronunciation">Pronunciation fixes, each entry maps a word to its pinyin annotation (e.g. { "天气": "tian1 qi4" }).</param>
/// <param name="Replace">Text replacements applied before synthesis, each entry maps a word to its replacement (e.g. { "今天": "金天" }).</param>
public record SpeechSynthesizerHotFix(
    List<Dictionary<string, string>>? Pronunciation,
    List<Dictionary<string, string>>? Replace);
