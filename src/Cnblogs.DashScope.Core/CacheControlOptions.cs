namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Cache control options for model.
    /// </summary>
    public class CacheControlOptions
    {
        /// <summary>
        /// The cache type, no need to change, defaults to "ephemeral".
        /// </summary>
        public string Type { get; set; } = "ephemeral";
    }
}
