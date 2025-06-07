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
                Output = new TextEmbeddingOutput(new List<TextEmbeddingItem> { new TextEmbeddingItem(0, new float[0]) }),
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
                Output = new TextEmbeddingOutput(new List<TextEmbeddingItem> { new TextEmbeddingItem(0, new float[0]) }),
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
    }
}
