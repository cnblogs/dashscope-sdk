using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.UniTranslation;

public class UniTranslationExample : UniTranslationSample
{
    /// <inheritdoc />
    public override string Description => "Translate texts and an image synchronously (qwen-mt-uni)";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        Console.WriteLine("Translating texts...");
        string[] sourceTexts = ["机器学习正在改变世界。", "全场9折", "这款商品支持七天无理由退换。"];
        var texts = await client.GetUniTranslationAsync(
            new ModelRequest<UniTranslationInput>
            {
                Model = "qwen-mt-uni",
                Input = new UniTranslationInput
                {
                    SourceTexts = sourceTexts.ToList(),
                    SourceLang = "zh",
                    TargetLang = "en",
                    Ext = new UniTranslationInputExt
                    {
                        DomainHint = "These sentences are from seller-buyer conversations on a B2C ecommerce platform.",
                        // a source text equal to a sensitive one is kept as it is
                        Sensitives = ["全场9折"],
                        // a hint of the terms, the model is guided rather than forced to use it
                        Glossary = [new UniTranslationGlossaryTerm("机器学习", "ML")]
                    }
                }
            });

        // the translated texts follow the order of the source texts
        foreach (var (source, translated) in sourceTexts.Zip(texts.Output.Data!.TranslatedTexts!))
        {
            Console.WriteLine($"{source} => {translated}");
        }

        Console.WriteLine("Translating an image...");
        var image = await client.GetUniTranslationAsync(
            new ModelRequest<UniTranslationInput>
            {
                Model = "qwen-mt-uni",
                Input = new UniTranslationInput
                {
                    FileUrl = "https://maas-infra-public.oss-cn-hangzhou.aliyuncs.com/lzhe/test/03bc3a6a-a02b-11f1-8874-00163e79676e.jpg",
                    TargetLang = "zh"
                }
            });

        Console.WriteLine($"Translated image (valid for 24 hours): {image.Output.Data!.TranslatedFileUrl}");
        Console.WriteLine($"Token usage: {texts.Usage?.TotalTokens} + {image.Usage?.TotalTokens}");
    }
}
