namespace Cnblogs.DashScope.Core.Internals;

internal static class ApiLinks
{
    public const string TextGeneration = "services/aigc/text-generation/generation";
    public const string MultimodalGeneration = "services/aigc/multimodal-generation/generation";
    public const string TextEmbedding = "services/embeddings/text-embedding/text-embedding";
    public const string Text2ImageSynthesis = "services/aigc/text2image/image-synthesis";
    public const string Image2ImageSynthesis = "services/aigc/image2image/image-synthesis";
    public const string VideoSynthesis = "services/aigc/video-generation/video-synthesis";
    public const string Image2VideoSynthesis = "services/aigc/image2video/video-synthesis";
    public const string FaceDetect = "services/aigc/image2video/face-detect";
    public const string FaceDetection = "services/vision/facedetection/detect";
    public const string FineTunes = "fine-tunes";
    public const string ImageGeneration = "services/aigc/image-generation/generation";
    public const string BackgroundGeneration = "services/aigc/background-generation/generation/";
    public const string Tasks = "tasks/";
    public const string Uploads = "uploads/";
    public const string Tokenizer = "tokenizer";
    public static string Files(string? id = null) => string.IsNullOrWhiteSpace(id) ? "files" : $"files/{id}";
    public const string FilesCompatible = "/compatible-mode/v1/files";
    public const string BatchesCompatible = "/compatible-mode/v1/batches";
    public static string Application(string applicationId) => $"apps/{applicationId}/completion";
}
