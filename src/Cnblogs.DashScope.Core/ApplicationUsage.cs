namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Total token usages of this application call.
    /// </summary>
    /// <param name="Models">All models been used and their token usages. Can be null when workflow application without using any model.</param>
    public record ApplicationUsage(List<ApplicationModelUsage>? Models);
}
