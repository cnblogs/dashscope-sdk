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
            QWenMultimodalModel.QWenVlV1 => "qwen-vl-v1",
            QWenMultimodalModel.QWenVlChatV1 => "qwen-vl-chat-v1",
            QWenMultimodalModel.QWenAudioChat => "qwen-audio-chat",
            _ => throw new ArgumentOutOfRangeException(
                nameof(multimodalModel),
                multimodalModel,
                "Unknown model type, please use the overload that accepts a string ‘model’ parameter.")
        };
    }
}
