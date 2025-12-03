namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// A text pair that used for translation reference.
    /// </summary>
    /// <param name="Source">The text in source language.</param>
    /// <param name="Target">The text in target language.</param>
    public record TranslationReference(string Source, string Target);
}
