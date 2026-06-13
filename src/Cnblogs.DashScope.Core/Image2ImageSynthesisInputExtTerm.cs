namespace Cnblogs.DashScope.Core;

/// <summary>
/// A pair of source and target terms used by the translation task.
/// </summary>
/// <param name="Src">The term in the source language.</param>
/// <param name="Tgt">The translation of <paramref name="Src"/> in the target language.</param>
public record Image2ImageSynthesisInputExtTerm(string Src, string Tgt);
