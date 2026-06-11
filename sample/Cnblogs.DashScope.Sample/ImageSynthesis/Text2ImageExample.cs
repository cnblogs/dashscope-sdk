using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.ImageSynthesis;

public class Text2ImageExample : ImageSynthesisSample
{
    /// <inheritdoc />
    public override string Description => "Text to image sample";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        Console.Write("Input > ");
        var prompt = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(prompt))
        {
            prompt =
                "冬日北京的都市街景，青灰瓦顶、朱红色外墙的两间相邻中式商铺比肩而立，檐下悬挂印有剪纸马的暖光灯笼，在阴天漫射光中投下柔和光晕，映照湿润鹅卵石路面泛起细腻反光。左侧为书法店：靛蓝色老旧的牌匾上以遒劲行书刻着“文字渲染”。店门口的玻璃上挂着一幅字，自上而下，用田英章硬笔写着“专业幻灯片 中英文海报 高级信息图”，落款印章为“1k token”朱砂印。店内的墙上，可以模糊的辨认有三幅竖排的书法作品，第一幅写着“阿里巴巴”，第二幅写着“通义千问”，第三幅写着“图像生成”。一位白发苍苍的老人背对着镜头观赏。右侧为花店，牌匾上以鲜花做成文字“真实质感”；店内多层花架陈列红玫瑰、粉洋牡丹和绿植，门上贴了一个圆形花边标识，标识上写着“2k resolution”，门口摆放了一个彩色霓虹灯，上面写着“细腻刻画 人物 自然 建筑”。两家店中间堆放了一个雪人，举了一老式小黑板，上面用粉笔字写着“Qwen-Image-2.0 正式发布”。街道左侧，年轻情侣依偎在一起，女孩是瘦脸，身穿米白色羊绒大衣，肉色光腿神器。女孩举着心形透明气球，气球印有白色的字：“生图编辑二合一”。里面有一个毛茸茸的卡皮巴拉玩偶。男孩身着剪裁合体的深灰色呢子外套，内搭浅色高领毛衣。街道右侧，一个后背上写着“更小模型，更快速度”的骑手疾驰而过。整条街光影交织、动静相宜。";
            Console.WriteLine($"Using default prompt: {prompt}");
        }

        Console.WriteLine("Generating...");
        var response = await client.GetMultimodalGenerationAsync(
            new ModelRequest<MultimodalInput, IMultimodalParameters>()
            {
                Model = "qwen-image-2.0-pro",
                Input = new MultimodalInput()
                {
                    Messages = new List<MultimodalMessage>()
                    {
                        MultimodalMessage.User(
                            new List<MultimodalMessageContent>()
                            {
                                MultimodalMessageContent.TextContent(prompt)
                            })
                    }
                },
                Parameters = new MultimodalParameters()
                {
                    NegativePrompt = "低分辨率，低画质，肢体畸形，手指畸形，画面过饱和，蜡像感，人脸无细节，过度光滑，画面具有AI感。构图混乱。文字模糊，扭曲。",
                    N = 1,
                    PromptExtend = true,
                    Watermark = false,
                    Size = "2048*2048"
                }
            });
        var url = response.Output.Choices[0].Message.Content[0].Image;
        Console.WriteLine($"Generated image url: {url}");
    }
}
