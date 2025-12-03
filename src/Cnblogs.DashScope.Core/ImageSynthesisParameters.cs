namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Optional parameters for image synthesis task.
    /// </summary>
    public class ImageSynthesisParameters : IImageSynthesisParameters
    {
        /// <inheritdoc />
        public string? Style { get; set; }

        /// <inheritdoc />
        public string? Size { get; set; }

        /// <inheritdoc />
        public int? N { get; set; }

        /// <inheritdoc />
        public uint? Seed { get; set; }

        /// <inheritdoc />
        public bool? PromptExtend { get; set; }

        /// <inheritdoc />
        public bool? Watermark { get; set; }
    }
}
