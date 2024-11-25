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
            QWenMultimodalModel.QWenVlOcr => "qwen-vl-ocr",
            _ => ThrowHelper.UnknownModelName(nameof(multimodalModel), multimodalModel)
        };
    }
}
