using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static class OpenAiCompatibleChat
    {
        public static readonly RequestSnapshot<OpenAiCompatibleChatRequest, OpenAiCompatibleChatCompletion>
            ChatNoSse = new(
                "openai-compatible-chat",
                new OpenAiCompatibleChatRequest
                {
                    Model = "qwen-flash",
                    Messages = new List<OpenAiCompatibleChatMessage>
                    {
                        new()
                        {
                            Role = "user",
                            Content = new List<OpenAiCompatibleChatContentPart>
                            {
                                new(Type: "text", Text: "用一句话介绍博客园")
                            }
                        }
                    },
                    Temperature = 0.7f,
                    TopP = 0.8f
                },
                new OpenAiCompatibleChatCompletion
                {
                    Id = "chatcmpl-b808206a-2e28-9547-b2ff-3b54bb9b9d38",
                    Object = "chat.completion",
                    Created = 1795409846,
                    Model = "qwen-flash",
                    Choices = new List<OpenAiCompatibleChatCompletionChoice>
                    {
                        new()
                        {
                            Index = 0,
                            FinishReason = "stop",
                            Message = new OpenAiCompatibleChatCompletionMessage
                            {
                                Role = "assistant",
                                Content = "博客园是中国领先的开发者技术社区，致力于为程序员提供分享技术、交流经验、展示成果的平台。"
                            }
                        }
                    },
                    Usage = new OpenAiCompatibleChatUsage
                    {
                        PromptTokens = 13,
                        CompletionTokens = 23,
                        TotalTokens = 36,
                        PromptTokensDetails = new OpenAiCompatibleChatTokenDetails { CachedTokens = 0 }
                    }
                });

        // The qwen3-livetranslate-flash models only work via the OpenAI compatible endpoint and
        // only in the streaming mode. The response kept the role chunk, the first two content
        // deltas, the first / last audio deltas (data replaced by a placeholder, 38 chunks with
        // large base64 payloads on the wire), the stop chunk and the usage chunk.
        public static readonly RequestSnapshot<OpenAiCompatibleChatRequest, List<OpenAiCompatibleChatChunk>>
            LivetranslateSse = new(
                "openai-compatible-livetranslate",
                new OpenAiCompatibleChatRequest
                {
                    Model = "qwen3-livetranslate-flash",
                    Messages = new List<OpenAiCompatibleChatMessage>
                    {
                        new()
                        {
                            Role = "user",
                            Content = new List<OpenAiCompatibleChatContentPart>
                            {
                                new(
                                    Type: "input_audio",
                                    InputAudio: new OpenAiCompatibleChatInputAudio(
                                        Data: "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250211/tixcef/cherry.wav",
                                        Format: "wav"))
                            }
                        }
                    },
                    Modalities = new List<string> { "text", "audio" },
                    Audio = new OpenAiCompatibleChatAudioOptions(Voice: "Cherry", Format: "wav"),
                    StreamOptions = new OpenAiCompatibleChatStreamOptions(IncludeUsage: true),
                    TranslationOptions = new OpenAiCompatibleTranslationOptions(
                        SourceLang: "zh",
                        TargetLang: "en")
                },
                new List<OpenAiCompatibleChatChunk>
                {
                    new()
                    {
                        Id = "chatcmpl-467e4c01-b1cc-9356-a408-26523c24e217",
                        Object = "chat.completion.chunk",
                        Created = 1795409849,
                        Model = "qwen3-livetranslate-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new() { Index = 0, Delta = new OpenAiCompatibleChatDelta { Role = "assistant", Content = "" } }
                        }
                    },
                    new()
                    {
                        Id = "chatcmpl-467e4c01-b1cc-9356-a408-26523c24e217",
                        Object = "chat.completion.chunk",
                        Created = 1795409849,
                        Model = "qwen3-livetranslate-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new() { Index = 0, Delta = new OpenAiCompatibleChatDelta { Content = "This" } }
                        }
                    },
                    new()
                    {
                        Id = "chatcmpl-467e4c01-b1cc-9356-a408-26523c24e217",
                        Object = "chat.completion.chunk",
                        Created = 1795409849,
                        Model = "qwen3-livetranslate-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new() { Index = 0, Delta = new OpenAiCompatibleChatDelta { Content = " picture" } }
                        }
                    },
                    new()
                    {
                        Id = "chatcmpl-467e4c01-b1cc-9356-a408-26523c24e217",
                        Object = "chat.completion.chunk",
                        Created = 1795409849,
                        Model = "qwen3-livetranslate-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new()
                            {
                                Index = 0,
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Audio = new OpenAiCompatibleChatDeltaAudio(
                                        Data: "<base64>",
                                        Id: "audio_467e4c01-b1cc-9356-a408-26523c24e217",
                                        ExpiresAt: 1795409849)
                                }
                            }
                        }
                    },
                    new()
                    {
                        Id = "chatcmpl-467e4c01-b1cc-9356-a408-26523c24e217",
                        Object = "chat.completion.chunk",
                        Created = 1795409849,
                        Model = "qwen3-livetranslate-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new()
                            {
                                Index = 0,
                                FinishReason = "stop",
                                Delta = new OpenAiCompatibleChatDelta
                                {
                                    Audio = new OpenAiCompatibleChatDeltaAudio(
                                        Data: "<base64>",
                                        Id: "audio_467e4c01-b1cc-9356-a408-26523c24e217",
                                        ExpiresAt: 1795409852)
                                }
                            }
                        }
                    },
                    new()
                    {
                        Id = "chatcmpl-467e4c01-b1cc-9356-a408-26523c24e217",
                        Object = "chat.completion.chunk",
                        Created = 1795409849,
                        Model = "qwen3-livetranslate-flash",
                        Choices = new List<OpenAiCompatibleChatChoice>
                        {
                            new() { Index = 0, FinishReason = "stop", Delta = new OpenAiCompatibleChatDelta { Content = "" } }
                        }
                    },
                    new()
                    {
                        Id = "chatcmpl-467e4c01-b1cc-9356-a408-26523c24e217",
                        Object = "chat.completion.chunk",
                        Created = 1795409849,
                        Model = "qwen3-livetranslate-flash",
                        Usage = new OpenAiCompatibleChatUsage
                        {
                            PromptTokens = 415,
                            CompletionTokens = 246,
                            TotalTokens = 661,
                            PromptTokensDetails = new OpenAiCompatibleChatTokenDetails
                            {
                                AudioTokens = 415,
                                TextTokens = 0
                            },
                            CompletionTokensDetails = new OpenAiCompatibleChatTokenDetails
                            {
                                AudioTokens = 195,
                                TextTokens = 51
                            }
                        }
                    }
                });
    }
}
