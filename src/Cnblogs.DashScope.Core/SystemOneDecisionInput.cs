namespace Cnblogs.DashScope.Core;

/// <summary>
/// The request of a decision model call (the System One protocol, models <c>decision-model</c>
/// and <c>decision-model-preview</c>). The model answers every question with a single forward
/// pass - a choice, a yes / no probability or a score, each with its probability distribution -
/// and generates no text.
/// </summary>
/// <example>
/// <code>
/// var input = new SystemOneDecisionInput
/// {
///     Model = "decision-model",
///     State = new { ticket_id = "T-1001", content = "The payment has not arrived 24 hours after the order." },
///     Questions = new Dictionary&lt;string, SystemOneQuestion&gt;
///     {
///         ["department"] = SystemOneQuestion.Choice("Which team should handle it?", new Dictionary&lt;string, string&gt;
///         {
///             ["billing"] = "Payment, refund and invoice issues",
///             ["technical"] = "Product failures and integration issues"
///         }),
///         ["escalate"] = SystemOneQuestion.Noul("Should the on-call engineer be notified right away?")
///     }
/// };
/// </code>
/// </example>
public class SystemOneDecisionInput
{
    /// <summary>
    /// The model to use, e.g. decision-model.
    /// </summary>
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// The business state to decide on: a text, a conversation or a structured object. An object
    /// is serialized and sent to the model as it is, pass an anonymous object, a dictionary or a
    /// list for the structured forms.
    /// </summary>
    public object State { get; set; } = string.Empty;

    /// <summary>
    /// The questions to answer, keyed by a caller defined question id that is used to look the
    /// answer up in <see cref="SystemOneDecision.Answers"/>. At most 16 questions, the latency
    /// grows with the question count.
    /// </summary>
    public Dictionary<string, SystemOneQuestion> Questions { get; set; } = new();
}
