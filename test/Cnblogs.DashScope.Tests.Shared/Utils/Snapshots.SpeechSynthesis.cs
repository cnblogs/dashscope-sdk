using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static partial class SpeechSynthesis
    {
        public static readonly RequestSnapshot<ModelRequest<SpeechSynthesisInput>,
                ModelResponse<SpeechSynthesisOutput, SpeechSynthesisUsage>>
            NoSse = new(
                "speech-synthesizer",
                new ModelRequest<SpeechSynthesisInput>
                {
                    Model = "qwen-audio-3.0-tts-flash",
                    Input = new SpeechSynthesisInput
                    {
                        Text = "我家的后面有一个很大的花园。",
                        Voice = "longanhuan_v3.6",
                        Format = "wav",
                        SampleRate = 24000
                    }
                },
                new ModelResponse<SpeechSynthesisOutput, SpeechSynthesisUsage>
                {
                    Output = new SpeechSynthesisOutput(
                        "stop",
                        Audio: new SpeechSynthesisAudio
                        {
                            Data = string.Empty,
                            Url = "http://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/prod/qwen-audio-3.0-tts-flash/20261122/1493478651020171/5b6769f6-b191-9307-b03e-d63fd041606f.wav?Expires=1795406500&OSSAccessKeyId=LTAI5tGzqbGcEmE58b221XQy&Signature=IJYq15QWYjBM3y8%2Fkr6KpUkzjXc%3D",
                            Id = "audio_5b6769f6-b191-9307-b03e-d63fd041606f",
                            ExpiresAt = 1795406500
                        }),
                    Usage = new SpeechSynthesisUsage
                    {
                        Characters = 27,
                        InputTokens = 14,
                        OutputTokens = 36,
                        TotalTokens = 50
                    },
                    RequestId = "5b6769f6-b191-9307-b03e-d63fd041606f"
                });

        public static readonly RequestSnapshot<ModelRequest<SpeechSynthesisInput>,
                ModelResponse<SpeechSynthesisOutput, SpeechSynthesisUsage>>
            Sse = new(
                "speech-synthesizer",
                new ModelRequest<SpeechSynthesisInput>
                {
                    Model = "qwen-audio-3.0-tts-flash",
                    Input = new SpeechSynthesisInput
                    {
                        Text = "我家的后面有一个很大的花园。",
                        Voice = "longanhuan_v3.6",
                        Format = "wav",
                        SampleRate = 24000,
                        WordTimestampEnabled = true
                    }
                },
                // The final frame of the stream: sentence-begin / sentence-synthesis /
                // sentence-end events precede it, the words of sentence-end carry
                // per-character timestamps (13 words for the 14 character sentence).
                new ModelResponse<SpeechSynthesisOutput, SpeechSynthesisUsage>
                {
                    Output = new SpeechSynthesisOutput(
                        "stop",
                        Audio: new SpeechSynthesisAudio
                        {
                            Data = string.Empty,
                            Url = "http://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/prod/qwen-audio-3.0-tts-flash/20261122/1493478651020171/178bbc28-19f4-964c-9e86-223ddeb773fc.wav?Expires=1795406529&OSSAccessKeyId=LTAI5tGzqbGcEmE58b221XQy&Signature=60p7noVkVAQgwD7Kztp1h04vfig%3D",
                            Id = "audio_178bbc28-19f4-964c-9e86-223ddeb773fc",
                            ExpiresAt = 1795406529
                        }),
                    Usage = new SpeechSynthesisUsage
                    {
                        Characters = 27,
                        InputTokens = 14,
                        OutputTokens = 36,
                        TotalTokens = 50
                    },
                    RequestId = "178bbc28-19f4-964c-9e86-223ddeb773fc"
                });

        public static readonly RequestSnapshot<ModelRequest<SpeechSynthesisInput>,
                ModelResponse<SpeechSynthesisOutput, SpeechSynthesisUsage>>
            AudioGenerationNoSse = new(
                "speech-synthesizer-audio-generation",
                new ModelRequest<SpeechSynthesisInput>
                {
                    Model = "qwen-audio-3.1-tts-next",
                    Input = new SpeechSynthesisInput
                    {
                        TextPrompt = "一位女性清晰地说：“你好，欢迎使用。”",
                        Format = "wav",
                        SampleRate = 48000,
                        Channels = 2
                    }
                },
                // The doc only documents usage.duration; the tokens are an undocumented
                // extra of the real response.
                new ModelResponse<SpeechSynthesisOutput, SpeechSynthesisUsage>
                {
                    Output = new SpeechSynthesisOutput(
                        "stop",
                        Audio: new SpeechSynthesisAudio
                        {
                            Data = string.Empty,
                            Url = "http://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/prod/qwen-audio-3.1-tts-next/20261122/1493478651020171/d5a8812a-fc96-9fe7-ab8a-414d90264ea1.wav?Expires=1795414557&OSSAccessKeyId=LTAI5tGzqbGcEmE58b221XQy&Signature=nk64CVunsZ4p%2BybvJMSMC2muXo8%3D",
                            Id = "audio_d5a8812a-fc96-9fe7-ab8a-414d90264ea1",
                            ExpiresAt = 1795414557,
                            Duration = 1.6f
                        }),
                    Usage = new SpeechSynthesisUsage
                    {
                        InputTokens = 171,
                        OutputTokens = 320,
                        TotalTokens = 491,
                        Duration = 2
                    },
                    RequestId = "d5a8812a-fc96-9fe7-ab8a-414d90264ea1"
                });

        public static readonly RequestSnapshot<ModelRequest<SpeechSynthesisInput>,
                ModelResponse<SpeechSynthesisOutput, SpeechSynthesisUsage>>
            AudioGenerationReferenceNoSse = new(
                "speech-synthesizer-audio-generation-reference",
                new ModelRequest<SpeechSynthesisInput>
                {
                    Model = "qwen-audio-3.1-tts-next",
                    Input = new SpeechSynthesisInput
                    {
                        TextPrompt = "@voice1 用参考音频的声音说：“代码改变世界。”",
                        References = new List<SpeechSynthesisReference>
                        {
                            new()
                            {
                                AudioUrl = "https://dashscope.oss-cn-beijing.aliyuncs.com/samples/audio/cosyvoice/cosyvoice-zeroshot-sample.wav"
                            }
                        },
                        Format = "mp3",
                        EnableCbr = true,
                        BitRate = 128
                    }
                },
                new ModelResponse<SpeechSynthesisOutput, SpeechSynthesisUsage>
                {
                    Output = new SpeechSynthesisOutput(
                        "stop",
                        Audio: new SpeechSynthesisAudio
                        {
                            Data = string.Empty,
                            Url = "http://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/prod/qwen-audio-3.1-tts-next/20261122/1493478651020171/3ab107a4-3f40-94e8-af70-4c5e3ec50fe0.mp3?Expires=1795414580&OSSAccessKeyId=LTAI5tGzqbGcEmE58b221XQy&Signature=oqFTRpXbbmAmtmaKD4%2FVayFBBXA%3D",
                            Id = "audio_3ab107a4-3f40-94e8-af70-4c5e3ec50fe0",
                            ExpiresAt = 1795414580,
                            Duration = 2.44f
                        }),
                    Usage = new SpeechSynthesisUsage
                    {
                        InputTokens = 3987,
                        OutputTokens = 488,
                        TotalTokens = 4475,
                        Duration = 2
                    },
                    RequestId = "3ab107a4-3f40-94e8-af70-4c5e3ec50fe0"
                });
    }
}
