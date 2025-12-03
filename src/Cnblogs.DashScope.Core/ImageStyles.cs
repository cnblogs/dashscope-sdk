namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Available styles for image synthesis task.
    /// </summary>
    public static class ImageStyles
    {
        /// <summary>
        /// Use the image style determined by model.
        /// </summary>
        public const string Auto = "<auto>";

        /// <summary>
        /// Generate 3d image in cartoon style.
        /// </summary>
        public const string Cartoon3D = "<3d cartoon>";

        /// <summary>
        /// Generate image in anime style.
        /// </summary>
        public const string Anime = "<anime>";

        /// <summary>
        /// Generate oil painting like image.
        /// </summary>
        public const string OilPainting = "<oil painting>";

        /// <summary>
        /// Generate water color style image.
        /// </summary>
        public const string WaterColor = "<watercolor>";

        /// <summary>
        /// Generate sketch-like image.
        /// </summary>
        public const string Sketch = "<sketch>";

        /// <summary>
        /// Generate image in chinese painting style.
        /// </summary>
        public const string ChinesePainting = "<chinese painting>";

        /// <summary>
        /// Generate image in flat illustration style.
        /// </summary>
        public const string FlatIllustration = "<flat illustration>";
    }
}
