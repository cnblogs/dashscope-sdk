using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils
{
    public static partial class Snapshots
    {
        public static class Error
        {
            public static readonly
                RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>, DashScopeError>
                AuthError = new(
                    "auth-error",
                    new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                    {
                        Model = "qwen-max",
                        Input = new TextGenerationInput { Prompt = "请问 1+1 是多少？" },
                        Parameters = new TextGenerationParameters
                        {
                            ResultFormat = "text",
                            Seed = 1234,
                            MaxTokens = 1500,
                            TopP = 0.8f,
                            TopK = 100,
                            RepetitionPenalty = 1.1f,
                            Temperature = 0.85f,
                            Stop = new[] { new[] { 37763, 367 } },
                            EnableSearch = false,
                            IncrementalOutput = false
                        }
                    },
                    new DashScopeError
                    {
                        Code = "InvalidApiKey",
                        Message = "Invalid API-key provided.",
                        RequestId = "a1c0561c-1dfe-98a6-a62f-983577b8bc5e"
                    });

            public static readonly
                RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>, DashScopeError>
                ParameterError = new(
                    "parameter-error",
                    new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                    {
                        Model = "qwen-max",
                        Input = new TextGenerationInput
                        {
                            Prompt = "请问 1+1 是多少？", Messages =
                                new List<TextChatMessage>()
                        },
                        Parameters = new TextGenerationParameters
                        {
                            ResultFormat = "text",
                            Seed = 1234,
                            MaxTokens = 1500,
                            TopP = 0.8f,
                            TopK = 100,
                            RepetitionPenalty = 1.1f,
                            Temperature = 0.85f,
                            Stop = new[] { new[] { 37763, 367 } },
                            EnableSearch = false,
                            IncrementalOutput = false
                        }
                    },
                    new DashScopeError
                    {
                        Code = "InvalidParameter",
                        Message = "Role must be user or assistant and Content length must be greater than 0",
                        RequestId = "a5898c04-d210-901b-965f-e4bd90478805"
                    });

            public static readonly
                RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>, DashScopeError>
                ParameterErrorSse = new(
                    "parameter-error",
                    new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                    {
                        Model = "qwen-max",
                        Input = new TextGenerationInput
                        {
                            Prompt = "请问 1+1 是多少？", Messages =
                                new List<TextChatMessage>()
                        },
                        Parameters = new TextGenerationParameters
                        {
                            ResultFormat = "text",
                            Seed = 1234,
                            MaxTokens = 1500,
                            TopP = 0.8f,
                            TopK = 100,
                            RepetitionPenalty = 1.1f,
                            Temperature = 0.85f,
                            Stop = new[] { new[] { 37763, 367 } },
                            EnableSearch = false,
                            IncrementalOutput = true
                        }
                    },
                    new DashScopeError
                    {
                        Code = "InvalidParameter",
                        Message = "Role must be user or assistant and Content length must be greater than 0",
                        RequestId = "7671ecd8-93cc-9ee9-bc89-739f0fd8b809"
                    });

            public static readonly RequestSnapshot<DashScopeError> UploadErrorNoSse = new(
                "upload-file-error",
                new DashScopeError
                {
                    Code = "invalid_request_error",
                    Message = "'purpose' must be 'file-extract'",
                    RequestId = string.Empty
                });
        }
    }
}
