using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Sdk;
using Microsoft.Extensions.AI;
using ChatMessage = Microsoft.Extensions.AI.ChatMessage;

namespace Cnblogs.DashScope.AI;

/// <summary>
/// <see cref="IChatClient" /> implemented with DashScope.
/// </summary>
public sealed class DashScopeChatClient : IChatClient
{
    private readonly IDashScopeClient _dashScopeClient;
    private readonly string _modelId;
    private readonly bool _useVl;

    private static readonly TextGenerationParameters
        DefaultTextGenerationParameter = new() { ResultFormat = "message" };

    /// <summary>
    /// Initialize a new instance of the
    /// </summary>
    /// <param name="dashScopeClient"></param>
    /// <param name="modelId"></param>
    /// <param name="useVl"></param>
    public DashScopeChatClient(IDashScopeClient dashScopeClient, string modelId, bool? useVl = null)
    {
        ArgumentNullException.ThrowIfNull(dashScopeClient);
        ArgumentNullException.ThrowIfNull(modelId);

        _dashScopeClient = dashScopeClient;
        _modelId = modelId;
        _useVl = useVl
                 ?? modelId.StartsWith("qwen-vl")
                 || modelId.StartsWith("qwen3-vl")
                 || modelId.StartsWith("qwen3.5")
                 || modelId.StartsWith("qwen3.6")
                 || modelId.StartsWith("qwen3-omni")
                 || modelId.StartsWith("gui-plus");
    }

