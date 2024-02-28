namespace Cnblogs.DashScope.Sdk.QWenMultimodal;

internal static class QWenMultimodalModelNames
{
    public static string GetModelName(this QWenMultimodalModel multimodalModel)
    {
        return multimodalModel switch
        {
            QWenMultimodalModel.QWenVlPlus => "qwen-vl-plus",
            QWenMultimodalModel.QWenVlMax => "qwen-vl-max",
            QWenMultimodalModel.QWenAudioTurbo => "qwen-audio-turbo",
            _ => throw new ArgumentOutOfRangeException(
                nameof(multimodalModel),
                multimodalModel,
                "Unknown model type, please use the overload that accepts a string ‘model’ parameter.")
        };
    }
}
