using Cnblogs.DashScope.Sdk.Internals;

namespace Cnblogs.DashScope.Sdk.Wanx;

internal static class WanxModelNames
{
    public static string GetModelName(this WanxModel model)
    {
        return model switch
        {
            WanxModel.WanxV1 => "wanx-v1",
            _ => ThrowHelper.UnknownModelName(nameof(model), model)
        };
    }

    public static string GetModelName(this WanxStyleRepaintModel model)
    {
        return model switch
        {
            WanxStyleRepaintModel.WanxStyleRepaintingV1 => "wanx-style-repaint-v1",
            _ => ThrowHelper.UnknownModelName(nameof(model), model)
        };
    }

    public static string GetModelName(this WanxBackgroundGenerationModel model)
    {
        return model switch
        {
            WanxBackgroundGenerationModel.WanxBackgroundGenerationV2 => "wanx-background-generation-v2",
            _ => ThrowHelper.UnknownModelName(nameof(model), model)
        };
    }
}
