namespace Cnblogs.DashScope.Core;

/// <summary>
/// A term of the glossary of an omni-modal translation.
/// </summary>
/// <param name="Src">The term in the source language.</param>
/// <param name="Tgt">The translation of <paramref name="Src"/> in the target language.</param>
public record UniTranslationGlossaryTerm(string Src, string? Tgt = null);
