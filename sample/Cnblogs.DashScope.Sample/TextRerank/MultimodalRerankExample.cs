using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.TextRerank;

public class MultimodalRerankExample : TextRerankSample
{
    /// <inheritdoc />
    public override string Description => "Rank text, image and video documents by an image query (qwen3-vl-rerank)";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        Console.WriteLine("Reranking...");
        const string image = "https://img.alicdn.com/imgextra/i3/O1CN01rdstgY1uiZWt8gqSL_!!6000000006071-0-tps-1970-356.jpg";

        var response = await client.RerankDocumentsAsync(
            new ModelRequest<TextRerankInput, TextRerankParameters>
            {
                Model = "qwen3-vl-rerank",
                Input = new TextRerankInput
                {
                    // an image to search by, a text query works as well
                    Query = new TextRerankContent(Image: image),
                    Documents =
                    [
                        "文本排序模型广泛用于搜索引擎和推荐系统中，它们根据文本相关性对候选文本进行排序",
                        new TextRerankContent(Image: image),
                        new TextRerankContent(
                            Video: "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250107/lbcemt/new+video.mp4")
                    ]
                },
                Parameters = new TextRerankParameters
                {
                    // the documents come back in the results, otherwise only the indexes do
                    ReturnDocuments = true,
                    Fps = 0.5f // the ratio of the video frames to read
                }
            });

        foreach (var result in response.Output.Results)
        {
            // the echoed document keeps its modality: text, image or video
            var document = result.Document;
            Console.WriteLine($"{result.RelevanceScore:F4}\t{document?.Text ?? document?.Image ?? document?.Video}");
        }

        Console.WriteLine($"Token usage: {response.Usage?.TotalTokens} (image: {response.Usage?.ImageTokens})");
    }
}
