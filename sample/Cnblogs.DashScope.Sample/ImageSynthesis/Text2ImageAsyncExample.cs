using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.ImageSynthesis;

public class Text2ImageAsyncExample : ImageSynthesisSample
{
    /// <inheritdoc />
    public override string Description => "Text to image using Task pattern";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        Console.Write("Input > ");
        var input = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(input))
        {
            input =
                "一副典雅庄重的对联悬挂于厅堂之中，房间是个安静古典的中式布置，桌子上放着一些青花瓷，对联上左书“义本生知人机同道善思新”，右书“通云赋智乾坤启数高志远”， 横批“智启千问”，字体飘逸，在中间挂着一幅中国风的画作，内容是岳阳楼。";
            Console.WriteLine($"Using the default input: {input}");
        }

        var response = await client.CreateImageSynthesisTaskAsync(
            new ModelRequest<ImageSynthesisInput, IImageSynthesisParameters>()
            {
                Model = "qwen-image-plus",
                Input = new ImageSynthesisInput() { Prompt = input },
                Parameters = new ImageSynthesisParameters()
                {
                    Size = "1664*928",
                    N = 1,
                    PromptExtend = true,
                    Watermark = false
                }
            });

        var taskId = response.Output.TaskId;
        Console.WriteLine($"Task Created: {taskId}");
        var waited = 0;
        var timeout = 300;
        do
        {
            var task = await client.GetImageSynthesisTaskAsync(taskId);
            if (task.Output.TaskStatus == DashScopeTaskStatus.Succeeded)
            {
                Console.WriteLine($"Task finished, image URL: {task.Output.Results?.FirstOrDefault()?.Url}");
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
