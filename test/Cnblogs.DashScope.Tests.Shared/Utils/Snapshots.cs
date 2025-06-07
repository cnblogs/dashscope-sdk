using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static class Tokenization
    {
        public static readonly
            RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                ModelResponse<TokenizationOutput, TokenizationUsage>> TokenizeNoSse = new(
                "tokenization",
                new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                {
                    Input = new TextGenerationInput
                    {
                        Messages =
                        new List<TextChatMessage> { TextChatMessage.User("代码改变世界") }.AsReadOnly()
                    },
                    Model = "qwen-max",
                    Parameters = new TextGenerationParameters { Seed = 1234 }
                },
                new ModelResponse<TokenizationOutput, TokenizationUsage>
                {
                    Output = new TokenizationOutput(
                        new List<int>
                        {
                            46100,
                            101933,
                            99489
                        },
                        new List<string>
                        {
                            "代码",
                            "改变",
                            "世界"
                        }),
                    Usage = new TokenizationUsage(3),
                    RequestId = "6615ba01-081d-9147-93ff-7bd26f3adf93"
                });
    }

    public static class ImageSynthesis
    {
        public static readonly
            RequestSnapshot<ModelRequest<ImageSynthesisInput, IImageSynthesisParameters>,
                ModelResponse<ImageSynthesisOutput, ImageSynthesisUsage>> CreateTask = new(
                "image-synthesis",
                new ModelRequest<ImageSynthesisInput, IImageSynthesisParameters>
                {
                    Model = "wanx-v1",
                    Input = new ImageSynthesisInput { Prompt = "一只奔跑的猫" },
                    Parameters = new ImageSynthesisParameters
                    {
                        N = 4,
                        Seed = 42,
                        Size = "1024*1024",
                        Style = "<sketch>"
                    }
                },
                new ModelResponse<ImageSynthesisOutput, ImageSynthesisUsage>
                {
                    Output = new ImageSynthesisOutput
                    {
                        TaskId = "9e2b6ef6-285d-4efa-8651-4dbda7d571fa",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "33da8e6b-1309-9a44-be83-352165959608"
                });
    }

    public static class ImageGeneration
    {
        public static readonly
            RequestSnapshot<ModelRequest<ImageGenerationInput>,
                ModelResponse<ImageGenerationOutput, ImageGenerationUsage>> CreateTaskNoSse = new(
                "image-generation",
                new ModelRequest<ImageGenerationInput>
                {
                    Model = "wanx-style-repaint-v1",
                    Input = new ImageGenerationInput
                    {
                        ImageUrl =
                            "https://public-vigen-video.oss-cn-shanghai.aliyuncs.com/public/dashscope/test.png",
                        StyleIndex = 3
                    }
                },
                new ModelResponse<ImageGenerationOutput, ImageGenerationUsage>
                {
                    Output = new ImageGenerationOutput
                    {
                        TaskId = "c4f94e00-5899-431b-9579-eb1ebe686379", TaskStatus = DashScopeTaskStatus.Pending,
                    },
                    RequestId = "565ff453-bcf7-99ec-9fbe-b99bb8caab07"
                });
    }

    public static class BackgroundGeneration
    {
        public static readonly
            RequestSnapshot<ModelRequest<BackgroundGenerationInput, IBackgroundGenerationParameters>,
                ModelResponse<BackgroundGenerationOutput, BackgroundGenerationUsage>> CreateTaskNoSse = new(
                "background-generation",
                new ModelRequest<BackgroundGenerationInput, IBackgroundGenerationParameters>
                {
                    Input = new BackgroundGenerationInput
                    {
                        BaseImageUrl =
                            "http://inner-materials.oss-cn-beijing.aliyuncs.com/graphic_design/jianguo/lllcho.lc/test_data/demo_example/demo/hailuo_2236873898_2.png",
                        RefImageUrl =
                            "http://inner-materials.oss-cn-beijing.aliyuncs.com/graphic_design/jianguo/lllcho.lc/test_data/demo_example/demo/d1faf4f26c8c4ea798d043a8bf3784bb_2.png",
                        RefPrompt = "远处有朝阳升起",
                        Title = "分享好时光",
                        SubTitle = "只为不一样的你",
                        NegRefPrompt = "低质量的，模糊的，错误的"
                    },
                    Model = "wanx-background-generation-v2",
                    Parameters = new BackgroundGenerationParameters
                    {
                        N = 2,
                        NoiseLevel = 300,
                        RefPromptWeight = 0.5f
                    }
                },
                new ModelResponse<BackgroundGenerationOutput, BackgroundGenerationUsage>
                {
                    RequestId = "a010df33-effc-9e32-aaa0-e55fffa40ef5",
                    Output = new BackgroundGenerationOutput
                    {
                        TaskId = "b2e98d78-c79b-431c-b2d7-c7bcd54465da",
                        TaskStatus = DashScopeTaskStatus.Pending
                    }
                });
    }

    public static class File
    {
        public static readonly FileInfo TestFile = new FileInfo("RawHttpData/test1.txt");

        public static readonly RequestSnapshot<DashScopeFile> UploadFileNoSse = new(
            "upload-file",
            new DashScopeFile("file-fe-qBKjZKfTx64R9oYmwyovNHBH", "file", 6, 1720582024, "test1.txt", "file-extract"));

        public static readonly RequestSnapshot<DashScopeFile> GetFileNoSse = new(
            "get-file",
            new DashScopeFile("file-fe-qBKjZKfTx64R9oYmwyovNHBH", "file", 6, 1720582024, "test1.txt", "file-extract"));

        public static readonly RequestSnapshot<DashScopeFileList> ListFileNoSse = new(
            "list-files",
            new DashScopeFileList(
                "list",
                false,
                new List<DashScopeFile>
                {
                    new DashScopeFile(
                        "file-fe-qBKjZKfTx64R9oYmwyovNHBH",
                        "file",
                        6,
                        1720582024,
                        "test1.txt",
                        "file-extract"),
                    new DashScopeFile(
                        "file-fe-WTTG89tIUTd4ByqP3K48R3bn",
                        "file",
                        6,
                        1720535665,
                        "test1.txt",
                        "file-extract")
                }));

        public static readonly RequestSnapshot<DashScopeDeleteFileResult> DeleteFileNoSse = new(
            "delete-file",
            new DashScopeDeleteFileResult("file", true, "file-fe-qBKjZKfTx64R9oYmwyovNHBH"));
    }
}
