using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.MultimodalEmbedding;

public class MultimodalEmbeddingExample : MultimodalEmbeddingSample
{
    /// <inheritdoc />
    public override string Description =>
        "Search images and a video by texts, all the modalities share one vector space (tongyi-embedding-vision-plus)";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        Console.WriteLine("Embedding...");
        const string model = "tongyi-embedding-vision-plus";

        // the candidates to search in: an image (a public url or a base64 data uri) and a video
        // (a public url) each take a content of their own, index them once
        List<MultimodalEmbeddingContent> candidates =
        [
            new(Image: "https://dashscope.oss-cn-beijing.aliyuncs.com/images/256_1.png"),
            new(Image: "https://img.alicdn.com/imgextra/i3/O1CN01rdstgY1uiZWt8gqSL_!!6000000006071-0-tps-1970-356.jpg"),
            new(Video: "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250107/lbcemt/new+video.mp4")
        ];
        var index = await client.GetMultimodalEmbeddingsAsync(
            new ModelRequest<MultimodalEmbeddingInput, MultimodalEmbeddingParameters>
            {
                Model = model,
                Input = new MultimodalEmbeddingInput { Contents = candidates }
            });

        // the queries go in as texts here, an image or a video searches the same way
        string[] queries = ["壁炉前放着两个杯子和一条毛毯", "一只绵羊站在绿色老爷车旁边"];
        var query = await client.GetMultimodalEmbeddingsAsync(
            new ModelRequest<MultimodalEmbeddingInput, MultimodalEmbeddingParameters>
            {
                Model = model,
                // a text converts to MultimodalEmbeddingContent implicitly
                Input = new MultimodalEmbeddingInput { Contents = queries.Select(q => (MultimodalEmbeddingContent)q).ToList() }
            });

        // one embedding per content, at the index of the content whichever modality it is
        foreach (var (text, embedding) in queries.Zip(query.Output.Embeddings))
        {
            Console.WriteLine($"Query: {text}");
            var hits = index.Output.Embeddings
                .Select((candidate, i) => (
                    Candidate: candidates[i],
                    Score: Cosine(embedding.Embedding, candidate.Embedding)))
                .OrderByDescending(hit => hit.Score);
            foreach (var hit in hits)
            {
                Console.WriteLine($"{hit.Score:F4}\t{hit.Candidate.Image ?? hit.Candidate.Video}");
            }
        }

        Console.WriteLine($"Token usage: {index.Usage?.TotalTokens} + {query.Usage?.TotalTokens}");
    }
}
