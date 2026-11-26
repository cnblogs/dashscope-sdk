namespace Cnblogs.DashScope.Core;

/// <summary>
/// The answer of a <see cref="SystemOneQuestion"/>. The fields it carries depend on the type
/// of the question: a choice answer has <see cref="Choice"/>, a noul answer has
/// <see cref="Noul"/>, a score answer has <see cref="Score"/> and <see cref="Legend"/>.
/// </summary>
public class SystemOneAnswer
{
    /// <summary>
    /// The type of the question, the same as <see cref="SystemOneQuestion.Type"/>: choice, noul
    /// or score.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// The name of the selected option, for a choice question.
    /// </summary>
    public string? Choice { get; set; }

    /// <summary>
    /// The probability of yes, 0 for no and 1 for yes, for a noul question.
    /// </summary>
    public double? Noul { get; set; }

    /// <summary>
    /// The probability weighted expected level index for a score question, it may fall between
    /// two levels (e.g. 1.43). The level index to level description mapping is
    /// <see cref="Legend"/>.
    /// </summary>
    public double? Score { get; set; }

    /// <summary>
    /// The probability of every option / level, they sum to 1. Not returned for a noul question.
    /// </summary>
    public Dictionary<string, double>? Probabilities { get; set; }

    /// <summary>
    /// The confidence of the answer, for choice and score questions.
    /// </summary>
    public double? Confidence { get; set; }

    /// <summary>
    /// The level index to level description mapping, for a score question.
    /// </summary>
    public Dictionary<string, string>? Legend { get; set; }
}
