using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static class TextEmbedding
    {
        public static readonly RequestSnapshot<ModelRequest<TextEmbeddingInput, ITextEmbeddingParameters>,
            ModelResponse<TextEmbeddingOutput, TextEmbeddingTokenUsage>> NoSse = new(
            "text-embedding",
            new ModelRequest<TextEmbeddingInput, ITextEmbeddingParameters>
            {
                Input = new TextEmbeddingInput { Texts = new List<string> { "代码改变世界" }.AsReadOnly() },
                Model = "text-embedding-v2",
                Parameters = new TextEmbeddingParameters { TextType = "query" }
            },
            new ModelResponse<TextEmbeddingOutput, TextEmbeddingTokenUsage>
            {
                Output = new TextEmbeddingOutput(new List<TextEmbeddingItem> { new(0, Array.Empty<float>(), null) }),
                RequestId = "1773f7b2-2148-9f74-b335-b413e398a116",
                Usage = new TextEmbeddingTokenUsage(3)
            });

        public static readonly RequestSnapshot<ModelRequest<TextEmbeddingInput, ITextEmbeddingParameters>,
            ModelResponse<TextEmbeddingOutput, TextEmbeddingTokenUsage>> EmbeddingClientNoSse = new(
            "text-embedding",
            new ModelRequest<TextEmbeddingInput, ITextEmbeddingParameters>
            {
                Input = new TextEmbeddingInput { Texts = new List<string> { "代码改变世界" }.AsReadOnly() },
                Model = "text-embedding-v3",
                Parameters = new TextEmbeddingParameters { Dimension = 1024 }
            },
            new ModelResponse<TextEmbeddingOutput, TextEmbeddingTokenUsage>
            {
                Output = new TextEmbeddingOutput(new List<TextEmbeddingItem> { new(0, Array.Empty<float>(), null) }),
                RequestId = "1773f7b2-2148-9f74-b335-b413e398a116",
                Usage = new TextEmbeddingTokenUsage(3)
            });

        public static readonly
            RequestSnapshot<ModelRequest<BatchGetEmbeddingsInput, IBatchGetEmbeddingsParameters>,
                ModelResponse<BatchGetEmbeddingsOutput, TextEmbeddingTokenUsage>> BatchNoSse = new(
                "batch-text-embedding",
                new ModelRequest<BatchGetEmbeddingsInput, IBatchGetEmbeddingsParameters>
                {
                    Input = new BatchGetEmbeddingsInput
                    {
                        Url =
                            "https://modelscope.oss-cn-beijing.aliyuncs.com/resource/text_embedding_file.txt"
                    },
                    Model = "text-embedding-async-v2",
                    Parameters = new BatchGetEmbeddingsParameters { TextType = "query" }
                },
                new ModelResponse<BatchGetEmbeddingsOutput, TextEmbeddingTokenUsage>
                {
                    RequestId = "db5ce040-4548-9919-9a75-3385ee152335",
                    Output = new BatchGetEmbeddingsOutput
                    {
                        TaskId = "6075262c-b56d-4968-9abf-2a9784a90f3e",
                        TaskStatus = DashScopeTaskStatus.Pending
                    }
                });

        public static readonly RequestSnapshot<ModelRequest<TextEmbeddingInput, ITextEmbeddingParameters>,
            ModelResponse<TextEmbeddingOutput, TextEmbeddingTokenUsage>> QwenNoSse = new(
            "text-embedding-qwen37",
            new ModelRequest<TextEmbeddingInput, ITextEmbeddingParameters>
            {
                Input = new TextEmbeddingInput { Texts = new List<string> { "代码改变世界" }.AsReadOnly() },
                Model = "qwen3.7-text-embedding",
                Parameters = new TextEmbeddingParameters { TextType = "query", Dimension = 2560 }
            },
            new ModelResponse<TextEmbeddingOutput, TextEmbeddingTokenUsage>
            {
                Output = new TextEmbeddingOutput(new List<TextEmbeddingItem> { new(0, Array.Empty<float>(), null) }),
                RequestId = "c9324591-f147-9444-824b-57494dfcc6e9",
                Usage = new TextEmbeddingTokenUsage(19, InputTokens: 19)
            });

        public static readonly RequestSnapshot<ModelRequest<TextEmbeddingInput, ITextEmbeddingParameters>,
            ModelResponse<TextEmbeddingOutput, TextEmbeddingTokenUsage>> QwenFlashNoSse = new(
            "text-embedding-qwen37-flash",
            new ModelRequest<TextEmbeddingInput, ITextEmbeddingParameters>
            {
                Input =
                    new TextEmbeddingInput
                    {
                        Texts = new List<string> { "代码改变世界", "科技创造未来" }.AsReadOnly()
                    },
                Model = "qwen3.7-text-embedding-flash",
                Parameters = new TextEmbeddingParameters { TextType = "document" }
            },
            new ModelResponse<TextEmbeddingOutput, TextEmbeddingTokenUsage>
            {
                Output = new TextEmbeddingOutput(
                    new List<TextEmbeddingItem> { new(0, Array.Empty<float>(), null), new(1, Array.Empty<float>(), null) }),
                RequestId = "d40c2eb5-430a-96ff-8c4c-cb8ce2360e86",
                Usage = new TextEmbeddingTokenUsage(
                    38,
                    PromptTokens: 38,
                    PromptTokensDetails: new TextEmbeddingPromptTokenDetails(ImageTokens: 0, TextTokens: 38))
            });
    }
}
