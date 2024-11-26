using System.Text.Json.Serialization;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents stop parameter in <see cref="TextGenerationParameters"/>, can be int, string, int array or string array.
/// </summary>
/// <example>
/// <code>
/// var parameter = new TextGenerationParameters()
/// {
///     Stop = ["你好"],
///     // Stop = "你好",
///     // Stop = [38, 42],
///     // Stop = new int[][] { [38, 42] },
/// }
/// </code>
/// </example>
[JsonConverter(typeof(TextGenerationStopConvertor))]
public class TextGenerationStop
{
    /// <summary>
    /// Generation will stop if this string is going to be generated.
    /// </summary>
    public string? StopString { get; }

    /// <summary>
    /// Generation will stop if one of this strings is going to be generated.
    /// </summary>
    public IEnumerable<string>? StopStrings { get; }

    /// <summary>
    /// Generation will stop if token with this id is going to be generated.
    /// </summary>
    public int[]? StopToken { get; }

    /// <summary>
    /// Generation will stop if token in this ids is going to be generated.
    /// </summary>
    public IEnumerable<int[]>? StopTokens { get; }

    /// <summary>
    /// Creates a <see cref="TextGenerationStop"/> with single string.
    /// </summary>
    /// <param name="stopString">The stop string.</param>
    public TextGenerationStop(string stopString)
    {
        StopString = stopString;
    }

    /// <summary>
    /// Creates a <see cref="TextGenerationStop"/> with multiple strings.
    /// </summary>
    /// <param name="stopStrings">The stop strings.</param>
    public TextGenerationStop(IEnumerable<string> stopStrings)
    {
        StopStrings = stopStrings;
    }

    /// <summary>
    /// Creates a <see cref="TextGenerationStop"/> with single token id.
    /// </summary>
    /// <param name="stopToken">The stop token id.</param>
    public TextGenerationStop(int[] stopToken)
    {
        StopToken = stopToken;
    }

    /// <summary>
    /// Creates a <see cref="TextGenerationStop"/> with multiple token ids.
    /// </summary>
    /// <param name="stopTokens">The stop token ids.</param>
    public TextGenerationStop(IEnumerable<int[]> stopTokens)
    {
        StopTokens = stopTokens;
    }

    /// <summary>
    /// Creates a <see cref="TextGenerationStop"/> with single string.
    /// </summary>
    /// <param name="stop">The stop string.</param>
    public static implicit operator TextGenerationStop(string stop) => new(stop);

    /// <summary>
    /// Creates a <see cref="TextGenerationStop"/> with multiple string.
    /// </summary>
    /// <param name="stops">The stop strings.</param>
    public static implicit operator TextGenerationStop(string[] stops) => new(stops);

    /// <summary>
    /// Creates a <see cref="TextGenerationStop"/> with multiple string.
    /// </summary>
    /// <param name="stops">The stop strings.</param>
    public static implicit operator TextGenerationStop(List<string> stops) => new(stops);

    /// <summary>
    /// Creates a <see cref="TextGenerationStop"/> with token ids.
    /// </summary>
    /// <param name="stop">The stop token id.</param>
    public static implicit operator TextGenerationStop(int[] stop) => new(stop);

    /// <summary>
    /// Creates a <see cref="TextGenerationStop"/> with multiple token ids.
    /// </summary>
    /// <param name="stops">The token ids.</param>
    public static implicit operator TextGenerationStop(int[][] stops) => new(stops);

    /// <summary>
    /// Creates a <see cref="TextGenerationStop"/> with multiple token ids.
    /// </summary>
    /// <param name="stops">The token ids.</param>
    public static implicit operator TextGenerationStop(List<int[]> stops) => new(stops);
}
