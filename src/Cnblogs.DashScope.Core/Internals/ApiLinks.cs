namespace Cnblogs.DashScope.Core.Internals
{
    internal static class ApiLinks
    {
        public const string TextGeneration = "services/aigc/text-generation/generation";
        public const string MultimodalGeneration = "services/aigc/multimodal-generation/generation";
        public const string TextEmbedding = "services/embeddings/text-embedding/text-embedding";
        public const string ImageSynthesis = "services/aigc/text2image/image-synthesis";
        public const string ImageGeneration = "services/aigc/image-generation/generation";
        public const string BackgroundGeneration = "services/aigc/background-generation/generation/";
        public const string Tasks = "tasks/";
        public const string Uploads = "uploads/";
        public const string Tokenizer = "tokenizer";
        public static string Files(string? id = null) => string.IsNullOrWhiteSpace(id) ? "files" : $"files/{id}";
        public const string FilesCompatible = "/compatible-mode/v1/files";
        public static string Application(string applicationId) => $"apps/{applicationId}/completion";
    }
}
