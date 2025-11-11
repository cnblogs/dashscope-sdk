namespace Cnblogs.DashScope.Core;

/// <summary>
/// Language annotation of the input file.
/// </summary>
/// <param name="Language">The language of the file.</param>
/// <param name="Type">The type of the file.</param>
public record MultimodalAnnotation(string Language, string Type);
