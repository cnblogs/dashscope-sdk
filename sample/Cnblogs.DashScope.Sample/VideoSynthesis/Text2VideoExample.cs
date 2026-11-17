using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.VideoSynthesis;

public class Text2VideoExample : VideoSynthesisSample
{
    /// <inheritdoc />
    public override string Description => "Text to video using wan2.7-t2v and the task pattern";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        Console.Write("Input > ");
        var input = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(input))
        {
            input = "一只小猫在月光下的屋顶上奔跑，城市的霓虹灯在远处闪烁，电影级画质，流畅运镜。";
            Console.WriteLine($"Using the default input: {input}");
        }

        var response = await client.CreateVideoSynthesisTaskAsync(
            new ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>()
            {
                Model = "wan2.7-t2v",
                Input = new VideoSynthesisInput()
                {
                    Prompt = input,
                    NegativePrompt = "低分辨率、错误、最差质量、低质量、残缺、多余的手指、比例不良"
                },
                Parameters = new VideoSynthesisParameters()
                {
                    Resolution = "720P",
                    Ratio = "16:9",
                    Duration = 5,
                    PromptExtend = true,
                    Watermark = false,
                    Seed = 42
                }
            });

        var taskId = response.Output.TaskId;
        Console.WriteLine($"Task Created: {taskId}");
        var waited = 0;
        var timeout = 600;
        do
        {
            var task = await client.GetVideoSynthesisTaskAsync(taskId);
            if (task.Output.TaskStatus == DashScopeTaskStatus.Succeeded)
            {
                Console.WriteLine($"Task finished, video URL: {task.Output.VideoUrl}");
                Console.WriteLine($"Usage: duration {task.Usage?.Duration}s, SR {task.Usage?.SR}, ratio {task.Usage?.Ratio}");
                break;
            }

            if (task.Output.TaskStatus == DashScopeTaskStatus.Failed)
            {
                Console.WriteLine($"Task failed, error message: {task.Output.Message}");
                break;
            }

            Console.WriteLine($"Task Status: {task.Output.TaskStatus}");
            await Task.Delay(TimeSpan.FromSeconds(15));
            waited += 15;
        } while (waited < timeout);
    }
}
