using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static partial class MultimodalGeneration
    {
        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>>
            QwenTtsNoSse = new(
                "multimodal-generation-qwen-tts",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen3-tts-flash",
                    Input = new MultimodalInput
                    {
                        Text = "那我来给大家推荐一款T恤，这款呢真的是超级好看，这个颜色呢很显气质，而且呢也是搭配的绝佳单品，大家可以闭眼入，真的是非常好看，对身材的包容性也很好，不管啥身材的宝宝呢，穿上去都是很好看的。推荐宝宝们下单哦。",
                        Voice = "Cherry",
                        LanguageType = "Chinese"
                    }
                },
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                {
                    Output = new MultimodalOutput(
                        null!,
                        FinishReason: "stop",
                        Audio: new SpeechSynthesisAudio
                        {
                            Data = string.Empty,
                            Url = "http://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/prod/qwen3-tts/20261122/1493478651020171/971630f1-16e0-9d55-82cc-0223806f8f41.wav?Expires=1795368712&OSSAccessKeyId=LTAI5tGzqbGcEmE58b221XQy&Signature=0tDMdqBrWS3OcGaXr%2FTJaUeg0DE%3D",
                            Id = "audio_971630f1-16e0-9d55-82cc-0223806f8f41",
                            ExpiresAt = 1795368712
                        }),
                    RequestId = "971630f1-16e0-9d55-82cc-0223806f8f41",
                    Usage = new MultimodalTokenUsage { Characters = 195 }
                });

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>>
            QwenTtsSse = new(
                "multimodal-generation-qwen-tts",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen3-tts-flash",
                    Input = new MultimodalInput
                    {
                        Text = "你好啊，我是千问",
                        Voice = "Cherry",
                        LanguageType = "Chinese"
                    }
                },
                // The last frame of the stream: intermediate frames carry base64 audio
                // chunks in Audio.Data with FinishReason "null" and an empty Audio.Url.
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                {
                    Output = new MultimodalOutput(
                        null!,
                        FinishReason: "stop",
                        Audio: new SpeechSynthesisAudio
                        {
                            Data = string.Empty,
                            Url = "http://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/prod/qwen3-tts/20261122/1493478651020171/3be7e577-3d24-924d-be5a-cbe464058342.wav?Expires=1795368803&OSSAccessKeyId=LTAI5tGzqbGcEmE58b221XQy&Signature=sOxPIi1AshlR7DffkZ16B%2BrKXhE%3D",
                            Id = "audio_3be7e577-3d24-924d-be5a-cbe464058342",
                            ExpiresAt = 1795368803
                        }),
                    RequestId = "3be7e577-3d24-924d-be5a-cbe464058342",
                    Usage = new MultimodalTokenUsage { Characters = 15 }
                });

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>>
            MinimaxSpeechNoSse = new(
                "multimodal-generation-minimax-speech",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "MiniMax/speech-2.8-hd",
                    Input = new MultimodalInput
                    {
                        Text = "今天是不是很开心呀(laughs)，当然了！",
                        VoiceSetting = new MinimaxVoiceSetting
                        {
                            VoiceId = "male-qn-qingse",
                            Speed = 1,
                            Vol = 1,
                            Pitch = 0,
                            Emotion = "happy"
                        },
                        AudioSetting = new MinimaxAudioSetting
                        {
                            SampleRate = 32000,
                            Bitrate = 128000,
                            Format = "mp3",
                            Channel = 1
                        },
                        PronunciationDict = new MinimaxPronunciationDict
                        {
                            Tone = new List<string> { "处理/(chu3)(li3)", "危险/dangerous" }
                        },
                        SubtitleEnable = false
                    }
                },
                // Data.Audio holds 123240 hex chars on the wire (61620 bytes, matching
                // ExtraInfo.AudioSize); the placeholder keeps the audio out of source
                // control, tests assert the hex length instead.
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                {
                    Output = new MultimodalOutput(
                        null!,
                        BaseResp: new MinimaxBaseResp { StatusCode = 0, StatusMsg = "success" },
                        Data: new MinimaxSpeechData { Audio = "<hex>", Ced = string.Empty, Status = 2 },
                        ExtraInfo: new MinimaxSpeechExtraInfo
                        {
                            AudioChannel = 1,
                            AudioFormat = "mp3",
                            AudioLength = 3744,
                            AudioSampleRate = 32000,
                            AudioSize = 61620,
                            Bitrate = 128000,
                            InvisibleCharacterRatio = 0,
                            UsageCharacters = 34,
                            WordCount = 22
                        },
                        TraceId: "0751089f380c8c81d950cf282bfdbdf5"),
                    RequestId = "81a55214-9c0e-9788-98e2-985d3a85f1dc",
                    Usage = new MultimodalTokenUsage { Characters = 34 }
                });

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>>
            MinimaxSpeechSse = new(
                "multimodal-generation-minimax-speech",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "MiniMax/speech-2.8-hd",
                    Input = new MultimodalInput
                    {
                        Text = "今天是不是很开心呀(laughs)，当然了！",
                        VoiceSetting = new MinimaxVoiceSetting
                        {
                            VoiceId = "male-qn-qingse",
                            Speed = 1,
                            Vol = 1,
                            Pitch = 0,
                            Emotion = "happy"
                        },
                        AudioSetting = new MinimaxAudioSetting
                        {
                            SampleRate = 32000,
                            Bitrate = 128000,
                            Format = "mp3",
                            Channel = 1
                        },
                        PronunciationDict = new MinimaxPronunciationDict
                        {
                            Tone = new List<string> { "处理/(chu3)(li3)", "危险/dangerous" }
                        },
                        SubtitleEnable = false
                    }
                },
                // The last frame of the stream: it aggregates the whole synthesis
                // (130152 hex chars, matching ExtraInfo.AudioSize); intermediate frames
                // carry hex chunks in Data.Audio with Data.Status 1 and no ExtraInfo.
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                {
                    Output = new MultimodalOutput(
                        null!,
                        BaseResp: new MinimaxBaseResp { StatusCode = 0, StatusMsg = "success" },
                        Data: new MinimaxSpeechData { Audio = "<hex>", Ced = string.Empty, Status = 2 },
                        ExtraInfo: new MinimaxSpeechExtraInfo
                        {
                            AudioChannel = 1,
                            AudioFormat = "mp3",
                            AudioLength = 3903,
                            AudioSampleRate = 32000,
                            AudioSize = 65076,
                            Bitrate = 128000,
                            InvisibleCharacterRatio = 0,
                            UsageCharacters = 34,
                            WordCount = 22
                        },
                        TraceId: "075108e7bcf5842c2b8eb6469612cd51"),
                    RequestId = "07c62d2c-de6d-9c90-a064-f565dfd87dc5",
                    Usage = new MultimodalTokenUsage { Characters = 34 }
                });

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>>
            MinimaxVoiceCloneNoSse = new(
                "multimodal-generation-minimax-voice-clone",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "MiniMax/speech-2.8-turbo",
                    Input = new MultimodalInput
                    {
                        Action = "voice_clone",
                        VoiceId = "cnblogsdksdkvoice1",
                        AudioUrl = "https://dashscope.oss-cn-beijing.aliyuncs.com/samples/audio/cosyvoice/cosyvoice-zeroshot-sample.wav",
                        Text = "今天天气怎么样？",
                        NeedNoiseReduction = false,
                        NeedVolumeNormalization = false
                    }
                },
                // extra_info is undocumented for voice cloning; the audition audio is only
                // available via the DemoAudio url.
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                {
                    Output = new MultimodalOutput(
                        null!,
                        BaseResp: new MinimaxBaseResp { StatusCode = 0, StatusMsg = "success" },
                        ExtraInfo: new MinimaxSpeechExtraInfo
                        {
                            AudioLength = 2052,
                            AudioSampleRate = 32000,
                            AudioSize = 26269,
                            Bitrate = 96900,
                            InvisibleCharacterRatio = 0,
                            UsageCharacters = 15,
                            WordCount = 8
                        },
                        InputSensitive: false,
                        InputSensitiveType: 0,
                        DemoAudio: "https://minimax-algeng-chat-tts.oss-cn-wulanchabu.aliyuncs.com/audio%2Feffect%2F0751af731907eb85eb77a67fc6c588aa_1795325050708_9559.mp3?Expires=1795497850&OSSAccessKeyId=LTAI5t6b6ytykqLxpkHbMvhN&Signature=VHKv%2FfH1b0T1VwE%2FYP8gHpj9q5g%3D"),
                    RequestId = "ebc8c41c-23fa-9799-b95f-604a4c3c6e5f",
                    Usage = new MultimodalTokenUsage { Characters = 15 }
                });
    }
}
