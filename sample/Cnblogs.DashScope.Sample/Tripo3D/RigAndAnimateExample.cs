using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Tripo3D;

public class RigAndAnimateExample : Tripo3DSample
{
    /// <inheritdoc />
    public override string Description =>
        "Rig a 3D model and retarget a preset animation onto it (Tripo-RigCheck + Tripo-AutoRig + Tripo-AnimationRetarget)";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        // A public GLB file as the model to rig, replace it with your own model url
        // or the task id of a preceding Tripo generation task.
        const string modelUrl =
            "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260507/ffaaxt/tripo-text-to-3d-result.glb";

        // Step 1: check whether the model is riggable and which skeleton type fits (free of charge).
        var checkResponse = await client.CreateTripo3DTaskAsync(
            new ModelRequest<Tripo3DInput, ITripo3DParameters>()
            {
                Model = "Tripo/Tripo-RigCheck",
                Input = new Tripo3DInput { ModelUrl = modelUrl }
            });
        var check = await WaitTaskAsync(client, checkResponse.Output.TaskId);
        var checkResult = check.Output.Results?.FirstOrDefault();
        Console.WriteLine($"Riggable: {checkResult?.Riggable}, recommended rig type: {checkResult?.RigType}");
        if (checkResult?.Riggable == false)
        {
            Console.WriteLine("The model is not riggable, try another model.");
            return;
        }

        // Step 2: rig the model with the recommended skeleton type.
        var rigResponse = await client.CreateTripo3DTaskAsync(
            new ModelRequest<Tripo3DInput, ITripo3DParameters>()
            {
                Model = "Tripo/Tripo-AutoRig",
                Input = new Tripo3DInput { ModelUrl = modelUrl },
                Parameters = new Tripo3DParameters
                {
                    RigType = checkResult!.RigType,
                    OutFormat = "glb"
                }
            });
        var rig = await WaitTaskAsync(client, rigResponse.Output.TaskId);
        Console.WriteLine($"Rigged model url: {rig.Output.Results?.FirstOrDefault()?.ModelUrl}");

        // Step 3: retarget a preset animation onto the rigged model.
        // Non-biped rigs require the preset:<rigType>:<name> form, see the animation
        // list in the Bailian documentation for available names.
        var walkAnimation = checkResult.RigType == "biped" ? "preset:walk" : $"preset:{checkResult.RigType}:walk";
        var retargetResponse = await client.CreateTripo3DTaskAsync(
            new ModelRequest<Tripo3DInput, ITripo3DParameters>()
            {
                Model = "Tripo/Tripo-AnimationRetarget",
                Input = new Tripo3DInput { OriginalModelTaskId = rigResponse.Output.TaskId },
                Parameters = new Tripo3DParameters
                {
                    Animations = new List<string> { walkAnimation },
                    OutFormat = "fbx"
                }
            });
        var retarget = await WaitTaskAsync(client, retargetResponse.Output.TaskId);
        Console.WriteLine($"Animated model url: {retarget.Output.Results?.FirstOrDefault()?.ModelUrl}");
        Console.WriteLine("Urls are valid for 2 hours, download them in time.");
    }
}
