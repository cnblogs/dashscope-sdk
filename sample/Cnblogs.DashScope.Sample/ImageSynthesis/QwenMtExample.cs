using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.ImageSynthesis;

public class QwenMtExample : ImageSynthesisSample
{
    /// <inheritdoc />
    public override string Description => "Image translation with qwen-MT-image models";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        var response = await client.CreateImageSynthesisTaskAsync(
            new ModelRequest<Image2ImageSynthesisInput>()
            {
                Model = "qwen-mt-image",
                Input = new Image2ImageSynthesisInput()
                {
                    SourceLang = "zh",
                    TargetLang = "en",
                    ImageUrl = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250916/ordhsk/1.webp",
                    Ext = new Image2ImageSynthesisInputExt()
                    {
                        DomainHint = "These sentences are from seller-buyer conversations on a B2C ecommerce platform. Translate them into clear, engaging customer service language, ensuring the translation is appropriate for handling potential issues or disputes.",
                        Sensitives = ["基础"],
                        Terminologies = [new Image2ImageSynthesisInputExtTerm("一体化", "Unified")],
                        Config = new Image2ImageSynthesisConfig()
                        {
                            ImageSegment = false
                        }
                    }
                }
            });

        var taskId = response.Output.TaskId;
        Console.WriteLine($"Task Created: {taskId}");
        var waited = 0;
        var timeout = 300;
        do
        {
            var task = await client.GetImage2ImageSynthesisTaskAsync(taskId);
            if (task.Output.TaskStatus == DashScopeTaskStatus.Succeeded)
            {
                Console.WriteLine($"Task finished, image URL: {task.Output.ImageUrl}");
                break;
            }

            if (task.Output.TaskStatus == DashScopeTaskStatus.Failed)
            {
                Console.WriteLine($"Task failed, error message: {task.Output.Message}");
                break;
            }

            Console.WriteLine($"Task Status: {task.Output.TaskStatus}");
            await Task.Delay(TimeSpan.FromSeconds(10));
            waited += 10;
        } while (waited < timeout);
    }
}
