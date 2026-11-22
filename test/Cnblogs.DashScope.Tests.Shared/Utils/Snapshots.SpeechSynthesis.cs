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
    }
}