    /// <summary>
    /// Gets or sets <see cref="JsonSerializerOptions"/> to use for any serialization activities related to tool call arguments and results.
    /// </summary>
    public JsonSerializerOptions ToolCallJsonSerializerOptions { get; set; } = new(JsonSerializerDefaults.Web)
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    /// <inheritdoc />
    public async Task<ChatResponse> GetResponseAsync(
        IEnumerable<ChatMessage> chatMessages,
        ChatOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        var modelId = options?.ModelId ?? _modelId;
        var useVlRaw = options?.AdditionalProperties?.GetValueOrDefault("useVl")?.ToString();
        var useVl = string.IsNullOrEmpty(useVlRaw)
            ? _useVl
            : string.Equals(useVlRaw, "true", StringComparison.OrdinalIgnoreCase);
        if (useVl)
        {
            var response = await _dashScopeClient.GetMultimodalGenerationAsync(
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Input = new MultimodalInput { Messages = chatMessages.SelectMany(ToMultimodalMessages) },
                    Parameters = ToMultimodalParameters(options),
                    Model = modelId
                },
                cancellationToken);

            var returnMessage = ToChatMessage(response.Output.Choices[0].Message);
            var completion = new ChatResponse(returnMessage)
            {
                RawRepresentation = response,
                ResponseId = response.RequestId,
                CreatedAt = DateTimeOffset.Now,
                ModelId = modelId,
                FinishReason = ToFinishReason(response.Output.Choices[0].FinishReason),
            };

            if (response.Usage != null)
            {
                completion.Usage = new UsageDetails
                {
                    InputTokenCount = response.Usage.InputTokens, OutputTokenCount = response.Usage.OutputTokens,
                };
            }

            return completion;
        }
        else
        {
            var parameters = ToTextGenerationParameters(options);
            var input = new TextGenerationInput() { Messages = chatMessages.SelectMany(ToTextChatMessages), };
            var response = await _dashScopeClient.GetTextCompletionAsync(
                new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                {
                    Input = input,
                    Model = modelId,
                    Parameters = parameters
                },
                cancellationToken);
            var returnMessage = ToChatMessage(response.Output.Choices![0].Message);
            var completion = new ChatResponse(returnMessage)
            {
                RawRepresentation = response,
                ResponseId = response.RequestId,
                CreatedAt = DateTimeOffset.Now,
                ModelId = modelId,
                FinishReason = ToFinishReason(response.Output.Choices[0].FinishReason),
            };

            if (response.Usage != null)
            {
                completion.Usage = new UsageDetails
                {
                    InputTokenCount = response.Usage.InputTokens,
                    OutputTokenCount = response.Usage.OutputTokens,
                    TotalTokenCount = response.Usage.TotalTokens,
                };
            }

            return completion;
        }
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<ChatResponseUpdate> GetStreamingResponseAsync(
        IEnumerable<ChatMessage> chatMessages,
        ChatOptions? options = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var useVlRaw = options?.AdditionalProperties?.GetValueOrDefault("useVl")?.ToString();
        var useVl = string.IsNullOrEmpty(useVlRaw)
            ? _useVl
            : string.Equals(useVlRaw, "true", StringComparison.OrdinalIgnoreCase);
        var modelId = options?.ModelId ?? _modelId;

        ChatRole? streamedRole = null;
        ChatFinishReason? finishReason = null;
        string? completionId = null;
        DateTimeOffset? createdAt = null;
        Dictionary<int, DashScopeStreamingToolCall>? functionCallInfos = null;
        if (useVl)
        {
            var parameter = ToMultimodalParameters(options);
            parameter.IncrementalOutput = true;
            var modelRequest = new ModelRequest<MultimodalInput, IMultimodalParameters>
            {
                Input = new MultimodalInput { Messages = chatMessages.SelectMany(ToMultimodalMessages) },
                Parameters = parameter,
                Model = modelId
            };
            var stream = _dashScopeClient.GetMultimodalGenerationStreamAsync(modelRequest, cancellationToken);
            await foreach (var response in stream)
            {
                streamedRole ??= string.IsNullOrEmpty(response.Output.Choices[0].Message.Role)
                    ? null
                    : ToChatRole(response.Output.Choices[0].Message.Role);
                finishReason ??= string.IsNullOrEmpty(response.Output.Choices[0].FinishReason)
                    ? null
                    : ToFinishReason(response.Output.Choices[0].FinishReason);
                completionId ??= response.RequestId;

                var update = new ChatResponseUpdate
                {
                    ResponseId = completionId,
                    CreatedAt = DateTimeOffset.Now,
                    FinishReason = finishReason,
                    ModelId = modelId,
                    RawRepresentation = response,
                    Role = streamedRole
                };

                var firstMessage = response.Output.Choices.FirstOrDefault()?.Message;
                AddReasoningIfAny(update, firstMessage?.ReasoningContent);
                AddTextIfAny(update, firstMessage?.Content.FirstOrDefault()?.Text);
                RecordToolCalls(firstMessage?.ToolCalls);

                if (response.Usage != null)
                {
                    update.Contents.Add(
                        new UsageContent(
                            new UsageDetails
                            {
                                InputTokenCount = response.Usage.InputTokens,
                                OutputTokenCount = response.Usage.OutputTokens,
                                TotalTokenCount = response.Usage.TotalTokens,
                            }) { RawRepresentation = response.Usage });
                }

                yield return update;
            }
        }
        else
        {
            var parameters = ToTextGenerationParameters(options);
            parameters.IncrementalOutput = true;
            var input = new TextGenerationInput { Messages = chatMessages.SelectMany(ToTextChatMessages), };
            var stream = _dashScopeClient.GetTextCompletionStreamAsync(
                new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                {
                    Input = input,
                    Model = modelId,
                    Parameters = parameters
                },
                cancellationToken);
            await foreach (var response in stream)
            {
                streamedRole ??= string.IsNullOrEmpty(response.Output.Choices?.FirstOrDefault()?.Message.Role)
                    ? null
                    : ToChatRole(response.Output.Choices[0].Message.Role);
                finishReason ??= string.IsNullOrEmpty(response.Output.Choices?.FirstOrDefault()?.FinishReason)
                    ? null
                    : ToFinishReason(response.Output.Choices[0].FinishReason);
                completionId ??= response.RequestId;
                createdAt ??= DateTimeOffset.Now;

                var update = new ChatResponseUpdate
                {
                    ResponseId = response.RequestId,
                    MessageId = response.RequestId,
                    CreatedAt = createdAt,
                    FinishReason = finishReason,
                    ModelId = modelId,
                    RawRepresentation = response,
                    Role = streamedRole
                };

                var firstMessage = response.Output.Choices?.FirstOrDefault()?.Message;
                AddReasoningIfAny(update, firstMessage?.ReasoningContent);
                AddTextIfAny(update, firstMessage?.Content.ToString());
                RecordToolCalls(firstMessage?.ToolCalls);

                if (response.Usage != null)
                {
                    update.Contents.Add(
                        new UsageContent(
                            new UsageDetails
                            {
                                InputTokenCount = response.Usage.InputTokens,
                                OutputTokenCount = response.Usage.OutputTokens,
                                TotalTokenCount = response.Usage.TotalTokens,
                                ReasoningTokenCount = response.Usage.OutputTokensDetails?.ReasoningTokens
                            }) { RawRepresentation = response.Usage, });
                }

                yield return update;
            }
        }

        // streaming is over, now call the function if there is any
        if (functionCallInfos is { Count: > 0 })
        {
            var responseUpdate = new ChatResponseUpdate
            {
                ResponseId = completionId,
                MessageId = completionId,
                CreatedAt = createdAt,
                FinishReason = finishReason,
                ModelId = modelId,
                Role = streamedRole,
            };

            foreach (var functionCallInfo in functionCallInfos)
            {
                var argumentsString = functionCallInfo.Value.Arguments.ToString();
                var arguments = string.IsNullOrEmpty(argumentsString)
                    ? null
                    : JsonSerializer.Deserialize<Dictionary<string, object?>>(argumentsString);
                var functionCallContent = new FunctionCallContent(
                    functionCallInfo.Value.Id ?? functionCallInfo.Key.ToString(),
                    functionCallInfo.Value.Name,
                    arguments);
                responseUpdate.Contents.Add(functionCallContent);
            }

            yield return responseUpdate;
        }

        yield break;

        void AddReasoningIfAny(ChatResponseUpdate update, string? reasoning)
        {
            if (reasoning is null)
            {
                return;
            }

            update.Contents.Add(new TextReasoningContent(reasoning));
        }

        void AddTextIfAny(ChatResponseUpdate update, string? text)
        {
            if (text is not null)
            {
                update.Contents.Add(new TextContent(text));
            }
        }

        void RecordToolCalls(List<ToolCall>? toolCalls)
        {
            if (toolCalls is null || toolCalls.Count == 0)
            {
                return;
            }

            foreach (var toolCall in toolCalls)
            {
                functionCallInfos ??= [];
                if (!functionCallInfos.TryGetValue(toolCall.Index, out var value))
                {
                    functionCallInfos[toolCall.Index] = new()
                    {
                        Id = toolCall.Id,
                        Index = toolCall.Index,
                        Name = toolCall.Function.Name,
                        Arguments = new StringBuilder(toolCall.Function.Arguments)
                    };
                }
                else
                {
                    value.Arguments.Append(toolCall.Function.Arguments);
                }
            }
        }
    }

