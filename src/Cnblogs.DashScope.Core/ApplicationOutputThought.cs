namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// The model thought output.
    /// </summary>
    /// <param name="Thought">The thought content of the model.</param>
    /// <param name="ActionType">Type of the action. e.g. <c>agentRag</c>, <c>reasoning</c>.</param>
    /// <param name="ActionName">The name of the action.</param>
    /// <param name="Action">The action been executed.</param>
    /// <param name="ActionInputStream">The streaming result of action input.</param>
    /// <param name="ActionInput">The input of the action.</param>
    /// <param name="Observation">Lookup or plugin output.</param>
    /// <param name="Response">Reasoning output when using DeepSeek-R1.</param>
    /// <param name="Arguments">Arguments of the action.</param>
    public record ApplicationOutputThought(
        string? Thought,
        string? ActionType,
        string? ActionName,
        string? Action,
        string? ActionInputStream,
        string? ActionInput,
        string? Observation,
        string? Response,
        string? Arguments);
}
