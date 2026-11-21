namespace Cnblogs.DashScope.Core;

/// <summary>
/// The pronunciation dictionary of a MiniMax speech synthesis request.
/// </summary>
public class MinimaxPronunciationDict
{
    /// <summary>
    /// The pronunciation replacement rules, each in the form of
    /// <c>text/(pinyin1)(pinyin2)</c> (tone as a digit suffix, 5 for the neutral tone)
    /// or <c>text/replacement</c>, e.g. <c>燕少飞/(yan4)(shao3)(fei1)</c> or
    /// <c>omg/oh my god</c>.
    /// </summary>
    public List<string>? Tone { get; set; }
}
