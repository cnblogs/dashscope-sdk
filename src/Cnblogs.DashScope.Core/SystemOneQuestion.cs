namespace Cnblogs.DashScope.Core;

/// <summary>
/// A question of a <see cref="SystemOneDecisionInput"/>. <see cref="Type"/> decides the shape
/// of <see cref="Criteria"/>, the factory methods <see cref="Choice"/>, <see cref="Noul"/> and
/// <see cref="Score"/> build a matching pair.
/// </summary>
/// <param name="Type">The type of the question: <c>choice</c> picks one of the options,
/// <c>noul</c> is a yes / no judgement, <c>score</c> is an ordered scale.</param>
/// <param name="Instructions">The description or the judging standard of the question.</param>
/// <param name="Criteria">The criteria of the question: an option name to option description
/// map for a choice question, an optional map with a <c>true</c> and a <c>false</c> description
/// for a noul question, the level descriptions from low to high for a score question.</param>
public record SystemOneQuestion(string Type, string? Instructions, object? Criteria)
{
    /// <summary>
    /// Create a question that picks one of the options.
    /// </summary>
    /// <param name="instructions">The description or the judging standard of the question.</param>
    /// <param name="criteria">The options, an option name to option description map (at most 255
    /// options). Pass every option, it is recommended to add an <c>other</c> option as the
    /// fallback.</param>
    /// <returns>The question.</returns>
    public static SystemOneQuestion Choice(string? instructions, IDictionary<string, string> criteria)
        => new("choice", instructions, criteria);

    /// <summary>
    /// Create a yes / no question, the answer is the probability of yes.
    /// </summary>
    /// <param name="instructions">The description or the judging standard of the question.</param>
    /// <param name="criteria">The optional descriptions of the yes and the no answer, a map with
    /// a <c>true</c> and a <c>false</c> key.</param>
    /// <returns>The question.</returns>
    public static SystemOneQuestion Noul(string? instructions, IDictionary<string, string>? criteria = null)
        => new("noul", instructions, criteria);

    /// <summary>
    /// Create a question answered with a level of an ordered scale.
    /// </summary>
    /// <param name="instructions">The description or the judging standard of the question.</param>
    /// <param name="criteria">The level descriptions from low to high (2-255 levels, 3-7 levels
    /// that can be told apart clearly are recommended).</param>
    /// <returns>The question.</returns>
    public static SystemOneQuestion Score(string? instructions, IReadOnlyList<string> criteria)
        => new("score", instructions, criteria);
}
