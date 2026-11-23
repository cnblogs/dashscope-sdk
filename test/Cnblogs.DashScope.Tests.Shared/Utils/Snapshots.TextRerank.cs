using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static class TextRerank
    {
        private const string Query = "什么是文本排序模型";
        private const string TextDocument =
            "文本排序模型广泛用于搜索引擎和推荐系统中，它们根据文本相关性对候选文本进行排序";

        private const string Image =
            "https://img.alicdn.com/imgextra/i3/O1CN01rdstgY1uiZWt8gqSL_!!6000000006071-0-tps-1970-356.jpg";

        private const string Video =
            "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250107/lbcemt/new+video.mp4";

        private static List<TextRerankContent> TextDocuments
            => new()
            {
                TextDocument,
                "量子计算是计算科学的一个前沿领域",
                "预训练语言模型的发展给文本排序模型带来了新的进展"
            };

        private static List<TextRerankContent> MultimodalDocuments
            => new()
            {
                new TextRerankContent(Text: TextDocument),
                new TextRerankContent(Image: Image),
                new TextRerankContent(Video: Video)
            };

        public static readonly RequestSnapshot<ModelRequest<TextRerankInput, TextRerankParameters>,
            ModelResponse<TextRerankOutput, TextRerankUsage>> NoSse = new(
            "text-rerank",
            new ModelRequest<TextRerankInput, TextRerankParameters>
            {
                Model = "qwen3-rerank",
                Input = new TextRerankInput { Query = Query, Documents = TextDocuments },
                Parameters = new TextRerankParameters
                {
                    TopN = 2,
                    ReturnDocuments = true,
                    Instruct = "Given a web search query, retrieve relevant passages that answer the query."
                }
            },
            new ModelResponse<TextRerankOutput, TextRerankUsage>
            {
                Output = new TextRerankOutput(new List<TextRerankResult>
                {
                    new(0, 0.9224626595345482, new TextRerankContent(Text: TextDocument)),
                    new(
                        2,
                        0.7439220489243172,
                        new TextRerankContent(Text: "预训练语言模型的发展给文本排序模型带来了新的进展"))
                }),
                Usage = new TextRerankUsage(105),
                RequestId = "c0c5eabc-e7ed-9058-b9ed-1a56d5d7e6b8"
            });

        // without parameters: every document is ranked and the documents are not echoed
        public static readonly RequestSnapshot<ModelRequest<TextRerankInput, TextRerankParameters>,
            ModelResponse<TextRerankOutput, TextRerankUsage>> Qwen37NoSse = new(
            "text-rerank-qwen37",
            new ModelRequest<TextRerankInput, TextRerankParameters>
            {
                Model = "qwen3.7-text-rerank",
                Input = new TextRerankInput { Query = Query, Documents = TextDocuments }
            },
            new ModelResponse<TextRerankOutput, TextRerankUsage>
            {
                Output = new TextRerankOutput(new List<TextRerankResult>
                {
                    new(0, 0.9949247716615863),
                    new(2, 0.7894754974541365),
                    new(1, 0.06368886700446985)
                }),
                Usage = new TextRerankUsage(95, PromptTokens: 95),
                RequestId = "e4496020-8420-9917-8be9-9c84b4c4b1c7"
            });

        public static readonly RequestSnapshot<ModelRequest<TextRerankInput, TextRerankParameters>,
            ModelResponse<TextRerankOutput, TextRerankUsage>> VlNoSse = new(
            "text-rerank-vl",
            new ModelRequest<TextRerankInput, TextRerankParameters>
            {
                Model = "qwen3-vl-rerank",
                Input = new TextRerankInput { Query = Query, Documents = MultimodalDocuments },
                Parameters = new TextRerankParameters { ReturnDocuments = true, Fps = 0.5f }
            },
            new ModelResponse<TextRerankOutput, TextRerankUsage>
            {
                Output = new TextRerankOutput(new List<TextRerankResult>
                {
                    new(0, 0.9267586886781294, new TextRerankContent(Text: TextDocument)),
                    new(1, 0.6261350464215385, new TextRerankContent(Image: Image)),
                    new(2, 0.3580369167116609, new TextRerankContent(Video: Video))
                }),
                Usage = new TextRerankUsage(2374, ImageTokens: 2122, InputTokens: 252),
                RequestId = "6caf5cd9-aae7-91f3-9948-36494233137c"
            });

        public static readonly RequestSnapshot<ModelRequest<TextRerankInput, TextRerankParameters>,
            ModelResponse<TextRerankOutput, TextRerankUsage>> VlImageQueryNoSse = new(
            "text-rerank-vl-image-query",
            new ModelRequest<TextRerankInput, TextRerankParameters>
            {
                Model = "qwen3-vl-rerank",
                Input = new TextRerankInput
                {
                    Query = new TextRerankContent(Image: Image),
                    Documents = MultimodalDocuments
                },
                Parameters = new TextRerankParameters { ReturnDocuments = true, TopN = 2 }
            },
            new ModelResponse<TextRerankOutput, TextRerankUsage>
            {
                Output = new TextRerankOutput(new List<TextRerankResult>
                {
                    new(1, 0.8547757168004047, new TextRerankContent(Image: Image)),
                    new(0, 0.5250293974646012, new TextRerankContent(Text: TextDocument))
                }),
                Usage = new TextRerankUsage(4414, ImageTokens: 4168, InputTokens: 246),
                RequestId = "58921e62-96fc-953f-a9fd-282468c81d4e"
            });
    }
}