    /// <inheritdoc />
    public object? GetService(Type serviceType, object? serviceKey = null)
    {
        return
            serviceKey is not null ? null :
            serviceType == typeof(IDashScopeClient) ? _dashScopeClient :
            serviceType.IsInstanceOfType(this) ? this :
            null;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        // nothing to dispose.
    }

    private static ChatFinishReason? ToFinishReason(string? finishReason)
        => string.IsNullOrEmpty(finishReason)
            ? null
            : finishReason switch
            {
                "stop" => ChatFinishReason.Stop,
                "length" => ChatFinishReason.Length,
                "tool_calls" => ChatFinishReason.ToolCalls,
                "null" => null,
                _ => new ChatFinishReason(finishReason),
            };

    private static ChatMessage ToChatMessage(TextChatMessage message)
    {
        var returnMessage = new ChatMessage
        {
            RawRepresentation = message, Role = ToChatRole(message.Role),
        };

        if (string.IsNullOrEmpty(message.Content) == false)
        {
            returnMessage.Contents.Add(new TextContent(message.Content));
        }

        if (message.ToolCalls is { Count: > 0 })
        {
            message.ToolCalls.ForEach(call =>
            {
                var arguments = string.IsNullOrEmpty(call.Function.Arguments)
                    ? null
                    : JsonSerializer.Deserialize<Dictionary<string, object?>>(call.Function.Arguments);
                returnMessage.Contents.Add(
                    new FunctionCallContent(
                        call.Id ?? string.Empty,
                        call.Function.Name,
                        arguments) { RawRepresentation = call });
            });
        }

        return returnMessage;
    }

    private static ChatMessage ToChatMessage(MultimodalMessage message)
    {
        var returnMessage = new ChatMessage
        {
            RawRepresentation = message, Role = ToChatRole(message.Role),
        };

        if (message.ReasoningContent != null)
        {
            returnMessage.Contents.Add(new TextReasoningContent(message.ReasoningContent));
        }

        foreach (var multimodalMessageContent in message.Content)
        {
            returnMessage.Contents.Add(new TextContent(multimodalMessageContent.Text));
        }

        if (message.ToolCalls != null)
        {
            foreach (var messageToolCall in message.ToolCalls)
            {
                var arguments = string.IsNullOrEmpty(messageToolCall.Function.Arguments)
                    ? null
                    : JsonSerializer.Deserialize<Dictionary<string, object?>>(messageToolCall.Function.Arguments);
                returnMessage.Contents.Add(
                    new FunctionCallContent(
                        messageToolCall.Id ?? messageToolCall.Index.ToString(),
                        messageToolCall.Function.Name,
                        arguments));
            }
        }

        return returnMessage;
    }

