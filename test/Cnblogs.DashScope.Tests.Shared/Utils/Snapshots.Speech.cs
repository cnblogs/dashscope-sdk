using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static class SpeechTranscription
    {
        public static readonly RequestSnapshot<
                ModelRequest<SpeechTranscriptionInput, ISpeechTranscriptionParameters>,
                ModelResponse<SpeechTranscriptionOutput, SpeechTranscriptionUsage>>
            CreateTask = new(
                "speech-transcription-create",
                new ModelRequest<SpeechTranscriptionInput, ISpeechTranscriptionParameters>
                {
                    Model = "paraformer-v2",
                    Input = new SpeechTranscriptionInput
                    {
                        FileUrls = new[] { "https://example.com/audio/sample.wav" }
                    },
                    Parameters = new SpeechTranscriptionParameters
                    {
                        ChannelId = new[] { 0 },
                        LanguageHints = new[] { "zh", "en" }
                    }
                },
                new ModelResponse<SpeechTranscriptionOutput, SpeechTranscriptionUsage>
                {
                    Output = new SpeechTranscriptionOutput
                    {
                        TaskId = "c2e5d63b-96e1-4607-bb91-1234567890ab",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "77ae55ae-be17-97b8-9942-1234567890ab"
                });

        public static readonly RequestSnapshot<
                ModelRequest<SpeechTranscriptionInput, ISpeechTranscriptionParameters>,
                ModelResponse<SpeechTranscriptionOutput, SpeechTranscriptionUsage>>
            CreateTaskWithOss = new(
                "speech-transcription-create-oss",
                new ModelRequest<SpeechTranscriptionInput, ISpeechTranscriptionParameters>
                {
                    Model = "fun-asr",
                    Input = new SpeechTranscriptionInput
                    {
                        FileUrls = new[] { "oss://dashscope-instant/sample.wav" }
                    },
                    Parameters = new SpeechTranscriptionParameters
                    {
                        Vocabulary = new Dictionary<string, int> { ["阿里巴巴"] = 4 },
                        DiarizationEnabled = true
                    }
                },
                new ModelResponse<SpeechTranscriptionOutput, SpeechTranscriptionUsage>
                {
                    Output = new SpeechTranscriptionOutput
                    {
                        TaskId = "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "req-oss-transcription-001"
                });

        public static readonly RequestSnapshot<DashScopeTask<SpeechTranscriptionOutput, SpeechTranscriptionUsage>>
            GetTaskSuccess = new(
                "speech-transcription-get-task",
                new DashScopeTask<SpeechTranscriptionOutput, SpeechTranscriptionUsage>(
                    "f9e1afad-94d3-997e-a83b-1234567890ab",
                    new SpeechTranscriptionOutput
                    {
                        TaskId = "c2e5d63b-96e1-4607-bb91-1234567890ab",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2024, 9, 12, 15, 11, 40, 41),
                        ScheduledTime = new DateTime(2024, 9, 12, 15, 11, 40, 71),
                        EndTime = new DateTime(2024, 9, 12, 15, 11, 40, 903),
                        Results =
                            new List<SpeechTranscriptionSubtaskResult>
                            {
                                new(
                                    "https://example.com/audio/sample.wav",
                                    "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/result.json",
                                    DashScopeTaskStatus.Succeeded)
                            },
                        TaskMetrics = new DashScopeTaskMetrics(1, 1, 0)
                    },
                    new SpeechTranscriptionUsage(9)));

        public static readonly RequestSnapshot<SpeechTranscriptionFileResult> DownloadResult = new(
            "speech-transcription-result",
            new SpeechTranscriptionFileResult(
                "https://example.com/audio/sample.wav",
                new SpeechTranscriptionFileProperties("pcm_s16le", new List<int> { 0 }, 16000, 3834),
                new List<SpeechTranscriptionTranscript>
                {
                    new(
                        0,
                        3720,
                        "Hello world",
                        new List<SpeechTranscriptionSentence>
                        {
                            new(
                                100,
                                3820,
                                "Hello world",
                                1,
                                Words: new List<SpeechTranscriptionWord>
                                {
                                    new(100, 596, "Hello ", ""),
                                    new(596, 844, "world", "")
                                })
                        })
                }));
    }

    public static class SpeechRecognition
    {
        public static readonly RequestSnapshot<
                ModelRequest<SpeechRecognitionInput, ISpeechRecognitionParameters>,
                ModelResponse<SpeechRecognitionOutput, SpeechRecognitionUsage>>
            FlashNoSse = new(
                "speech-recognition-flash",
                new ModelRequest<SpeechRecognitionInput, ISpeechRecognitionParameters>
                {
                    Model = "fun-asr-flash-2026-06-15",
                    Input = new SpeechRecognitionInput
                    {
                        Messages = new[]
                        {
                            SpeechRecognitionMessage.User(
                            [
                                SpeechRecognitionMessageContent.InputAudioContent(
                                    "https://example.com/audio/sample.wav")
                            ])
                        }
                    },
                    Parameters = new SpeechRecognitionParameters
                    {
                        Format = "wav",
                        SampleRate = "16000",
                        LanguageHints = new[] { "zh" }
                    }
                },
                new ModelResponse<SpeechRecognitionOutput, SpeechRecognitionUsage>
                {
                    Output = new SpeechRecognitionOutput(
                        "Hello world",
                        new SpeechRecognitionSentence(
                            1,
                            true,
                            0,
                            1200,
                            "Hello world",
                            0,
                            new List<SpeechRecognitionWord>
                            {
                                new("Hello", 0, 500, " ", true),
                                new("world", 500, 1200, "", true)
                            })),
                    Usage = new SpeechRecognitionUsage(2),
                    RequestId = "recog-flash-req-001"
                });

        public static readonly RequestSnapshot<
                ModelRequest<SpeechRecognitionInput, ISpeechRecognitionParameters>,
                ModelResponse<SpeechRecognitionOutput, SpeechRecognitionUsage>>
            FlashSse = new(
                "speech-recognition-flash",
                new ModelRequest<SpeechRecognitionInput, ISpeechRecognitionParameters>
                {
                    Model = "qwen-audio-3.0-asr-flash",
                    Input = new SpeechRecognitionInput
                    {
                        Messages = new[]
                        {
                            SpeechRecognitionMessage.User(
                            [
                                SpeechRecognitionMessageContent.InputAudioContent(
                                    "https://example.com/audio/long.wav")
                            ])
                        }
                    },
                    Parameters = new SpeechRecognitionParameters { Format = "wav" }
                },
                new ModelResponse<SpeechRecognitionOutput, SpeechRecognitionUsage>
                {
                    Output = new SpeechRecognitionOutput(
                        "Hello world",
                        new SpeechRecognitionSentence(
                            1,
                            true,
                            0,
                            1200,
                            "Hello world",
                            0,
                            new List<SpeechRecognitionWord>
                            {
                                new("Hello", 0, 500, " ", true),
                                new("world", 500, 1200, "", true)
                            })),
                    Usage = new SpeechRecognitionUsage(2),
                    RequestId = "recog-flash-sse-001"
                });
    }

    public static class SpeechVocabulary
    {
        public static readonly RequestSnapshot<
                ModelRequest<SpeechVocabularyInput>,
                ModelResponse<SpeechVocabularyCreateOutput, SpeechVocabularyUsage>>
            Create = new(
                "speech-vocabulary-create",
                new ModelRequest<SpeechVocabularyInput>
                {
                    Model = SpeechVocabularyModels.SpeechBiasing,
                    Input = SpeechVocabularyInput.Create(
                        "paraformer-v2",
                        "test",
                        new[] { new SpeechVocabularyItem("阿里巴巴", 4, "zh") })
                },
                new ModelResponse<SpeechVocabularyCreateOutput, SpeechVocabularyUsage>
                {
                    Output = new SpeechVocabularyCreateOutput("test1234567890abcdef"),
                    Usage = new SpeechVocabularyUsage(1),
                    RequestId = "vocab-create-001"
                });

        public static readonly RequestSnapshot<
                ModelRequest<SpeechVocabularyInput>,
                ModelResponse<SpeechVocabularyListOutput, SpeechVocabularyUsage>>
            List = new(
                "speech-vocabulary-list",
                new ModelRequest<SpeechVocabularyInput>
                {
                    Model = SpeechVocabularyModels.SpeechBiasing,
                    Input = SpeechVocabularyInput.List("test", 0, 10)
                },
                new ModelResponse<SpeechVocabularyListOutput, SpeechVocabularyUsage>
                {
                    Output = new SpeechVocabularyListOutput(
                        new List<SpeechVocabularyListItem>
                        {
                            new("test1234567890abcdef", "2024-01-01 00:00:00", "2024-01-02 00:00:00", "OK")
                        }),
                    Usage = new SpeechVocabularyUsage(1),
                    RequestId = "vocab-list-001"
                });

        public static readonly RequestSnapshot<
                ModelRequest<SpeechVocabularyInput>,
                ModelResponse<SpeechVocabularyQueryOutput, SpeechVocabularyUsage>>
            Query = new(
                "speech-vocabulary-query",
                new ModelRequest<SpeechVocabularyInput>
                {
                    Model = SpeechVocabularyModels.SpeechBiasing,
                    Input = SpeechVocabularyInput.Query("test1234567890abcdef")
                },
                new ModelResponse<SpeechVocabularyQueryOutput, SpeechVocabularyUsage>
                {
                    Output = new SpeechVocabularyQueryOutput(
                        "2024-01-01 00:00:00",
                        "2024-01-02 00:00:00",
                        "OK",
                        "paraformer-v2",
                        new List<SpeechVocabularyItem> { new("阿里巴巴", 4, "zh") }),
                    Usage = new SpeechVocabularyUsage(1),
                    RequestId = "vocab-query-001"
                });

        public static readonly RequestSnapshot<
                ModelRequest<SpeechVocabularyInput>,
                ModelResponse<SpeechVocabularyMutationOutput, SpeechVocabularyUsage>>
            Update = new(
                "speech-vocabulary-update",
                new ModelRequest<SpeechVocabularyInput>
                {
                    Model = SpeechVocabularyModels.SpeechBiasing,
                    Input = SpeechVocabularyInput.Update(
                        "test1234567890abcdef",
                        new[] { new SpeechVocabularyItem("通义千问", 5, "zh") })
                },
                new ModelResponse<SpeechVocabularyMutationOutput, SpeechVocabularyUsage>
                {
                    Output = new SpeechVocabularyMutationOutput(),
                    Usage = new SpeechVocabularyUsage(1),
                    RequestId = "vocab-update-001"
                });

        public static readonly RequestSnapshot<
                ModelRequest<SpeechVocabularyInput>,
                ModelResponse<SpeechVocabularyMutationOutput, SpeechVocabularyUsage>>
            Delete = new(
                "speech-vocabulary-delete",
                new ModelRequest<SpeechVocabularyInput>
                {
                    Model = SpeechVocabularyModels.SpeechBiasing,
                    Input = SpeechVocabularyInput.Delete("test1234567890abcdef")
                },
                new ModelResponse<SpeechVocabularyMutationOutput, SpeechVocabularyUsage>
                {
                    Output = new SpeechVocabularyMutationOutput(),
                    Usage = new SpeechVocabularyUsage(1),
                    RequestId = "vocab-delete-001"
                });
    }
}
