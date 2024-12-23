using System.Text.RegularExpressions;
using Cnblogs.DashScope.Core.Internals;
using Microsoft.ML.Tokenizers;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Local implementation for QWen tokenizer
/// </summary>
public partial class QWenTokenizer
{
    private static readonly Dictionary<string, int> SpecialTokens =
        new List<string>
            {
                "<|endoftext|>",
                "<|im_start|>",
                "<|im_end|>"
            }
            .Concat(Enumerable.Range(0, 205).Select(x => $"<|extra_{x}|>"))
            .Select((x, i) => new KeyValuePair<string, int>(x, 151643 + i))
            .ToDictionary();

    [GeneratedRegex(
        @"(?i:'s|'t|'re|'ve|'m|'ll|'d)|[^\r\n\p{L}\p{N}]?\p{L}+|\p{N}| ?[^\s\p{L}\p{N}]+[\r\n]*|\s*[\r\n]+|\s+(?!\S)|\s+",
        RegexOptions.Compiled,
        "zh-CN")]
    private static partial Regex Pattern();

    /// <summary>
    /// Created tokenizer
    /// </summary>
    public static Tokenizer Tokenizer { get; } = TiktokenTokenizer.Create(
        DashScopeEmbeddedResource.ReadBpeFile(),
        new RegexPreTokenizer(Pattern(), SpecialTokens),
        null,
        SpecialTokens);

    /// <summary>
    /// Encode text to tokens.
    /// </summary>
    /// <param name="text">The text to encode.</param>
    /// <returns></returns>
    public static IReadOnlyList<int> Encode(string text)
    {
        return Tokenizer.EncodeToIds(text);
    }

    /// <summary>
    /// Decode tokens.
    /// </summary>
    /// <param name="tokens">The tokens to be decoded</param>
    /// <returns></returns>
    public static string Decode(int[] tokens)
    {
        return Tokenizer.Decode(tokens);
    }

    /// <summary>
    /// Get token count for text.
    /// </summary>
    /// <param name="text">The text to tokenize.</param>
    /// <returns></returns>
    public static int CountTokens(string text)
    {
        return Tokenizer.CountTokens(text);
    }

    /// <summary>
    /// Find the index of the maximum encoding capacity without surpassing the token limit.
    /// </summary>
    /// <param name="text">The input text.</param>
    /// <param name="maxTokenCount">The maximum number of tokens to encode.</param>
    /// <param name="normalizedText">If the tokenizer's normalization is enabled or <paramRef name="considerNormalization" /> is <see langword="false"/>, this will be set to <paramRef name="text" /> in its normalized form; otherwise, this value will be set to <see langword="null"/>.</param>
    /// <param name="tokenCount">The token count can be generated which should be smaller than the maximum token count.</param>
    /// <returns></returns>
    public static int GetIndexByTokenCount(string text, int maxTokenCount, out string? normalizedText, out int tokenCount)
    {
        return Tokenizer.GetIndexByTokenCount(text, maxTokenCount, out normalizedText, out tokenCount);
    }
}
