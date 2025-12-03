namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Token usages for one model.
    /// </summary>
    /// <param name="ModelId">The id of the model.</param>
    /// <param name="InputTokens">Total input tokens of this model.</param>
    /// <param name="OutputTokens">Total output tokens from this model.</param>
    public record ApplicationModelUsage(string ModelId, int InputTokens, int OutputTokens);
}
