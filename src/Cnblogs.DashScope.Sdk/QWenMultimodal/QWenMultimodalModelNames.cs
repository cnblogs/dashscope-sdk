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
            QWenMultimodalModel.QWenVlMaxLatest => "qwen-vl-max-latest",
            QWenMultimodalModel.QWenVlPlusLatest => "qwen-vl-plus-latest",
            QWenMultimodalModel.QWenVlOcrLatest => "qwen-vl-ocr-latest",
            QWenMultimodalModel.QWenAudioTurboLatest => "qwen-audio-turbo-latest",
            _ => ThrowHelper.UnknownModelName(nameof(multimodalModel), multimodalModel)
        };
    }
}
