using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static class MultimodalEmbedding
    {
        private const string Image = "https://dashscope.oss-cn-beijing.aliyuncs.com/images/256_1.png";

        private const string Image2 =
            "https://img.alicdn.com/imgextra/i3/O1CN01rdstgY1uiZWt8gqSL_!!6000000006071-0-tps-1970-356.jpg";

        private const string Image3 =
            "https://img.alicdn.com/imgextra/i2/O1CN019eO00F1HDdlU4Syj5_!!6000000000724-2-tps-2476-1158.png";

        private const string Video =
            "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250107/lbcemt/new+video.mp4";

        private const string Instruct = "Given a user query, retrieve the most relevant document from the corpus";

        // the vectors are too large for the typed snapshot, their dimension is asserted by the test
        private static MultimodalEmbeddingItem Item(int index, string type) => new(index, Array.Empty<float>(), type);

        // one vector per content, without parameters
        public static readonly RequestSnapshot<ModelRequest<MultimodalEmbeddingInput, MultimodalEmbeddingParameters>,
            ModelResponse<MultimodalEmbeddingOutput, MultimodalEmbeddingUsage>> NoSse = new(
            "multimodal-embedding",
            new ModelRequest<MultimodalEmbeddingInput, MultimodalEmbeddingParameters>
            {
                Model = "tongyi-embedding-vision-plus",
                Input = new MultimodalEmbeddingInput
                {
                    Contents = new List<MultimodalEmbeddingContent>
                    {
                        "多模态向量模型",
                        new MultimodalEmbeddingContent(Image: Image2),
                        new MultimodalEmbeddingContent(Video: Video),
                        new MultimodalEmbeddingContent(MultiImages: new List<string> { Image3, Image })
                    }
                }
            },
            new ModelResponse<MultimodalEmbeddingOutput, MultimodalEmbeddingUsage>
            {
                Output = new MultimodalEmbeddingOutput(new List<MultimodalEmbeddingItem>
                {
                    Item(0, "text"),
                    Item(1, "image"),
                    Item(2, "video"),
                    Item(3, "multi_images")
                }),
                Usage = new MultimodalEmbeddingUsage(
                    335,
                    TotalTokens: 339,
                    OutputTokens: 4,
                    InputTokensDetails: new MultimodalEmbeddingInputTokenDetails(ImageTokens: 328, TextTokens: 7)),
                RequestId = "17b9444f-db5d-95d1-bcc6-6f8912aac7b8"
            });

        // the members of the first content are fused into one vector
        public static readonly RequestSnapshot<ModelRequest<MultimodalEmbeddingInput, MultimodalEmbeddingParameters>,
            ModelResponse<MultimodalEmbeddingOutput, MultimodalEmbeddingUsage>> FusedNoSse = new(
            "multimodal-embedding-fused",
            new ModelRequest<MultimodalEmbeddingInput, MultimodalEmbeddingParameters>
            {
                Model = "tongyi-embedding-vision-plus-2026-03-06",
                Input = new MultimodalEmbeddingInput
                {
                    Contents = new List<MultimodalEmbeddingContent>
                    {
                        new(Text: "这是1个视觉多模态表征模型", Image: Image, Video: Video),
                        "白色运动鞋，轻量透气，适合跑步和日常穿着"
                    }
                },
                Parameters = new MultimodalEmbeddingParameters { Dimension = 64, ResLevel = 2, MaxVideoFrames = 16 }
            },
            new ModelResponse<MultimodalEmbeddingOutput, MultimodalEmbeddingUsage>
            {
                Output = new MultimodalEmbeddingOutput(new List<MultimodalEmbeddingItem>
                {
                    Item(0, "fused"),
                    Item(1, "text")
                }),
                Usage = new MultimodalEmbeddingUsage(
                    1795,
                    TotalTokens: 1797,
                    OutputTokens: 2,
                    InputTokensDetails: new MultimodalEmbeddingInputTokenDetails(ImageTokens: 1732, TextTokens: 63)),
                RequestId = "b29e7d04-f294-985e-8bfa-cdc1a5b81038"
            });

        public static readonly RequestSnapshot<ModelRequest<MultimodalEmbeddingInput, MultimodalEmbeddingParameters>,
            ModelResponse<MultimodalEmbeddingOutput, MultimodalEmbeddingUsage>> FusionNoSse = new(
            "multimodal-embedding-fusion",
            new ModelRequest<MultimodalEmbeddingInput, MultimodalEmbeddingParameters>
            {
                Model = "qwen3-vl-embedding",
                Input = new MultimodalEmbeddingInput
                {
                    Contents = new List<MultimodalEmbeddingContent>
                    {
                        "商品描述文本",
                        new MultimodalEmbeddingContent(Image: Image),
                        new MultimodalEmbeddingContent(Image: Image2),
                        new MultimodalEmbeddingContent(Video: Video)
                    }
                },
                Parameters = new MultimodalEmbeddingParameters { EnableFusion = true }
            },
            new ModelResponse<MultimodalEmbeddingOutput, MultimodalEmbeddingUsage>
            {
                Output = new MultimodalEmbeddingOutput(new List<MultimodalEmbeddingItem> { Item(0, "fusion") }),
                Usage = new MultimodalEmbeddingUsage(43, TotalTokens: 2229, ImageTokens: 2186),
                RequestId = "039a0efd-9d75-9808-ba52-b162c2013937"
            });

        public static readonly RequestSnapshot<ModelRequest<MultimodalEmbeddingInput, MultimodalEmbeddingParameters>,
            ModelResponse<MultimodalEmbeddingOutput, MultimodalEmbeddingUsage>> VlNoSse = new(
            "multimodal-embedding-vl",
            new ModelRequest<MultimodalEmbeddingInput, MultimodalEmbeddingParameters>
            {
                Model = "qwen3-vl-embedding",
                Input = new MultimodalEmbeddingInput
                {
                    Contents = new List<MultimodalEmbeddingContent>
                    {
                        "多模态向量模型",
                        new MultimodalEmbeddingContent(Image: Image),
                        new MultimodalEmbeddingContent(Video: Video)
                    }
                },
                Parameters = new MultimodalEmbeddingParameters { Dimension = 256, Instruct = Instruct }
            },
            new ModelResponse<MultimodalEmbeddingOutput, MultimodalEmbeddingUsage>
            {
                Output = new MultimodalEmbeddingOutput(new List<MultimodalEmbeddingItem>
                {
                    Item(0, "vl"),
                    Item(1, "vl"),
                    Item(2, "vl")
                }),
                // the instruct is counted into the input tokens
                Usage = new MultimodalEmbeddingUsage(108, TotalTokens: 1612, ImageTokens: 1504),
                RequestId = "aac1b517-21e3-9224-a3b2-ab7e5a01e641"
            });

        // all the contents are fused into one vector, no total tokens in the usage
        public static readonly RequestSnapshot<ModelRequest<MultimodalEmbeddingInput, MultimodalEmbeddingParameters>,
            ModelResponse<MultimodalEmbeddingOutput, MultimodalEmbeddingUsage>> Qwen25VlNoSse = new(
            "multimodal-embedding-qwen25-vl",
            new ModelRequest<MultimodalEmbeddingInput, MultimodalEmbeddingParameters>
            {
                Model = "qwen2.5-vl-embedding",
                Input = new MultimodalEmbeddingInput
                {
                    Contents = new List<MultimodalEmbeddingContent>
                    {
                        "白色运动鞋，轻量透气，适合跑步和日常穿着",
                        new MultimodalEmbeddingContent(Image: Image)
                    }
                },
                Parameters = new MultimodalEmbeddingParameters { Dimension = 512 }
            },
            new ModelResponse<MultimodalEmbeddingOutput, MultimodalEmbeddingUsage>
            {
                Output = new MultimodalEmbeddingOutput(new List<MultimodalEmbeddingItem> { Item(0, "vl") }),
                Usage = new MultimodalEmbeddingUsage(35, ImageTokens: 81),
                RequestId = "8cc96f5b-9060-925a-9d70-9eab99ac58e5"
            });

        public static readonly RequestSnapshot<ModelRequest<MultimodalEmbeddingInput, MultimodalEmbeddingParameters>,
            ModelResponse<MultimodalEmbeddingOutput, MultimodalEmbeddingUsage>> V1NoSse = new(
            "multimodal-embedding-v1",
            new ModelRequest<MultimodalEmbeddingInput, MultimodalEmbeddingParameters>
            {
                Model = "multimodal-embedding-v1",
                Input = new MultimodalEmbeddingInput
                {
                    Contents = new List<MultimodalEmbeddingContent>
                    {
                        "多模态向量模型",
                        new MultimodalEmbeddingContent(Image: Image),
                        new MultimodalEmbeddingContent(Video: Video)
                    }
                },
                Parameters = new MultimodalEmbeddingParameters { OutputType = "dense", Fps = 0.5f }
            },
            new ModelResponse<MultimodalEmbeddingOutput, MultimodalEmbeddingUsage>
            {
                Output = new MultimodalEmbeddingOutput(new List<MultimodalEmbeddingItem>
                {
                    Item(0, "text"),
                    Item(1, "image"),
                    Item(2, "video")
                }),
                // the duration of the video comes as a float although the document says int
                Usage = new MultimodalEmbeddingUsage(9, ImageTokens: 896, ImageCount: 1, Duration: 1.2012012012012012),
                RequestId = "e93b14cb-c91f-990d-9f48-255099f443f2"
            });
    }
}
