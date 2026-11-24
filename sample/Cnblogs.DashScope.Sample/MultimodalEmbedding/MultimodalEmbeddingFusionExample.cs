using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.MultimodalEmbedding;

public class MultimodalEmbeddingFusionExample : MultimodalEmbeddingSample
{
    /// <inheritdoc />
    public override string Description =>
        "Fuse a text and an image into one vector, then match it by a text and by an image (qwen3-vl-embedding)";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        Console.WriteLine("Embedding...");
        const string model = "qwen3-vl-embedding";
        const string image = "https://dashscope.oss-cn-beijing.aliyuncs.com/images/256_1.png";

        // an item made of a text and an image, e.g. a product with its description: EnableFusion
        // fuses all the contents into one vector instead of one vector per content
        var item = await client.GetMultimodalEmbeddingsAsync(
            new ModelRequest<MultimodalEmbeddingInput, MultimodalEmbeddingParameters>
            {
                Model = model,
                Input = new MultimodalEmbeddingInput
                {
                    Contents = ["一只绵羊站在绿色老爷车旁边", new MultimodalEmbeddingContent(Image: image)]
                },
                Parameters = new MultimodalEmbeddingParameters { EnableFusion = true }
            });
        var fused = item.Output.Embeddings.Single();
        Console.WriteLine($"The fused vector: type = {fused.Type}, dimension = {fused.Embedding.Length}");

        // the queries keep one vector each: the fused vector is close to the image and to a text
        // describing the item, and far from an unrelated text
        string[] texts = ["一只羊站在汽车边上", "壁炉前放着两个杯子"];
        var query = await client.GetMultimodalEmbeddingsAsync(
            new ModelRequest<MultimodalEmbeddingInput, MultimodalEmbeddingParameters>
            {
                Model = model,
                Input = new MultimodalEmbeddingInput
                {
                    Contents =
                    [
                        new MultimodalEmbeddingContent(Image: image),
                        texts[0],
                        texts[1]
                    ]
                }
            });

        Console.WriteLine($"{Cosine(fused.Embedding, query.Output.Embeddings[0].Embedding):F4}\timage\t{image}");
        for (var i = 0; i < texts.Length; i++)
        {
            Console.WriteLine($"{Cosine(fused.Embedding, query.Output.Embeddings[i + 1].Embedding):F4}\ttext\t{texts[i]}");
        }

        Console.WriteLine($"Token usage: {item.Usage?.TotalTokens} + {query.Usage?.TotalTokens}");
    }
}
