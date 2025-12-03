namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Extra info from deep research model.
    /// </summary>
    public class DashScopeDeepResearchInfo
    {
        /// <summary>
        /// Current research result.
        /// </summary>
        public DashScopeDeepResearchTask? Research { get; set; }

        /// <summary>
        /// References of final answers.
        /// </summary>
        public List<DashScopeDeepResearchReference>? References { get; set; }
    }
}
