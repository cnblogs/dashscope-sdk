using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static class VoiceCustomization
    {
        public static readonly RequestSnapshot<ModelRequest<VoiceCustomizationInput, IVoiceCustomizationParameters>,
                ModelResponse<VoiceCustomizationOutput, VoiceCustomizationUsage>>
            CloneCreateNoSse = new(
                "voice-customization-create",
                new ModelRequest<VoiceCustomizationInput, IVoiceCustomizationParameters>
                {
                    Model = "voice-enrollment",
                    Input = new VoiceCustomizationInput
                    {
                        Action = "create_voice",
                        TargetModel = "qwen-audio-3.0-tts-flash",
                        Prefix = "sdkvoice",
                        Audio = "https://dashscope.oss-cn-beijing.aliyuncs.com/samples/audio/cosyvoice/cosyvoice-zeroshot-sample.wav",
                        LanguageHints = new List<string> { "zh" },
                        EnableVolumeNormalization = false
                    }
                },
                // preview_audio is an undocumented empty object when cloning.
                new ModelResponse<VoiceCustomizationOutput, VoiceCustomizationUsage>
                {
                    Output = new VoiceCustomizationOutput(
                        VoiceId: "qwen-audio-3.0-tts-flash-sdkvoice-415b04bdf1df459faf83b26f957e009c",
                        PreviewAudio: new VoicePreviewAudio()),
                    Usage = new VoiceCustomizationUsage { Count = 1 },
                    RequestId = "a2d5bce9-2c59-9c83-88d0-e0fb82f698e4"
                });

        public static readonly RequestSnapshot<ModelRequest<VoiceCustomizationInput, IVoiceCustomizationParameters>,
                ModelResponse<VoiceCustomizationOutput, VoiceCustomizationUsage>>
            DesignCreateNoSse = new(
                "voice-customization-design-create",
                new ModelRequest<VoiceCustomizationInput, IVoiceCustomizationParameters>
                {
                    Model = "voice-enrollment",
                    Input = new VoiceCustomizationInput
                    {
                        Action = "create_voice",
                        TargetModel = "cosyvoice-v3.5-plus",
                        VoicePrompt = "沉稳的中年男性，音色低沉浑厚",
                        PreviewText = "各位听众朋友们大家好，欢迎收听本期节目",
                        Prefix = "sdkvoice",
                        LanguageHints = new List<string> { "zh" }
                    },
                    Parameters = new VoiceCustomizationParameters
                    {
                        SampleRate = 24000,
                        ResponseFormat = "wav"
                    }
                },
                // PreviewAudio.Data holds 223016 base64 chars on the wire (a 3.5s wav);
                // the placeholder keeps the audio out of source control, the test asserts
                // the decoded RIFF header instead.
                new ModelResponse<VoiceCustomizationOutput, VoiceCustomizationUsage>
                {
                    Output = new VoiceCustomizationOutput(
                        VoiceId: "cosyvoice-v3.5-plus-vd-sdkvoice-27ce52c66d554daf984df90cdcb7dbc5",
                        PreviewAudio: new VoicePreviewAudio
                        {
                            Data = "<base64>",
                            SampleRate = 24000,
                            ResponseFormat = "wav"
                        }),
                    Usage = new VoiceCustomizationUsage { Count = 1 },
                    RequestId = "cbce8aa0-8cb4-96bd-bd3b-32ac01857630"
                });

        public static readonly RequestSnapshot<ModelRequest<VoiceCustomizationInput, IVoiceCustomizationParameters>,
                ModelResponse<VoiceCustomizationOutput, VoiceCustomizationUsage>>
            ListNoSse = new(
                "voice-customization-list",
                new ModelRequest<VoiceCustomizationInput, IVoiceCustomizationParameters>
                {
                    Model = "voice-enrollment",
                    Input = new VoiceCustomizationInput
                    {
                        Action = "list_voice",
                        Prefix = "sdkvoice",
                        PageSize = 10,
                        PageIndex = 0
                    }
                },
                // The designed voice (with VoicePrompt / PreviewText) and the cloned voice
                // (DEPLOYING while reviewing at capture time) in one list.
                new ModelResponse<VoiceCustomizationOutput, VoiceCustomizationUsage>
                {
                    Output = new VoiceCustomizationOutput(
                        VoiceList: new List<VoiceCustomizationVoice>
                        {
                            new(
                                VoiceId: "cosyvoice-v3.5-plus-vd-sdkvoice-27ce52c66d554daf984df90cdcb7dbc5",
                                GmtCreate: "2026-11-22 13:22:43",
                                GmtModified: "2026-11-22 13:22:43",
                                Status: "OK",
                                TargetModel: "cosyvoice-v3.5-plus",
                                VoicePrompt: "沉稳的中年男性，音色低沉浑厚",
                                PreviewText: "各位听众朋友们大家好，欢迎收听本期节目"),
                            new(
                                VoiceId: "qwen-audio-3.0-tts-flash-sdkvoice-415b04bdf1df459faf83b26f957e009c",
                                GmtCreate: "2026-11-22 13:22:26",
                                GmtModified: "2026-11-22 13:23:04",
                                Status: "DEPLOYING",
                                TargetModel: "qwen-audio-3.0-tts-flash")
                        },
                        PageIndex: 0,
                        PageSize: 10,
                        TotalCount: 2),
                    Usage = new VoiceCustomizationUsage { Count = 1 },
                    RequestId = "9798b5be-9c0e-9ead-8a94-32d6df098fab"
                });

        public static readonly RequestSnapshot<ModelRequest<VoiceCustomizationInput, IVoiceCustomizationParameters>,
                ModelResponse<VoiceCustomizationOutput, VoiceCustomizationUsage>>
            QueryNoSse = new(
                "voice-customization-query",
                new ModelRequest<VoiceCustomizationInput, IVoiceCustomizationParameters>
                {
                    Model = "voice-enrollment",
                    Input = new VoiceCustomizationInput
                    {
                        Action = "query_voice",
                        VoiceId = "qwen-audio-3.0-tts-flash-sdkvoice-415b04bdf1df459faf83b26f957e009c"
                    }
                },
                new ModelResponse<VoiceCustomizationOutput, VoiceCustomizationUsage>
                {
                    Output = new VoiceCustomizationOutput(
                        VoiceId: "qwen-audio-3.0-tts-flash-sdkvoice-415b04bdf1df459faf83b26f957e009c",
                        ResourceLink: "http://nls-cloud-cn-beijing.oss-cn-beijing.aliyuncs.com/nls-customization-data-prod/tts/voice/1493478651020171/sdkvoice/qwen-audio-3.0-tts-flash-sdkvoice-415b04bdf1df459faf83b26f957e009c/orig/qwen-audio-3.0-tts-flash-sdkvoice-415b04bdf1df459faf83b26f957e009c.wav?Expires=1795411391&OSSAccessKeyId=LTAIbiWMiTSgcJ6i&Signature=zn3G2a6QvYUx9%2BV6zq37RgagJLM%3D",
                        GmtCreate: "2026-11-22 13:22:26",
                        GmtModified: "2026-11-22 13:23:10",
                        Status: "OK",
                        TargetModel: "qwen-audio-3.0-tts-flash"),
                    Usage = new VoiceCustomizationUsage { Count = 1 },
                    RequestId = "ff63c147-7e84-9375-8b28-1df0f4fc9c78"
                });

        public static readonly RequestSnapshot<ModelRequest<VoiceCustomizationInput, IVoiceCustomizationParameters>,
                ModelResponse<VoiceCustomizationOutput, VoiceCustomizationUsage>>
            QueryDesignNoSse = new(
                "voice-customization-query-design",
                new ModelRequest<VoiceCustomizationInput, IVoiceCustomizationParameters>
                {
                    Model = "voice-enrollment",
                    Input = new VoiceCustomizationInput
                    {
                        Action = "query_voice",
                        VoiceId = "cosyvoice-v3.5-plus-vd-sdkvoice-27ce52c66d554daf984df90cdcb7dbc5"
                    }
                },
                // resource_link of a designed voice points at the pcm preview.
                new ModelResponse<VoiceCustomizationOutput, VoiceCustomizationUsage>
                {
                    Output = new VoiceCustomizationOutput(
                        VoiceId: "cosyvoice-v3.5-plus-vd-sdkvoice-27ce52c66d554daf984df90cdcb7dbc5",
                        ResourceLink: "http://nls-cloud-cn-beijing.oss-cn-beijing.aliyuncs.com/nls-customization-data-prod/tts/voice/1493478651020171/sdkvoice/cosyvoice-v3.5-plus-vd-sdkvoice-27ce52c66d554daf984df90cdcb7dbc5/orig/cosyvoice-v3.5-plus-vd-sdkvoice-27ce52c66d554daf984df90cdcb7dbc5.pcm?Expires=1795411394&OSSAccessKeyId=LTAIbiWMiTSgcJ6i&Signature=iTKikkCXFcLV1%2FOGSaxKMkyDnu8%3D",
                        GmtCreate: "2026-11-22 13:22:43",
                        GmtModified: "2026-11-22 13:22:43",
                        Status: "OK",
                        TargetModel: "cosyvoice-v3.5-plus",
                        VoicePrompt: "沉稳的中年男性，音色低沉浑厚",
                        PreviewText: "各位听众朋友们大家好，欢迎收听本期节目"),
                    Usage = new VoiceCustomizationUsage { Count = 1 },
                    RequestId = "47ed91b8-5dd7-97e0-a5e4-c5a7db2540eb"
                });

        public static readonly RequestSnapshot<ModelRequest<VoiceCustomizationInput, IVoiceCustomizationParameters>,
                ModelResponse<VoiceCustomizationOutput, VoiceCustomizationUsage>>
            UpdateNoSse = new(
                "voice-customization-update",
                new ModelRequest<VoiceCustomizationInput, IVoiceCustomizationParameters>
                {
                    Model = "voice-enrollment",
                    Input = new VoiceCustomizationInput
                    {
                        Action = "update_voice",
                        VoiceId = "qwen-audio-3.0-tts-flash-sdkvoice-415b04bdf1df459faf83b26f957e009c",
                        Audio = "https://dashscope.oss-cn-beijing.aliyuncs.com/samples/audio/cosyvoice/cosyvoice-zeroshot-sample.wav"
                    }
                },
                // preview_audio is an undocumented empty object in the update response too.
                new ModelResponse<VoiceCustomizationOutput, VoiceCustomizationUsage>
                {
                    Output = new VoiceCustomizationOutput(PreviewAudio: new VoicePreviewAudio()),
                    Usage = new VoiceCustomizationUsage { Count = 1 },
                    RequestId = "2cee1da9-5276-9099-a572-8a03aae9ed03"
                });

        public static readonly RequestSnapshot<ModelRequest<VoiceCustomizationInput, IVoiceCustomizationParameters>,
                ModelResponse<VoiceCustomizationOutput, VoiceCustomizationUsage>>
            DeleteNoSse = new(
                "voice-customization-delete",
                new ModelRequest<VoiceCustomizationInput, IVoiceCustomizationParameters>
                {
                    Model = "voice-enrollment",
                    Input = new VoiceCustomizationInput
                    {
                        Action = "delete_voice",
                        VoiceId = "qwen-audio-3.0-tts-flash-sdkvoice-415b04bdf1df459faf83b26f957e009c"
                    }
                },
                new ModelResponse<VoiceCustomizationOutput, VoiceCustomizationUsage>
                {
                    Output = new VoiceCustomizationOutput(),
                    Usage = new VoiceCustomizationUsage { Count = 1 },
                    RequestId = "56a7c0fc-ad2d-925d-b744-d21b7c1b9136"
                });
    }
}
