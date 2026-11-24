using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.TextRerank;

public class TextRerankExample : TextRerankSample
{
    /// <inheritdoc />
    public override string Description => "Rank text documents by their relevance to a query (qwen3-rerank)";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        Console.WriteLine("Reranking...");
        // the chunks recalled from the knowledge base go here, the result tells which
        // of them are worth passing to the model
        string[] chunks =
        [
            "文本排序模型广泛用于搜索引擎和推荐系统中，它们根据文本相关性对候选文本进行排序",
            "量子计算是计算科学的一个前沿领域",
            "预训练语言模型的发展给文本排序模型带来了新的进展"
        ];

        var response = await client.RerankDocumentsAsync(
            new ModelRequest<TextRerankInput, TextRerankParameters>
            {
                Model = "qwen3-rerank",
                Input = new TextRerankInput
                {
                    Query = "什么是文本排序模型",
                    // a text converts to TextRerankContent implicitly, an image or a video
                    // is created with its url
                    Documents = chunks.Select(c => (TextRerankContent)c).ToList()
                },
                Parameters = new TextRerankParameters
                {
                    TopN = 2, // all documents when unset
                    Instruct = "Given a web search query, retrieve relevant passages that answer the query."
                }
            });

        // the results are sorted by the score descending, Index points to the input chunks
        foreach (var result in response.Output.Results)
        {
            Console.WriteLine($"{result.RelevanceScore:F4}\t{chunks[result.Index]}");
        }

        Console.WriteLine($"Token usage: {response.Usage?.TotalTokens}");
    }
}
