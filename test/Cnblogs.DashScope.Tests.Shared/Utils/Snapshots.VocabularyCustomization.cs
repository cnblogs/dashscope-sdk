using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static class VocabularyCustomization
    {
        public static readonly RequestSnapshot<ModelRequest<VocabularyCustomizationInput>,
                ModelResponse<VocabularyCustomizationOutput, VocabularyCustomizationUsage>>
            CreateNoSse = new(
                "vocabulary-customization-create",
                new ModelRequest<VocabularyCustomizationInput>
                {
                    Model = "speech-biasing",
                    Input = new VocabularyCustomizationInput
                    {
                        Action = "create_vocabulary",
                        TargetModel = "fun-asr",
                        Prefix = "sdkvocab",
                        Vocabulary = new List<VocabularyWord> { new(Text: "赛德克巴莱", Weight: 4) }
                    }
                },
                new ModelResponse<VocabularyCustomizationOutput, VocabularyCustomizationUsage>
                {
                    Output = new VocabularyCustomizationOutput(
                        VocabularyId: "vocab-sdkvocab-b504d3226635431782b5fa299cc52523"),
                    Usage = new VocabularyCustomizationUsage { Count = 1 },
                    RequestId = "79d7f5b4-cfc2-97d9-8217-7d3f3f0298b0"
                });

        public static readonly RequestSnapshot<ModelRequest<VocabularyCustomizationInput>,
                ModelResponse<VocabularyCustomizationOutput, VocabularyCustomizationUsage>>
            ListNoSse = new(
                "vocabulary-customization-list",
                new ModelRequest<VocabularyCustomizationInput>
                {
                    Model = "speech-biasing",
                    Input = new VocabularyCustomizationInput
                    {
                        Action = "list_vocabulary",
                        Prefix = "sdkvocab",
                        PageIndex = 0,
                        PageSize = 10
                    }
                },
                // The list covers a vocabulary modified by update_vocabulary (earlier
                // gmt_modified) and the one just created by the create fixture.
                new ModelResponse<VocabularyCustomizationOutput, VocabularyCustomizationUsage>
                {
                    Output = new VocabularyCustomizationOutput(
                        VocabularyList: new List<VocabularyCustomizationVocabulary>
                        {
                            new(
                                VocabularyId: "vocab-sdkvocab-8e75374c66aa49b790f6ebb583883c80",
                                GmtCreate: "2026-11-23 10:55:39",
                                GmtModified: "2026-11-23 10:56:35",
                                Status: "OK"),
                            new(
                                VocabularyId: "vocab-sdkvocab-b504d3226635431782b5fa299cc52523",
                                GmtCreate: "2026-11-23 10:58:14",
                                GmtModified: "2026-11-23 10:58:14",
                                Status: "OK")
                        }),
                    Usage = new VocabularyCustomizationUsage { Count = 1 },
                    RequestId = "791da25e-6373-93cf-b054-deb6dd8f4320"
                });

        public static readonly RequestSnapshot<ModelRequest<VocabularyCustomizationInput>,
                ModelResponse<VocabularyCustomizationOutput, VocabularyCustomizationUsage>>
            UpdateNoSse = new(
                "vocabulary-customization-update",
                new ModelRequest<VocabularyCustomizationInput>
                {
                    Model = "speech-biasing",
                    Input = new VocabularyCustomizationInput
                    {
                        Action = "update_vocabulary",
                        VocabularyId = "vocab-sdkvocab-8e75374c66aa49b790f6ebb583883c80",
                        Vocabulary = new List<VocabularyWord>
                        {
                            new(Text: "赛德克巴莱", Weight: 4, Lang: "zh"),
                            new(Text: "kesnowy", Weight: 3, Lang: "en")
                        }
                    }
                },
                // The update replaces the whole vocabulary and returns an empty output.
                new ModelResponse<VocabularyCustomizationOutput, VocabularyCustomizationUsage>
                {
                    Output = new VocabularyCustomizationOutput(),
                    Usage = new VocabularyCustomizationUsage { Count = 1 },
                    RequestId = "ceba7ae3-5cca-99c0-9699-88288bc3ad98"
                });

        public static readonly RequestSnapshot<ModelRequest<VocabularyCustomizationInput>,
                ModelResponse<VocabularyCustomizationOutput, VocabularyCustomizationUsage>>
            QueryNoSse = new(
                "vocabulary-customization-query",
                new ModelRequest<VocabularyCustomizationInput>
                {
                    Model = "speech-biasing",
                    Input = new VocabularyCustomizationInput
                    {
                        Action = "query_vocabulary",
                        VocabularyId = "vocab-sdkvocab-8e75374c66aa49b790f6ebb583883c80"
                    }
                },
                // Captured after the update fixture: the hotwords carry the lang echo.
                new ModelResponse<VocabularyCustomizationOutput, VocabularyCustomizationUsage>
                {
                    Output = new VocabularyCustomizationOutput(
                        GmtCreate: "2026-11-23 10:55:39",
                        GmtModified: "2026-11-23 10:58:31",
                        Status: "OK",
                        TargetModel: "fun-asr",
                        Vocabulary: new List<VocabularyWord>
                        {
                            new(Text: "赛德克巴莱", Weight: 4, Lang: "zh"),
                            new(Text: "kesnowy", Weight: 3, Lang: "en")
                        }),
                    Usage = new VocabularyCustomizationUsage { Count = 1 },
                    RequestId = "0d2e2fa3-3e6c-9a72-8187-be0dbc10801c"
                });

        public static readonly RequestSnapshot<ModelRequest<VocabularyCustomizationInput>,
                ModelResponse<VocabularyCustomizationOutput, VocabularyCustomizationUsage>>
            DeleteNoSse = new(
                "vocabulary-customization-delete",
                new ModelRequest<VocabularyCustomizationInput>
                {
                    Model = "speech-biasing",
                    Input = new VocabularyCustomizationInput
                    {
                        Action = "delete_vocabulary",
                        VocabularyId = "vocab-sdkvocab-8e75374c66aa49b790f6ebb583883c80"
                    }
                },
                new ModelResponse<VocabularyCustomizationOutput, VocabularyCustomizationUsage>
                {
                    Output = new VocabularyCustomizationOutput(),
                    Usage = new VocabularyCustomizationUsage { Count = 1 },
                    RequestId = "531524e4-c1bf-91a9-863f-36b3ce381eea"
                });
    }
}
