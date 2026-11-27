using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Sdk;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static partial class OpenAiCompatibleChat
    {
        private const string AudioUrl = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250211/tixcef/cherry.wav";

        private const string ImageUrl = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241022/emyrja/dog_and_girl.jpeg";

        /// <summary>
        /// The audio output of qwen3.5-omni-flash: the model streams a part of the text, then the audio that
        /// speaks it, then the next part of the text. Captured with -TrimAudioDeltas, which keeps the first and
        /// the last audio chunk of every run and replaces their base64 payload with a placeholder.
        /// </summary>
        public static readonly RequestSnapshot<OpenAiCompatibleChatRequest, List<OpenAiCompatibleChatChunk>>
            OmniAudioOutputSse = new(
                "openai-compatible-omni-audio-output",
                new OpenAiCompatibleChatRequest
                {
                    Model = "qwen3.5-omni-flash",
                    Messages = new List<OpenAiCompatibleChatMessage>
                    {
                        new OpenAiCompatibleChatMessage
                        {
                            Role = "user",
                            Content = new List<OpenAiCompatibleChatContentPart> { new OpenAiCompatibleChatContentPart(Type: "text", Text: "用一句话介绍博客园。") }
                        }
                    },
                    Modalities = new List<string> { "text", "audio" },
                    Audio = new OpenAiCompatibleChatAudioOptions(Voice: "Tina", Format: "wav"),
                    StreamOptions = new OpenAiCompatibleChatStreamOptions(IncludeUsage: true)
                },
                new List<OpenAiCompatibleChatChunk>
                {
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                        Object = "chat.completion.chunk",
                        Created = 1795703676,
                        Model = "qwen3.5-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Role = "assistant",
                                    Content = string.Empty
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                        Object = "chat.completion.chunk",
                        Created = 1795703676,
                        Model = "qwen3.5-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "博客"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                        Object = "chat.completion.chunk",
                        Created = 1795703676,
                        Model = "qwen3.5-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "园（"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                        Object = "chat.completion.chunk",
                        Created = 1795703676,
                        Model = "qwen3.5-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "CN"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                        Object = "chat.completion.chunk",
                        Created = 1795703676,
                        Model = "qwen3.5-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "Blogs"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                        Object = "chat.completion.chunk",
                        Created = 1795703676,
                        Model = "qwen3.5-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "）是一个面向"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                        Object = "chat.completion.chunk",
                        Created = 1795703676,
                        Model = "qwen3.5-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "开发"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                        Object = "chat.completion.chunk",
                        Created = 1795703676,
                        Model = "qwen3.5-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "者的技术"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                        Object = "chat.completion.chunk",
                        Created = 1795703676,
                        Model = "qwen3.5-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "分享"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                        Object = "chat.completion.chunk",
                        Created = 1795703676,
                        Model = "qwen3.5-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "与交流平台"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                        Object = "chat.completion.chunk",
                        Created = 1795703676,
                        Model = "qwen3.5-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "，以"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                        Object = "chat.completion.chunk",
                        Created = 1795703676,
                        Model = "qwen3.5-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "高质量"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                        Object = "chat.completion.chunk",
                        Created = 1795703676,
                        Model = "qwen3.5-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "的技术文章"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                        Object = "chat.completion.chunk",
                        Created = 1795703676,
                        Model = "qwen3.5-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "、活跃的"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                        Object = "chat.completion.chunk",
                        Created = 1795703676,
                        Model = "qwen3.5-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "社区互动"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                        Object = "chat.completion.chunk",
                        Created = 1795703676,
                        Model = "qwen3.5-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "及"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                        Object = "chat.completion.chunk",
                        Created = 1795703676,
                        Model = "qwen3.5-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Audio = new OpenAiCompatibleChatDeltaAudio(
                                        Data: "<base64>",
                                        Id: "audio_dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                                        ExpiresAt: 1795703676)
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                        Object = "chat.completion.chunk",
                        Created = 1795703676,
                        Model = "qwen3.5-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "开源"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                        Object = "chat.completion.chunk",
                        Created = 1795703676,
                        Model = "qwen3.5-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "项目"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                        Object = "chat.completion.chunk",
                        Created = 1795703676,
                        Model = "qwen3.5-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "资源"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                        Object = "chat.completion.chunk",
                        Created = 1795703676,
                        Model = "qwen3.5-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "而"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                        Object = "chat.completion.chunk",
                        Created = 1795703676,
                        Model = "qwen3.5-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "著称。"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                        Object = "chat.completion.chunk",
                        Created = 1795703676,
                        Model = "qwen3.5-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Audio = new OpenAiCompatibleChatDeltaAudio(
                                        Data: "<base64>",
                                        Id: "audio_dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                                        ExpiresAt: 1795703676)
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                        Object = "chat.completion.chunk",
                        Created = 1795703676,
                        Model = "qwen3.5-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Audio = new OpenAiCompatibleChatDeltaAudio(
                                        Data: "<base64>",
                                        Id: "audio_dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                                        ExpiresAt: 1795703677)
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                        Object = "chat.completion.chunk",
                        Created = 1795703676,
                        Model = "qwen3.5-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = string.Empty
                                },
                                FinishReason = "stop"
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-dc7b2ca5-0b08-9f58-bab5-cd7b4a2fe81d",
                        Object = "chat.completion.chunk",
                        Created = 1795703676,
                        Model = "qwen3.5-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>(),
                        Usage = new OpenAiCompatibleChatUsage
                        {
                            PromptTokens = 18,
                            CompletionTokens = 172,
                            TotalTokens = 190,
                            PromptTokensDetails = new OpenAiCompatibleChatTokenDetails
                            {
                                TextTokens = 18
                            },
                            CompletionTokensDetails = new OpenAiCompatibleChatTokenDetails
                            {
                                AudioTokens = 139,
                                TextTokens = 33
                            }
                        }
                    }
                });

        /// <summary>
        /// A non-streaming request of a model with audio output answers with the text only, although the usage
        /// still counts the audio tokens of the speech that was generated.
        /// </summary>
        public static readonly RequestSnapshot<OpenAiCompatibleChatRequest, OpenAiCompatibleChatCompletion>
            OmniAudioOutputNoSse = new(
                "openai-compatible-omni-audio-output",
                new OpenAiCompatibleChatRequest
                {
                    Model = "qwen3.5-omni-flash",
                    Messages = new List<OpenAiCompatibleChatMessage>
                    {
                        new OpenAiCompatibleChatMessage
                        {
                            Role = "user",
                            Content = new List<OpenAiCompatibleChatContentPart> { new OpenAiCompatibleChatContentPart(Type: "text", Text: "你是谁") }
                        }
                    },
                    Modalities = new List<string> { "text", "audio" },
                    Audio = new OpenAiCompatibleChatAudioOptions(Voice: "Tina", Format: "wav")
                },
                new OpenAiCompatibleChatCompletion
                {
                    Id = "chatcmpl-c3dc2d43-27e4-9087-a745-ddebe74b02cf",
                    Object = "chat.completion",
                    Created = 1795703734,
                    Model = "qwen3.5-omni-flash",
                    Choices = new List<OpenAiCompatibleChatCompletionChoice>
                    {
                        new OpenAiCompatibleChatCompletionChoice
                        {
                            Index = 0,
                            Message = new OpenAiCompatibleChatCompletionMessage
                            {
                                Role = "assistant",
                                Content = "我是 Qwen-Omni，通义千问系列的多模态超大规模语言模型。我由阿里巴巴集团旗下的通义实验室研发，能够实时理解并融合文本、图像、音频、视频等多种模态信息。支持用户输入图片、视频和音频文件，" +
                                          "还支持实时麦克风录音和摄像头录影，我可以进行多轮对话，提供文字或语音回复。\n\n如果你有任何问题或需要帮助，欢迎随时告诉我！",
                                ReasoningContent = string.Empty
                            },
                            FinishReason = "stop"
                        }
                    },
                    Usage = new OpenAiCompatibleChatUsage
                    {
                        PromptTokens = 13,
                        CompletionTokens = 447,
                        TotalTokens = 460,
                        PromptTokensDetails = new OpenAiCompatibleChatTokenDetails
                        {
                            TextTokens = 13
                        },
                        CompletionTokensDetails = new OpenAiCompatibleChatTokenDetails
                        {
                            AudioTokens = 355,
                            TextTokens = 92
                        }
                    }
                });

        /// <summary>
        /// qwen3-omni-flash only outputs audio while it is not thinking. Captured with -TrimAudioDeltas.
        /// </summary>
        public static readonly RequestSnapshot<OpenAiCompatibleChatRequest, List<OpenAiCompatibleChatChunk>>
            OmniQwen3AudioOutputSse = new(
                "openai-compatible-omni-qwen3-audio-output",
                new OpenAiCompatibleChatRequest
                {
                    Model = "qwen3-omni-flash",
                    Messages = new List<OpenAiCompatibleChatMessage>
                    {
                        new OpenAiCompatibleChatMessage
                        {
                            Role = "user",
                            Content = new List<OpenAiCompatibleChatContentPart> { new OpenAiCompatibleChatContentPart(Type: "text", Text: "你是谁") }
                        }
                    },
                    Modalities = new List<string> { "text", "audio" },
                    Audio = new OpenAiCompatibleChatAudioOptions(Voice: "Cherry", Format: "wav"),
                    StreamOptions = new OpenAiCompatibleChatStreamOptions(IncludeUsage: true),
                    EnableThinking = false
                },
                new List<OpenAiCompatibleChatChunk>
                {
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Role = "assistant",
                                    Content = string.Empty
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "我是"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "你的"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "对话"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "伙伴"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "，"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "随时"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "准备"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "陪你"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "聊天"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "、"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "解答"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "问题"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "或者"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "只是"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "安静"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "地"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "听"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "你说"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "说话"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "。"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "你可以"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "把我"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "当成"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "一个"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "朋友"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "，"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "不用"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Audio = new OpenAiCompatibleChatDeltaAudio(
                                        Data: "<base64>",
                                        Id: "audio_2692602c-f3b2-990f-80a5-1b0774080081",
                                        ExpiresAt: 1795703678)
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "拘"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "束"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "，"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "有什么"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "想"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "问"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "的"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Audio = new OpenAiCompatibleChatDeltaAudio(
                                        Data: "<base64>",
                                        Id: "audio_2692602c-f3b2-990f-80a5-1b0774080081",
                                        ExpiresAt: 1795703678)
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "、"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "想"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "聊"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "的"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "，"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "我"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "都会"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Audio = new OpenAiCompatibleChatDeltaAudio(
                                        Data: "<base64>",
                                        Id: "audio_2692602c-f3b2-990f-80a5-1b0774080081",
                                        ExpiresAt: 1795703678)
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "尽力"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "回应"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "。"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "你"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "今天"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "想"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Audio = new OpenAiCompatibleChatDeltaAudio(
                                        Data: "<base64>",
                                        Id: "audio_2692602c-f3b2-990f-80a5-1b0774080081",
                                        ExpiresAt: 1795703678)
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "从"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "哪里"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "开始"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "呢"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "？"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Audio = new OpenAiCompatibleChatDeltaAudio(
                                        Data: "<base64>",
                                        Id: "audio_2692602c-f3b2-990f-80a5-1b0774080081",
                                        ExpiresAt: 1795703678)
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Audio = new OpenAiCompatibleChatDeltaAudio(
                                        Data: "<base64>",
                                        Id: "audio_2692602c-f3b2-990f-80a5-1b0774080081",
                                        ExpiresAt: 1795703682)
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = string.Empty
                                },
                                FinishReason = "stop"
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-2692602c-f3b2-990f-80a5-1b0774080081",
                        Object = "chat.completion.chunk",
                        Created = 1795703678,
                        Model = "qwen3-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>(),
                        Usage = new OpenAiCompatibleChatUsage
                        {
                            PromptTokens = 191,
                            CompletionTokens = 238,
                            TotalTokens = 429,
                            PromptTokensDetails = new OpenAiCompatibleChatTokenDetails
                            {
                                TextTokens = 191
                            },
                            CompletionTokensDetails = new OpenAiCompatibleChatTokenDetails
                            {
                                AudioTokens = 186,
                                TextTokens = 52
                            }
                        }
                    }
                });

        /// <summary>
        /// qwen3.8-omni-flash thinks by default, its reasoning_content deltas come before the text.
        /// </summary>
        public static readonly RequestSnapshot<OpenAiCompatibleChatRequest, List<OpenAiCompatibleChatChunk>>
            OmniAudioInputSse = new(
                "openai-compatible-omni-audio-input",
                new OpenAiCompatibleChatRequest
                {
                    Model = "qwen3.8-omni-flash",
                    Messages = new List<OpenAiCompatibleChatMessage>
                    {
                        new OpenAiCompatibleChatMessage
                        {
                            Role = "user",
                            Content = new List<OpenAiCompatibleChatContentPart>
                            {
                                new OpenAiCompatibleChatContentPart(
                                    Type: "input_audio",
                                    InputAudio: new OpenAiCompatibleChatInputAudio(Data: AudioUrl, Format: "wav")),
                                new OpenAiCompatibleChatContentPart(Type: "text", Text: "这段音频在说什么")
                            }
                        }
                    },
                    StreamOptions = new OpenAiCompatibleChatStreamOptions(IncludeUsage: true)
                },
                new List<OpenAiCompatibleChatChunk>
                {
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Role = "assistant",
                                    Content = string.Empty,
                                    ReasoningContent = string.Empty
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "The"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " user wants"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " me"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " to trans"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "cribe or summarize what"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " is"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " being said in the"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " audio.\n\n"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "1.  **"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "Transcribe the audio"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = ":** \"嗯"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "，这幅图啊"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "，是在海滩上"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "，有个女人和"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "一只狗在玩"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "呢。"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "背景是海浪拍"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "打着沙滩，天空"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "有点发白，"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "应该是日出或者日落"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "的时候吧。你要是"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "还有啥想知道"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "的尽管"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "问哈。\""
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "\n2. "
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " **Translate/"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "Summarize:**"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " The speaker is describing"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " a"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " picture. It shows"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " a woman"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " and a dog playing"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " on a beach."
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " The background features"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " waves hitting"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " the sand,"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " and the sky is"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " whitish, suggesting"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " sunrise or sunset."
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " The speaker invites"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " further"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " questions.\n\n"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "I"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " will provide"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " the Chinese"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " transcription as"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " the primary"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " answer,"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " as"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " the"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " question is"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = " in Chinese."
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "这段"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "音频的内容"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "如下"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "：\n\n“"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "嗯，这幅"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "图啊，是在"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "海滩上，有个"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "女人和一只狗"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "在玩呢。背景"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "是海浪拍"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "打着沙滩，天空"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "有点发白，"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "应该是日出或者日落"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "的时候吧。你要是"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "还有啥想知道"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "的尽管问哈"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "。”\n\n简单来说"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "，说话人正在"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "描述一张"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "图片："
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "画面"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "是在海滩上，"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "一个女人"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "和一只狗在"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "玩耍，背景是"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "海浪和"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "发白的天空（"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "可能是日出或日落"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "），最后"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "表示"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "如果"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "还有"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "问题"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "可以继续"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "问。"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = string.Empty
                                },
                                FinishReason = "stop"
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-38616d9e-dcc0-9e84-bffb-7a359869ae0c",
                        Object = "chat.completion.chunk",
                        Created = 1795703703,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>(),
                        Usage = new OpenAiCompatibleChatUsage
                        {
                            PromptTokens = 170,
                            CompletionTokens = 250,
                            TotalTokens = 420,
                            PromptTokensDetails = new OpenAiCompatibleChatTokenDetails
                            {
                                AudioTokens = 114,
                                TextTokens = 56,
                                CachedTokens = 0
                            },
                            CompletionTokensDetails = new OpenAiCompatibleChatTokenDetails
                            {
                                TextTokens = 100,
                                ReasoningTokens = 150
                            }
                        }
                    }
                });

        /// <summary>
        /// An image, the frames of a video and an audio in one turn of qwen3.8-omni-flash, the thinking is limited
        /// by a budget.
        /// </summary>
        public static readonly RequestSnapshot<OpenAiCompatibleChatRequest, OpenAiCompatibleChatCompletion>
            OmniMixedInputNoSse = new(
                "openai-compatible-omni-mixed-input",
                new OpenAiCompatibleChatRequest
                {
                    Model = "qwen3.8-omni-flash",
                    Messages = new List<OpenAiCompatibleChatMessage>
                    {
                        new OpenAiCompatibleChatMessage
                        {
                            Role = "user",
                            Content = new List<OpenAiCompatibleChatContentPart>
                            {
                                new OpenAiCompatibleChatContentPart(Type: "image_url", ImageUrl: new OpenAiCompatibleChatImageUrl(Url: ImageUrl)),
                                new OpenAiCompatibleChatContentPart(
                                    Type: "video",
                                    Video: new List<string>
                                    {
                                        "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/xzsgiz/football1.jpg",
                                        "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/tdescd/football2.jpg",
                                        "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/zefdja/football3.jpg",
                                        "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/aedbqh/football4.jpg"
                                    },
                                    Fps: 2.0f),
                                new OpenAiCompatibleChatContentPart(
                                    Type: "input_audio",
                                    InputAudio: new OpenAiCompatibleChatInputAudio(Data: AudioUrl, Format: "wav")),
                                new OpenAiCompatibleChatContentPart(Type: "text", Text: "请分别用一句话说出图片、视频和音频的内容。")
                            }
                        }
                    },
                    ThinkingBudget = 4096
                },
                new OpenAiCompatibleChatCompletion
                {
                    Id = "chatcmpl-1a2088c5-8461-907d-96e6-5d843edfed57",
                    Object = "chat.completion",
                    Created = 1795703706,
                    Model = "qwen3.8-omni-flash",
                    Choices = new List<OpenAiCompatibleChatCompletionChoice>
                    {
                        new OpenAiCompatibleChatCompletionChoice
                        {
                            Index = 0,
                            Message = new OpenAiCompatibleChatCompletionMessage
                            {
                                Role = "assistant",
                                Content = "图片：在夕阳（或朝阳）映照的海滩上，一位穿格子衬衫的女子坐着与一只戴胸背带的拉布拉多犬伸手互动，画面温馨。\n\n视频：透过球门网视角记录了一场足球比赛的进球瞬间，守门员飞身扑救未果，球员们在场中" +
                                          "庆祝。\n\n音频：一段轻松的中文口语描述，讲述的是海滩上女人与狗玩耍、背景为海浪与泛白天空（疑似日出或日落）的画面内容。",
                                ReasoningContent = "用户要求分别用一句话概括图片、视频和音频的内容。我需要观察三个模态的核心信息：图片是海滩上女子与狗击掌/互动的温馨场景；视频是足球比赛进球瞬间（透过球网视角，守门员扑救、球员庆祝）；音频是一段" +
                                                   "中文口语描述，内容恰好对应图片（海滩、女人和狗、日出日落）。我要提炼各自最显著的特征，用简洁的一句话表达，避免冗长。语言应与用户一致，使用中文。"
                            },
                            FinishReason = "stop"
                        }
                    },
                    Usage = new OpenAiCompatibleChatUsage
                    {
                        PromptTokens = 3870,
                        CompletionTokens = 203,
                        TotalTokens = 4073,
                        PromptTokensDetails = new OpenAiCompatibleChatTokenDetails
                        {
                            AudioTokens = 114,
                            TextTokens = 63,
                            VideoTokens = 1190,
                            ImageTokens = 2503,
                            CachedTokens = 0
                        },
                        CompletionTokensDetails = new OpenAiCompatibleChatTokenDetails
                        {
                            TextTokens = 102,
                            ReasoningTokens = 101
                        }
                    }
                });

        /// <summary>
        /// A video file sampled at one frame per second by qwen3.8-omni-flash.
        /// </summary>
        public static readonly RequestSnapshot<OpenAiCompatibleChatRequest, List<OpenAiCompatibleChatChunk>>
            OmniVideoInputSse = new(
                "openai-compatible-omni-video-input",
                new OpenAiCompatibleChatRequest
                {
                    Model = "qwen3.8-omni-flash",
                    Messages = new List<OpenAiCompatibleChatMessage>
                    {
                        new OpenAiCompatibleChatMessage
                        {
                            Role = "user",
                            Content = new List<OpenAiCompatibleChatContentPart>
                            {
                                new OpenAiCompatibleChatContentPart(
                                    Type: "video_url",
                                    VideoUrl: new OpenAiCompatibleChatVideoUrl(
                                        Url: "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241115/cqqkru/1.mp4"),
                                    Fps: 1.0f),
                                new OpenAiCompatibleChatContentPart(Type: "text", Text: "视频的内容是什么?")
                            }
                        }
                    },
                    StreamOptions = new OpenAiCompatibleChatStreamOptions(IncludeUsage: true),
                    ReasoningEffort = "low"
                },
                new List<OpenAiCompatibleChatChunk>
                {
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Role = "assistant",
                                    Content = string.Empty,
                                    ReasoningContent = string.Empty
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "用户提供"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "了一段"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "视频（实际上"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "是一组"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "带"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "时间戳的"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "帧），"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "并"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "询问“视频的内容"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "是什么？”。我需要"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "观察这些"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "帧，"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "描述"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "视频内容。\n\n"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "观察帧："
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "画面中"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "是一位年轻女性，"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "短发"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "（棕色波波"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "头/"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "齐肩"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "短发带刘海），"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "穿着粉色针织开"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "衫内"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "搭白色T恤"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "，戴着"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "细项链。"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "背景是模糊的"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "户外建筑（暖"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "色调，像是街道"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "或校园建筑"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "）。右上角有“"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "通义·"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "AI合成”水印"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "，说明这是AI"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "生成的视频。"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "\n\n她的"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "表情"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "在"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "变化：微笑"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "、说话/"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "唱歌的口型（"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "嘴巴"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "张开）、"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "眨眼等"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "，看起来"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "像是在对着镜头说话"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "或唱歌，"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "带有"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "生动的"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "面部"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "表情和"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "轻微"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "头部动作。\n\n"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "结合音频线索"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "？"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "用户没有提供"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "音频"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "，但帧"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "序列"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "显示口型在"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "动"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "，像是在歌唱"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "或讲话"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "。水印"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "表明"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "AI"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "合成。\n\n我"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "应直接、"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "完整地回答"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "视频内容描述，"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "不需要冗长铺垫"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "。语言用中文"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "。\n\n我会"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "描述：主体"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "人物、外貌"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "、服装、背景"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "、动作表情"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "、以及"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "AI"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ReasoningContent = "合成标识。"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "这段"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "视频的内容"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "是一位"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "年轻"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "女性的近"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "景人像"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "，"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "她"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "正"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "对着镜头微笑"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "、说话（"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "或唱歌），"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "表情"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "生动自然。"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "具体"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "细节"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "如下：\n\n1"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = ". **人物形象"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "**：一位"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "长相"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "清秀的年轻女子"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "，留着棕"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "色的"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "齐肩短发"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "（波波头）"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "配"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "空气刘海，皮肤"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "白皙，化"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "着淡"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "妆，面带"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "笑容。"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "\n"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "2. **穿着"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "打扮**：身穿"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "粉色"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "针织开衫，"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "内搭白色圆"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "领T"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "恤，脖子上"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "戴着一条细项链"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "，整体风格"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "清新温柔"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "。\n3."
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = " **动作表情"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "**：在整个"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "片段"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "中，她的嘴"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "型"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "不断开合，"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "像是在对着"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "镜头讲话"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "或唱歌，"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "伴随"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "微笑"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "、眨眼、轻微的"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "头部摆动"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "等自然"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "的面"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "部与"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "肢体动作，显得"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "活泼"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "亲切。\n4"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = ". **背景"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "环境**：背景"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "为虚化的户外"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "场景，呈"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "暖色调，"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "隐约可见建筑物"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "（"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "类似"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "街道或校园楼房"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "），阳光"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "柔和"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "，营造出温暖"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "明亮的氛围。\n"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "5. **来源"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "标识**：画面"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "右上角带有"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "\"通义·"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "AI合成\"的水"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "印，表明"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "这是一段由"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "人工智能"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "生成/"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "合成的视频，而非"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "真人实拍"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "。\n\n**总结"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "**：这是一段"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = " AI 合成的"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "短视频"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "，内容"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "为一位穿"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "粉色开衫、"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "留"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "短发的年轻女性在"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "暖"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "色户外背景"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "前，"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "对着"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "镜头微笑并"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "做"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "说话/唱歌的口"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "型表演"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "，整体呈现"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "清新、治愈"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "的人物"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "肖像"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "效果"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "。"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = string.Empty
                                },
                                FinishReason = "stop"
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-6e426b92-deb0-900b-b3e8-ce71cc0fe054",
                        Object = "chat.completion.chunk",
                        Created = 1795703710,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>(),
                        Usage = new OpenAiCompatibleChatUsage
                        {
                            PromptTokens = 1933,
                            CompletionTokens = 482,
                            TotalTokens = 2415,
                            PromptTokensDetails = new OpenAiCompatibleChatTokenDetails
                            {
                                AudioTokens = 93,
                                TextTokens = 46,
                                VideoTokens = 1794,
                                CachedTokens = 0
                            },
                            CompletionTokensDetails = new OpenAiCompatibleChatTokenDetails
                            {
                                TextTokens = 275,
                                ReasoningTokens = 207
                            }
                        }
                    }
                });

        /// <summary>
        /// qwen3.8-omni-flash is forced to call the tool: the tool_calls of the deltas add up to the call.
        /// </summary>
        public static readonly RequestSnapshot<OpenAiCompatibleChatRequest, List<OpenAiCompatibleChatChunk>>
            OmniToolCallSse = new(
                "openai-compatible-omni-tool-call",
                new OpenAiCompatibleChatRequest
                {
                    Model = "qwen3.8-omni-flash",
                    Messages = new List<OpenAiCompatibleChatMessage>
                    {
                        new OpenAiCompatibleChatMessage
                        {
                            Role = "user",
                            Content = new List<OpenAiCompatibleChatContentPart> { new OpenAiCompatibleChatContentPart(Type: "text", Text: "北京今天天气怎么样？") }
                        }
                    },
                    StreamOptions = new OpenAiCompatibleChatStreamOptions(IncludeUsage: true),
                    ReasoningEffort = "none",
                    Tools = new List<ToolDefinition>
                    {
                        new ToolDefinition(
                            Type: "function",
                            Function: new FunctionDefinition(
                                "get_weather",
                                "查询指定城市的实时天气",
                                new
                                {
                                    type = "object",
                                    properties = new { city = new { type = "string", description = "城市名称，如北京" } },
                                    required = new[] { "city" }
                                }))
                    },
                    ToolChoice = ToolChoice.FunctionChoice("get_weather")
                },
                new List<OpenAiCompatibleChatChunk>
                {
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-79993ab7-2c69-929b-b769-3afd812638e7",
                        Object = "chat.completion.chunk",
                        Created = 1795703717,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Role = "assistant",
                                    Content = string.Empty,
                                    ToolCalls = new List<ToolCall>
                                    {
                                        new ToolCall(
                                            Id: "call_33ce5ce049474336a746100c",
                                            Type: "function",
                                            Index: 0,
                                            Function: new FunctionCall("get_weather", string.Empty))
                                    }
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-79993ab7-2c69-929b-b769-3afd812638e7",
                        Object = "chat.completion.chunk",
                        Created = 1795703717,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ToolCalls = new List<ToolCall>
                                    {
                                        new ToolCall(Id: string.Empty, Type: "function", Index: 0, Function: new FunctionCall(string.Empty, string.Empty))
                                    }
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-79993ab7-2c69-929b-b769-3afd812638e7",
                        Object = "chat.completion.chunk",
                        Created = 1795703717,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ToolCalls = new List<ToolCall>
                                    {
                                        new ToolCall(Id: string.Empty, Type: "function", Index: 0, Function: new FunctionCall(string.Empty, "{\"city\": \"北京"))
                                    }
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-79993ab7-2c69-929b-b769-3afd812638e7",
                        Object = "chat.completion.chunk",
                        Created = 1795703717,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ToolCalls = new List<ToolCall>
                                    {
                                        new ToolCall(Id: string.Empty, Type: "function", Index: 0, Function: new FunctionCall(string.Empty, "\""))
                                    }
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-79993ab7-2c69-929b-b769-3afd812638e7",
                        Object = "chat.completion.chunk",
                        Created = 1795703717,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ToolCalls = new List<ToolCall>
                                    {
                                        new ToolCall(Id: string.Empty, Type: "function", Index: 0, Function: new FunctionCall(string.Empty, "}"))
                                    }
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-79993ab7-2c69-929b-b769-3afd812638e7",
                        Object = "chat.completion.chunk",
                        Created = 1795703717,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    ToolCalls = new List<ToolCall>
                                    {
                                        new ToolCall(Id: string.Empty, Type: "function", Index: 0, Function: new FunctionCall(string.Empty, string.Empty))
                                    }
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-79993ab7-2c69-929b-b769-3afd812638e7",
                        Object = "chat.completion.chunk",
                        Created = 1795703717,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = string.Empty
                                },
                                FinishReason = "tool_calls"
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-79993ab7-2c69-929b-b769-3afd812638e7",
                        Object = "chat.completion.chunk",
                        Created = 1795703717,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>(),
                        Usage = new OpenAiCompatibleChatUsage
                        {
                            PromptTokens = 290,
                            CompletionTokens = 20,
                            TotalTokens = 310,
                            PromptTokensDetails = new OpenAiCompatibleChatTokenDetails
                            {
                                TextTokens = 290,
                                CachedTokens = 0
                            },
                            CompletionTokensDetails = new OpenAiCompatibleChatTokenDetails
                            {
                                TextTokens = 20
                            }
                        }
                    }
                });

        /// <summary>
        /// The result of the tool call is fed back to qwen3.8-omni-flash.
        /// </summary>
        public static readonly RequestSnapshot<OpenAiCompatibleChatRequest, OpenAiCompatibleChatCompletion>
            OmniToolResultNoSse = new(
                "openai-compatible-omni-tool-result",
                new OpenAiCompatibleChatRequest
                {
                    Model = "qwen3.8-omni-flash",
                    Messages = new List<OpenAiCompatibleChatMessage>
                    {
                        new OpenAiCompatibleChatMessage
                        {
                            Role = "user",
                            Content = new List<OpenAiCompatibleChatContentPart> { new OpenAiCompatibleChatContentPart(Type: "text", Text: "北京今天天气怎么样？") }
                        },
                        new OpenAiCompatibleChatMessage
                        {
                            Role = "assistant",
                            Content = new List<OpenAiCompatibleChatContentPart>(),
                            ToolCalls = new List<ToolCall>
                            {
                                new ToolCall(
                                    Id: "call_4a007421b5204f5dafb26b4e",
                                    Type: "function",
                                    Index: 0,
                                    Function: new FunctionCall("get_weather", "{\"city\": \"北京\"}"))
                            }
                        },
                        new OpenAiCompatibleChatMessage
                        {
                            Role = "tool",
                            Content = new List<OpenAiCompatibleChatContentPart>
                            {
                                new OpenAiCompatibleChatContentPart(Type: "text", Text: "{\"weather\": \"晴\", \"temperature\": \"12℃\"}")
                            },
                            ToolCallId = "call_4a007421b5204f5dafb26b4e"
                        }
                    },
                    ReasoningEffort = "none",
                    Tools = new List<ToolDefinition>
                    {
                        new ToolDefinition(
                            Type: "function",
                            Function: new FunctionDefinition(
                                "get_weather",
                                "查询指定城市的实时天气",
                                new
                                {
                                    type = "object",
                                    properties = new { city = new { type = "string", description = "城市名称，如北京" } },
                                    required = new[] { "city" }
                                }))
                    }
                },
                new OpenAiCompatibleChatCompletion
                {
                    Id = "chatcmpl-4dd95c26-827e-9ae6-80c6-5990fbc69227",
                    Object = "chat.completion",
                    Created = 1795703718,
                    Model = "qwen3.8-omni-flash",
                    Choices = new List<OpenAiCompatibleChatCompletionChoice>
                    {
                        new OpenAiCompatibleChatCompletionChoice
                        {
                            Index = 0,
                            Message = new OpenAiCompatibleChatCompletionMessage
                            {
                                Role = "assistant",
                                Content = "北京今天是晴天，气温为12℃。"
                            },
                            FinishReason = "stop"
                        }
                    },
                    Usage = new OpenAiCompatibleChatUsage
                    {
                        PromptTokens = 341,
                        CompletionTokens = 11,
                        TotalTokens = 352,
                        PromptTokensDetails = new OpenAiCompatibleChatTokenDetails
                        {
                            TextTokens = 341,
                            CachedTokens = 0
                        },
                        CompletionTokensDetails = new OpenAiCompatibleChatTokenDetails
                        {
                            TextTokens = 11
                        }
                    }
                });

        /// <summary>
        /// The web search of qwen3.8-omni-flash with the agent strategy: streaming only and served on the workspace
        /// domain only. The stream opens with a frame that carries a message instead of a delta, which the SDK
        /// hands out as a choice without a delta.
        /// </summary>
        public static readonly RequestSnapshot<OpenAiCompatibleChatRequest, List<OpenAiCompatibleChatChunk>>
            OmniSearchSse = new(
                "openai-compatible-omni-search",
                new OpenAiCompatibleChatRequest
                {
                    Model = "qwen3.8-omni-flash",
                    Messages = new List<OpenAiCompatibleChatMessage>
                    {
                        new OpenAiCompatibleChatMessage
                        {
                            Role = "user",
                            Content = new List<OpenAiCompatibleChatContentPart> { new OpenAiCompatibleChatContentPart(Type: "text", Text: "请查询今天的日期和星期。") }
                        }
                    },
                    StreamOptions = new OpenAiCompatibleChatStreamOptions(IncludeUsage: true),
                    ReasoningEffort = "none",
                    EnableSearch = true,
                    SearchOptions = new SearchOptions
                    {
                        SearchStrategy = "agent"
                    }
                },
                new List<OpenAiCompatibleChatChunk>
                {
                    new OpenAiCompatibleChatChunk
                    {
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                FinishReason = "null"
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-b77aab38-02ac-9143-99a5-d8e9482232f0",
                        Object = "chat.completion.chunk",
                        Created = 1795703721,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Role = "assistant",
                                    Content = string.Empty
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-b77aab38-02ac-9143-99a5-d8e9482232f0",
                        Object = "chat.completion.chunk",
                        Created = 1795703721,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "今天是"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-b77aab38-02ac-9143-99a5-d8e9482232f0",
                        Object = "chat.completion.chunk",
                        Created = 1795703721,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = " **"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-b77aab38-02ac-9143-99a5-d8e9482232f0",
                        Object = "chat.completion.chunk",
                        Created = 1795703721,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "2026"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-b77aab38-02ac-9143-99a5-d8e9482232f0",
                        Object = "chat.completion.chunk",
                        Created = 1795703721,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "年11月"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-b77aab38-02ac-9143-99a5-d8e9482232f0",
                        Object = "chat.completion.chunk",
                        Created = 1795703721,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "26日**"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-b77aab38-02ac-9143-99a5-d8e9482232f0",
                        Object = "chat.completion.chunk",
                        Created = 1795703721,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = "，星期四。"
                                }
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-b77aab38-02ac-9143-99a5-d8e9482232f0",
                        Object = "chat.completion.chunk",
                        Created = 1795703721,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new OpenAiCompatibleChatChoice
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Content = string.Empty
                                },
                                FinishReason = "stop"
                            }
                        }
                    },
                    new OpenAiCompatibleChatChunk
                    {
                        Id = "chatcmpl-b77aab38-02ac-9143-99a5-d8e9482232f0",
                        Object = "chat.completion.chunk",
                        Created = 1795703721,
                        Model = "qwen3.8-omni-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>(),
                        Usage = new OpenAiCompatibleChatUsage
                        {
                            PromptTokens = 337,
                            CompletionTokens = 18,
                            TotalTokens = 355,
                            PromptTokensDetails = new OpenAiCompatibleChatTokenDetails
                            {
                                TextTokens = 337,
                                CachedTokens = 0
                            },
                            CompletionTokensDetails = new OpenAiCompatibleChatTokenDetails
                            {
                                TextTokens = 18
                            }
                        }
                    }
                });

        /// <summary>
        /// The channels of the input audio are kept for qwen3.8-omni-flash.
        /// </summary>
        public static readonly RequestSnapshot<OpenAiCompatibleChatRequest, OpenAiCompatibleChatCompletion>
            OmniMultichannelNoSse = new(
                "openai-compatible-omni-multichannel",
                new OpenAiCompatibleChatRequest
                {
                    Model = "qwen3.8-omni-flash",
                    Messages = new List<OpenAiCompatibleChatMessage>
                    {
                        new OpenAiCompatibleChatMessage
                        {
                            Role = "user",
                            Content = new List<OpenAiCompatibleChatContentPart>
                            {
                                new OpenAiCompatibleChatContentPart(Type: "text", Text: "这段音频在说什么"),
                                new OpenAiCompatibleChatContentPart(
                                    Type: "input_audio",
                                    InputAudio: new OpenAiCompatibleChatInputAudio(Data: AudioUrl, Format: "wav"))
                            }
                        }
                    },
                    ReasoningEffort = "none",
                    UseMultichannel = true
                },
                new OpenAiCompatibleChatCompletion
                {
                    Id = "chatcmpl-bab70741-1c55-99ec-bfef-aa3d3f0ae1a9",
                    Object = "chat.completion",
                    Created = 1795703722,
                    Model = "qwen3.8-omni-flash",
                    Choices = new List<OpenAiCompatibleChatCompletionChoice>
                    {
                        new OpenAiCompatibleChatCompletionChoice
                        {
                            Index = 0,
                            Message = new OpenAiCompatibleChatCompletionMessage
                            {
                                Role = "assistant",
                                Content = "这段音频在描述一幅图画的内容。\n\n图画的场景是在海滩上，有一个女人和一只狗正在玩耍。背景是海浪拍打着沙滩，天空的颜色发白，讲述者推测这可能是日出或日落时分。最后，讲述者表示如果还有想了解的地方可以继续提问。"
                            },
                            FinishReason = "stop"
                        }
                    },
                    Usage = new OpenAiCompatibleChatUsage
                    {
                        PromptTokens = 130,
                        CompletionTokens = 58,
                        TotalTokens = 188,
                        PromptTokensDetails = new OpenAiCompatibleChatTokenDetails
                        {
                            AudioTokens = 114,
                            TextTokens = 16,
                            CachedTokens = 0
                        },
                        CompletionTokensDetails = new OpenAiCompatibleChatTokenDetails
                        {
                            TextTokens = 58
                        }
                    }
                });

        /// <summary>
        /// qwen3.8-omni-flash answers with a JSON object.
        /// </summary>
        public static readonly RequestSnapshot<OpenAiCompatibleChatRequest, OpenAiCompatibleChatCompletion>
            OmniJsonObjectNoSse = new(
                "openai-compatible-omni-json-object",
                new OpenAiCompatibleChatRequest
                {
                    Model = "qwen3.8-omni-flash",
                    Messages = new List<OpenAiCompatibleChatMessage>
                    {
                        new OpenAiCompatibleChatMessage
                        {
                            Role = "user",
                            Content = new List<OpenAiCompatibleChatContentPart>
                            {
                                new OpenAiCompatibleChatContentPart(
                                    Type: "input_audio",
                                    InputAudio: new OpenAiCompatibleChatInputAudio(Data: AudioUrl, Format: "wav")),
                                new OpenAiCompatibleChatContentPart(Type: "text", Text: "分析这段音频，只返回一个 JSON 对象，包含两个键：asr（音频的转写文本）和 language（语种）。")
                            }
                        }
                    },
                    Modalities = new List<string> { "text" },
                    ReasoningEffort = "none",
                    ResponseFormat = DashScopeResponseFormat.JsonObject
                },
                new OpenAiCompatibleChatCompletion
                {
                    Id = "chatcmpl-d07d834b-c3ce-9416-804d-ee14c919d568",
                    Object = "chat.completion",
                    Created = 1795703726,
                    Model = "qwen3.8-omni-flash",
                    Choices = new List<OpenAiCompatibleChatCompletionChoice>
                    {
                        new OpenAiCompatibleChatCompletionChoice
                        {
                            Index = 0,
                            Message = new OpenAiCompatibleChatCompletionMessage
                            {
                                Role = "assistant",
                                Content = "{\"asr\": \"嗯，这幅图啊是在海滩上有个女人和一只狗在玩呢。背景是海浪拍打着沙滩，天空有点发白，应该是日出或者日落的时候吧。你要是还有啥想知道的尽管问哈。\", \"language\": \"zh\" }"
                            },
                            FinishReason = "stop"
                        }
                    },
                    Usage = new OpenAiCompatibleChatUsage
                    {
                        PromptTokens = 157,
                        CompletionTokens = 58,
                        TotalTokens = 215,
                        PromptTokensDetails = new OpenAiCompatibleChatTokenDetails
                        {
                            AudioTokens = 114,
                            TextTokens = 43,
                            CachedTokens = 0
                        },
                        CompletionTokensDetails = new OpenAiCompatibleChatTokenDetails
                        {
                            TextTokens = 58
                        }
                    }
                });
    }
}
