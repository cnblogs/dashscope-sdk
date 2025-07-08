namespace Cnblogs.DashScope.Sdk.Wanx;

internal static class WanxModelNames
{
    public static string GetModelName(this WanxModel model)
    {
        return model switch
        {
            WanxModel.WanxV1 => "wanx-v1",
            WanxModel.WanxV21Plus => "wanx2.1-t2i-plus",
            WanxModel.WanxV21Turbo => "wanx2.1-t2i-turbo",
            WanxModel.WanxV20Turbo => "wanx2.0-t2i-turbo",
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
