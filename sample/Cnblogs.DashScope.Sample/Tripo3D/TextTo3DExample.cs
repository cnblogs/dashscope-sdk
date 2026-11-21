using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Tripo3D;

public class TextTo3DExample : Tripo3DSample
{
    /// <inheritdoc />
    public override string Description => "Text to 3D model using Tripo/Tripo-P1.0 and the task pattern";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        Console.Write("Input > ");
        var input = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(input))
        {
            input = "一只可爱的猫";
            Console.WriteLine($"Using the default input: {input}");
        }

        var response = await client.CreateTripo3DTaskAsync(
            new ModelRequest<Tripo3DInput, ITripo3DParameters>()
            {
                Model = "Tripo/Tripo-P1.0",
                Input = new Tripo3DInput { Prompt = input },
                Parameters = new Tripo3DParameters { TextureQuality = "standard" }
            });
        var task = await WaitTaskAsync(client, response.Output.TaskId);
        var result = task.Output.Results?.FirstOrDefault();
        Console.WriteLine($"Model url: {result?.ModelUrl}");
        Console.WriteLine($"PBR model url: {result?.PbrModelUrl}");
        Console.WriteLine($"Rendered image url: {result?.RenderedImageUrl}");
        Console.WriteLine("Urls are valid for 2 hours, download them in time.");
    }
}