    private static ChatRole ToChatRole(string role)
        => role switch
        {
            DashScopeRoleNames.System => ChatRole.System,
            DashScopeRoleNames.User => ChatRole.User,
            DashScopeRoleNames.Assistant => ChatRole.Assistant,
            DashScopeRoleNames.Tool => ChatRole.Tool,
            _ => new ChatRole(role),
        };

    private MultimodalParameters ToMultimodalParameters(ChatOptions? options)
    {
        if (options is null)
        {
            return new MultimodalParameters();
        }

        if (options.AdditionalProperties?.GetValueOrDefault("raw") is MultimodalParameters raw)
        {
            return raw;
        }

        var parameters = new MultimodalParameters
        {
            ResponseFormat = ToDashScopeResponseFormat(options.ResponseFormat),
            Temperature = options.Temperature,
            MaxTokens = options.MaxOutputTokens,
            TopP = options.TopP,
            TopK = options.TopK,
            RepetitionPenalty = options.FrequencyPenalty,
            PresencePenalty = options.PresencePenalty,
            Seed = (ulong?)options.Seed,
            Stop = options.StopSequences == null ? null : new TextGenerationStop(options.StopSequences),
            Tools = options.Tools == null ? null : ToToolDefinitions(options.Tools),
            ToolChoice = options.ToolMode switch
            {
                AutoChatToolMode => ToolChoice.AutoChoice,
                RequiredChatToolMode required when string.IsNullOrEmpty(required.RequiredFunctionName) == false =>
                    ToolChoice.FunctionChoice(required.RequiredFunctionName),
                null => null,
                _ => ToolChoice.AutoChoice
            },
            ParallelToolCalls = options.AllowMultipleToolCalls,
        };

        return parameters;
    }

    private IEnumerable<MultimodalMessage> ToMultimodalMessages(ChatMessage from)
    {
        if (from.Role == ChatRole.System || from.Role == ChatRole.User)
        {
            var contents = ToMultimodalMessageContents(from.Contents);
            yield return from.Role == ChatRole.System
                ? MultimodalMessage.System(contents)
                : MultimodalMessage.User(contents);
        }
        else if (from.Role == ChatRole.Tool)
        {
            foreach (var content in from.Contents)
            {
                if (content is not FunctionResultContent resultContent)
                {
                    continue;
                }

                var result = resultContent.Result as string;
                if (result is null && resultContent.Result is not null)
                {
                    try
                    {
                        result = JsonSerializer.Serialize(resultContent.Result, ToolCallJsonSerializerOptions);
                    }
                    catch (NotSupportedException)
                    {
                        // If the type can't be serialized, skip it.
                    }
                }

                var contents = result is null
                    ? []
                    : new List<MultimodalMessageContent>() { MultimodalMessageContent.TextContent(result) };
                yield return MultimodalMessage.Tool(contents, resultContent.CallId);
            }
        }
        else if (from.Role == ChatRole.Assistant)
        {
            var contents = ToMultimodalMessageContents(from.Contents);
            var functionCall = from.Contents
                .OfType<FunctionCallContent>()
                .Select((c, i) => new ToolCall(
                    c.CallId,
                    "function",
                    i,
                    new FunctionCall(c.Name, JsonSerializer.Serialize(c.Arguments, ToolCallJsonSerializerOptions))))
                .ToList();
            yield return MultimodalMessage.Assistant(
                contents,
                toolCalls: functionCall.Count > 0 ? functionCall : null);
        }
    }

    private List<MultimodalMessageContent> ToMultimodalMessageContents(IList<AIContent> contents)
    {
        var mapped = new List<MultimodalMessageContent>();
        foreach (var aiContent in contents)
        {
            var content = aiContent switch
            {
                { RawRepresentation: MultimodalMessageContent raw } => raw,
                TextContent text => MultimodalMessageContent.TextContent(text.Text),
                DataContent { Data.Length: > 0 } data when data.HasTopLevelMediaType("image") =>
                    MultimodalMessageContent.ImageContent(
                        data.Data.Span,
                        data.MediaType ?? throw new InvalidOperationException("image media type should not be null")),
                DataContent { Uri: { } uri } data when data.HasTopLevelMediaType("image") =>
                    MultimodalMessageContent.ImageContent(uri),
                DataContent { Uri: { } uri } data when data.HasTopLevelMediaType("video") => MultimodalMessageContent
                    .VideoContent(uri),
                UriContent uri when uri.HasTopLevelMediaType("image") => MultimodalMessageContent.ImageContent(
                    uri.Uri.AbsoluteUri),
                _ => null
            };
            if (content is not null)
            {
                mapped.Add(content);
            }
        }

        if (mapped.Count == 0)
        {
            mapped.Add(MultimodalMessageContent.TextContent(string.Empty));
        }

        return mapped;
    }

