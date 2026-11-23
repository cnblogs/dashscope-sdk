using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static class OpenAiCompatibleChat
    {
        private const string LivetranslateCompletionId = "chatcmpl-7ccfad76-b5da-9bb7-91d6-609b570b6dc6";

        private const int LivetranslateCreated = 1795415680;

        private const string LivetranslateAudioId = "audio_7ccfad76-b5da-9bb7-91d6-609b570b6dc6";

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
        // only in the streaming mode. The stream was captured with -TrimAudioDeltas: the text
        // deltas are complete, while of the audio deltas only the first and the last are kept,
        // the base64 payload of the first one replaced by a placeholder (the last one is empty
        // on the wire, it only carries the finish reason).
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
                LivetranslateChunks());

        private static List<OpenAiCompatibleChatChunk> LivetranslateChunks()
        {
            // the content deltas of the translation, one chunk each
            string[] deltas =
            {
                "This", " picture", " is", " of", " a", " woman", " and", " a",
                " dog", " playing", " on", " the", " beach", ".", " The", " background",
                " is", " waves", " l", "apping", " against", " the", " sand", ",",
                " and", " the", " sky", " is", " a", " bit", " pale", ",",
                " probably", " at", " sunrise", " or", " sunset", ".", " If", " there",
                "'s", " anything", " else", " you", " want", " to", " know", ",",
                " just", " ask", "."
            };
            var chunks = new List<OpenAiCompatibleChatChunk>
            {
                LivetranslateChunk(new OpenAiCompatibleChatDelta { Role = "assistant", Content = string.Empty })
            };
            chunks.AddRange(deltas.Select(d => LivetranslateChunk(new OpenAiCompatibleChatDelta { Content = d })));
            chunks.Add(LivetranslateChunk(
                new OpenAiCompatibleChatDelta
                {
                    Audio = new OpenAiCompatibleChatDeltaAudio(
                        Data: "<base64>",
                        Id: LivetranslateAudioId,
                        ExpiresAt: 1795415680)
                }));
            chunks.Add(LivetranslateChunk(
                new OpenAiCompatibleChatDelta
                {
                    Audio = new OpenAiCompatibleChatDeltaAudio(
                        Data: string.Empty,
                        Id: LivetranslateAudioId,
                        ExpiresAt: 1795415682)
                },
                finishReason: "stop"));
            chunks.Add(LivetranslateChunk(
                new OpenAiCompatibleChatDelta { Content = string.Empty },
                finishReason: "stop"));
            chunks.Add(
                new OpenAiCompatibleChatChunk
                {
                    Id = LivetranslateCompletionId,
                    Object = "chat.completion.chunk",
                    Created = LivetranslateCreated,
                    Model = "qwen3-livetranslate-flash",
                    Choices = new List<OpenAiCompatibleChatChoice>(),
                    Usage = new OpenAiCompatibleChatUsage
                    {
                        PromptTokens = 415,
                        CompletionTokens = 239,
                        TotalTokens = 654,
                        PromptTokensDetails = new OpenAiCompatibleChatTokenDetails
                        {
                            AudioTokens = 415,
                            TextTokens = 0
                        },
                        CompletionTokensDetails = new OpenAiCompatibleChatTokenDetails
                        {
                            AudioTokens = 188,
                            TextTokens = 51
                        }
                    }
                });
            return chunks;
        }

        private static OpenAiCompatibleChatChunk LivetranslateChunk(
            OpenAiCompatibleChatDelta delta,
            string? finishReason = null)
        {
            return new OpenAiCompatibleChatChunk
            {
                Id = LivetranslateCompletionId,
                Object = "chat.completion.chunk",
                Created = LivetranslateCreated,
                Model = "qwen3-livetranslate-flash",
                Choices = new List<OpenAiCompatibleChatChoice>
                {
                    new() { Index = 0, Delta = delta, FinishReason = finishReason }
                }
            };
        }
    }
}
