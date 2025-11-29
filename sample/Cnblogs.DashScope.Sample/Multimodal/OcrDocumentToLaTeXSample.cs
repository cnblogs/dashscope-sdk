using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Multimodal;

public class OcrDocumentToLaTeXSample : ISample
{
    /// <inheritdoc />
    public string Description => "OCR parsing scanned document to LaTeX sample";

    /// <inheritdoc />
    public async Task RunAsync(IDashScopeClient client)
    {
        // upload file
        await using var file = File.OpenRead("scanned.jpg");
        var ossLink = await client.UploadTemporaryFileAsync("qwen-vl-ocr-latest", file, "scanned.jpg");
        Console.WriteLine($"File uploaded: {ossLink}");
        var messages =
            new List<MultimodalMessage> { MultimodalMessage.User([MultimodalMessageContent.ImageContent(ossLink)]) };
        var completion = await client.GetMultimodalGenerationAsync(
            new ModelRequest<MultimodalInput, IMultimodalParameters>()
            {
                Model = "qwen-vl-ocr-latest",
                Input = new MultimodalInput { Messages = messages },
                Parameters = new MultimodalParameters()
                {
                    OcrOptions = new MultimodalOcrOptions()
                    {
                        Task = "document_parsing",
                    }
                }
            });

        Console.WriteLine("LaTeX:");
        Console.WriteLine(completion.Output.Choices[0].Message.Content[0].Text);

        if (completion.Usage != null)
        {
            var usage = completion.Usage;
            Console.WriteLine(
                $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/image({usage.ImageTokens})/total({usage.TotalTokens})");
        }
    }
}
