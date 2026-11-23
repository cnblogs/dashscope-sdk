namespace Cnblogs.DashScope.Core;

/// <summary>
/// The translation options of an OpenAI compatible chat completion request, required by
/// the qwen3-livetranslate-flash models (audio / video file translation).
/// </summary>
/// <param name="TargetLang">The target language as its full English name, e.g. Chinese,
/// English. Required.</param>
/// <param name="SourceLang">The source language as its full English name. Unset to detect
/// automatically.</param>
public record OpenAiCompatibleTranslationOptions(
    string? TargetLang = null,
    string? SourceLang = null);