    private IEnumerable<TextChatMessage> ToTextChatMessages(ChatMessage from)
    {
        if (from.Role == ChatRole.System || from.Role == ChatRole.User)
        {
            if (from.RawRepresentation is TextChatMessage text)
            {
                yield return text;
            }

            yield return new TextChatMessage(
                from.Role.Value,
                from.Text,
                from.AuthorName);
        }
        else if (from.Role == ChatRole.Tool)
        {
            foreach (var content in from.Contents)
            {
                if (content is FunctionResultContent resultContent)
                {
                    var result = resultContent.Result as string;
                    if (result is null && resultContent.Result is not null)
                    {
                        try
                        {
                            result = JsonSerializer.Serialize(resultContent.Result, ToolCallJsonSerializerOptions);
                        }
                        catch (NotSupportedException)
                        {
                            // If the type can't be serialized, skip it.
                        }
                    }

                    yield return new TextChatMessage(from.Role.Value, result ?? string.Empty, resultContent.CallId);
                }
            }
        }
        else if (from.Role == ChatRole.Assistant)
        {
            var functionCall = from.Contents
                .OfType<FunctionCallContent>()
                .Select((c, i) => new ToolCall(
                    c.CallId,
                    "function",
                    i,
                    new FunctionCall(c.Name, JsonSerializer.Serialize(c.Arguments, ToolCallJsonSerializerOptions))))
                .ToList();

            // function all array must be null when empty
            // <400> InternalError.Algo.InvalidParameter: Empty tool_calls is not supported in message
            yield return new TextChatMessage(
                from.Role.Value,
                from.Text,
                from.AuthorName,
                null,
                null,
                functionCall.Count > 0 ? functionCall : null);
        }
    }

    private static TextGenerationParameters ToTextGenerationParameters(ChatOptions? options)
    {
        if (options is null)
        {
            return DefaultTextGenerationParameter;
        }

        if (options.AdditionalProperties?.GetValueOrDefault("raw") is TextGenerationParameters parameters)
        {
            return parameters;
        }

        return new TextGenerationParameters
        {
            ResultFormat = "message",
            ResponseFormat = ToDashScopeResponseFormat(options.ResponseFormat),
            Temperature = options.Temperature,
            MaxTokens = options.MaxOutputTokens,
            TopP = options.TopP,
            TopK = options.TopK,
            RepetitionPenalty = options.FrequencyPenalty,
            PresencePenalty = options.PresencePenalty,
            Seed = options.Seed == null ? null : (ulong)options.Seed.Value,
            Stop = options.StopSequences == null ? null : new TextGenerationStop(options.StopSequences),
            Tools = options.Tools == null ? null : ToToolDefinitions(options.Tools),
            ToolChoice = options.ToolMode switch
            {
                AutoChatToolMode => ToolChoice.AutoChoice,
                RequiredChatToolMode required when string.IsNullOrEmpty(required.RequiredFunctionName) == false =>
                    ToolChoice.FunctionChoice(required.RequiredFunctionName),
                null => null,
                _ => ToolChoice.AutoChoice
            },
            ParallelToolCalls = options.AllowMultipleToolCalls,
        };
    }

    private static DashScopeResponseFormat? ToDashScopeResponseFormat(ChatResponseFormat? format)
    {
        return format switch
        {
            null => null,
            ChatResponseFormatJson => DashScopeResponseFormat.Json,
            ChatResponseFormatText => DashScopeResponseFormat.Text,
            _ => throw new ArgumentOutOfRangeException("Unknown response format: " + format)
        };
    }

    private static IEnumerable<ToolDefinition>? ToToolDefinitions(IList<AITool>? tools)
    {
        return tools?.OfType<AIFunction>().Select(f => new ToolDefinition(
            "function",
            new FunctionDefinition(
                f.Name,
                f.Description,
                f.JsonSchema)));
    }
}
