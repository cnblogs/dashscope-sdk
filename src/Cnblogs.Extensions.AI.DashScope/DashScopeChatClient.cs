using System.Runtime.CompilerServices;
using System.Text.Json;
using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Sdk;
using Json.Schema;
using Microsoft.Extensions.AI;
using ChatMessage = Microsoft.Extensions.AI.ChatMessage;

namespace Cnblogs.Extensions.AI.DashScope;

/// <summary>
/// <see cref="IChatClient" /> implemented with DashScope.
/// </summary>
public sealed class DashScopeChatClient : IChatClient
{
    private readonly IDashScopeClient _dashScopeClient;
    private readonly string _modelId;

    private static readonly JsonSchema EmptyObjectSchema =
        JsonSchema.FromText("""{"type":"object","required":[],"properties":{}}""");

    private static readonly TextGenerationParameters
        DefaultTextGenerationParameter = new() { ResultFormat = "message" };

    /// <summary>
    /// Initialize a new instance of the
    /// </summary>
    /// <param name="dashScopeClient"></param>
    /// <param name="modelId"></param>
    public DashScopeChatClient(IDashScopeClient dashScopeClient, string modelId)
    {
        ArgumentNullException.ThrowIfNull(dashScopeClient, nameof(dashScopeClient));
        ArgumentNullException.ThrowIfNull(modelId, nameof(modelId));

        _dashScopeClient = dashScopeClient;
        _modelId = modelId;
        Metadata = new ChatClientMetadata("dashscope", _dashScopeClient.BaseAddress, _modelId);
    }

    /// <summary>
    /// Gets or sets <see cref="JsonSerializerOptions"/> to use for any serialization activities related to tool call arguments and results.
    /// </summary>
    public JsonSerializerOptions ToolCallJsonSerializerOptions { get; set; } = new(JsonSerializerDefaults.Web);

    /// <inheritdoc />
    public async Task<ChatCompletion> CompleteAsync(
        IList<ChatMessage> chatMessages,
        ChatOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        var useVlRaw = options?.AdditionalProperties?.GetValueOrDefault("useVl")?.ToString();
        var useVl = string.IsNullOrEmpty(useVlRaw)
            ? chatMessages.Any(c => c.Contents.Any(m => m is ImageContent))
            : string.Equals(useVlRaw, "true", StringComparison.OrdinalIgnoreCase);
        var modelId = options?.ModelId ?? _modelId;
        if (useVl)
        {
            var response = await _dashScopeClient.GetMultimodalGenerationAsync(
                new ModelRequest<MultimodalInput, IMultimodalParameters>()
                {
                    Input = new MultimodalInput { Messages = ToMultimodalMessages(chatMessages) },
                    Parameters = ToMultimodalParameters(options),
                    Model = modelId
                },
                cancellationToken);

            var returnMessage = new ChatMessage()
            {
                RawRepresentation = response, Role = ToChatRole(response.Output.Choices[0].Message.Role),
            };

            returnMessage.Contents.Add(new TextContent(response.Output.Choices[0].Message.Content[0].Text));
            var completion = new ChatCompletion(returnMessage)
            {
                RawRepresentation = response,
                CompletionId = response.RequestId,
                CreatedAt = DateTimeOffset.Now,
                ModelId = modelId,
                FinishReason = ToFinishReason(response.Output.Choices[0].FinishReason),
            };

            if (response.Usage != null)
            {
                completion.Usage = new UsageDetails()
                {
                    InputTokenCount = response.Usage.InputTokens, OutputTokenCount = response.Usage.OutputTokens,
                };
            }

            return completion;
        }
        else
        {
            var parameters = ToTextGenerationParameters(options) ?? DefaultTextGenerationParameter;
            var response = await _dashScopeClient.GetTextCompletionAsync(
                new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
                {
                    Input = new TextGenerationInput
                    {
                        Messages = chatMessages.SelectMany(
                            c => ToTextChatMessages(c, parameters.Tools?.ToList())),
                        Tools = ToToolDefinitions(options?.Tools)
                    },
                    Model = modelId,
                    Parameters = parameters
                },
                cancellationToken);
            var returnMessage = ToChatMessage(response.Output.Choices![0].Message);
            var completion = new ChatCompletion(returnMessage)
            {
                RawRepresentation = response,
                CompletionId = response.RequestId,
                CreatedAt = DateTimeOffset.Now,
                ModelId = modelId,
                FinishReason = ToFinishReason(response.Output.FinishReason),
            };

            if (response.Usage != null)
            {
                completion.Usage = new UsageDetails()
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
    public async IAsyncEnumerable<StreamingChatCompletionUpdate> CompleteStreamingAsync(
        IList<ChatMessage> chatMessages,
        ChatOptions? options = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var useVlRaw = options?.AdditionalProperties?.GetValueOrDefault("useVl")?.ToString();
        var useVl = string.IsNullOrEmpty(useVlRaw)
            ? chatMessages.Any(c => c.Contents.Any(m => m is ImageContent))
            : string.Equals(useVlRaw, "true", StringComparison.OrdinalIgnoreCase);
        var modelId = options?.ModelId ?? _modelId;

        ChatRole? streamedRole = null;
        ChatFinishReason? finishReason = null;
        string? completionId = null;
        if (useVl)
        {
            var parameter = ToMultimodalParameters(options);
            parameter.IncrementalOutput = true;
            var stream = _dashScopeClient.GetMultimodalGenerationStreamAsync(
                new ModelRequest<MultimodalInput, IMultimodalParameters>()
                {
                    Input = new MultimodalInput { Messages = ToMultimodalMessages(chatMessages) },
                    Parameters = parameter,
                    Model = modelId
                },
                cancellationToken);
            await foreach (var response in stream)
            {
                streamedRole ??= string.IsNullOrEmpty(response.Output.Choices[0].Message.Role)
                    ? null
                    : ToChatRole(response.Output.Choices[0].Message.Role);
                finishReason ??= string.IsNullOrEmpty(response.Output.Choices[0].FinishReason)
                    ? null
                    : ToFinishReason(response.Output.Choices[0].FinishReason);
                completionId ??= response.RequestId;

                var update = new StreamingChatCompletionUpdate()
                {
                    CompletionId = completionId,
                    CreatedAt = DateTimeOffset.Now,
                    FinishReason = finishReason,
                    ModelId = modelId,
                    RawRepresentation = response,
                    Role = streamedRole
                };

                if (response.Output.Choices[0].Message.Content is { Count: > 0 })
                {
                    update.Contents.Add(new TextContent(response.Output.Choices[0].Message.Content[0].Text));
                }

                if (response.Usage != null)
                {
                    update.Contents.Add(
                        new UsageContent(
                            new UsageDetails()
                            {
                                InputTokenCount = response.Usage.InputTokens,
                                OutputTokenCount = response.Usage.OutputTokens,
                            }));
                }

                yield return update;
            }
        }
        else
        {
            if (options?.Tools is { Count: > 0 })
            {
                // qwen does not support streaming with function call, fallback to non-streaming
                var completion = await CompleteAsync(chatMessages, options, cancellationToken);
                yield return new StreamingChatCompletionUpdate()
                {
                    CompletionId = completion.CompletionId,
                    Role = completion.Message.Role,
                    AdditionalProperties = completion.AdditionalProperties,
                    Contents = completion.Message.Contents,
                    RawRepresentation = completion.Message.RawRepresentation,
                    CreatedAt = completion.CreatedAt,
                    FinishReason = completion.FinishReason,
                    ModelId = completion.ModelId,
                };
            }

            var parameters = ToTextGenerationParameters(options) ?? DefaultTextGenerationParameter;
            parameters.IncrementalOutput = true;
            var stream = _dashScopeClient.GetTextCompletionStreamAsync(
                new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
                {
                    Input = new TextGenerationInput
                    {
                        Messages = chatMessages.SelectMany(
                            c => ToTextChatMessages(c, parameters.Tools?.ToList())),
                        Tools = ToToolDefinitions(options?.Tools)
                    },
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

                var update = new StreamingChatCompletionUpdate()
                {
                    CompletionId = completionId,
                    CreatedAt = DateTimeOffset.Now,
                    FinishReason = finishReason,
                    ModelId = modelId,
                    RawRepresentation = response,
                    Role = streamedRole
                };

                if (response.Output.Choices?.FirstOrDefault()?.Message.Content is { Length: > 0 })
                {
                    update.Contents.Add(new TextContent(response.Output.Choices[0].Message.Content));
                }

                if (response.Usage != null)
                {
                    update.Contents.Add(
                        new UsageContent(
                            new UsageDetails()
                            {
                                InputTokenCount = response.Usage.InputTokens,
                                OutputTokenCount = response.Usage.OutputTokens,
                            }));
                }

                yield return update;
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

    /// <inheritdoc />
    public ChatClientMetadata Metadata { get; }

    private static ChatFinishReason? ToFinishReason(string? finishReason)
        => string.IsNullOrEmpty(finishReason)
            ? null
            : finishReason switch
            {
                "stop" => ChatFinishReason.Stop,
                "length" => ChatFinishReason.ContentFilter,
                "tool_calls" => ChatFinishReason.ToolCalls,
                _ => new ChatFinishReason(finishReason),
            };

    private static ChatMessage ToChatMessage(Cnblogs.DashScope.Core.ChatMessage message)
    {
        var returnMessage = new ChatMessage()
        {
            RawRepresentation = message, Role = ToChatRole(message.Role),
        };

        if (string.IsNullOrEmpty(message.Content) == false)
        {
            returnMessage.Contents.Add(new TextContent(message.Content));
        }

        if (message.ToolCalls is { Count: > 0 })
        {
            message.ToolCalls.ForEach(
                call =>
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
        var parameters = new MultimodalParameters();
        if (options is null)
        {
            return parameters;
        }

        parameters.Temperature = options.Temperature;
        parameters.MaxTokens = options.MaxOutputTokens;
        parameters.TopP = options.TopP;
        parameters.TopK = options.TopK;
        parameters.RepetitionPenalty = options.FrequencyPenalty;
        parameters.PresencePenalty = options.PresencePenalty;
        parameters.Seed = (ulong?)options.Seed;
        if (options.StopSequences is { Count: > 0 })
        {
            parameters.Stop = new TextGenerationStop(options.StopSequences);
        }

        return parameters;
    }

    private IEnumerable<MultimodalMessage> ToMultimodalMessages(IEnumerable<ChatMessage> messages)
    {
        foreach (var from in messages)
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
                // do not support tool.
            }
            else if (from.Role == ChatRole.Assistant)
            {
                var contents = ToMultimodalMessageContents(from.Contents);
                yield return MultimodalMessage.Assistant(contents);
            }
        }
    }

    private List<MultimodalMessageContent> ToMultimodalMessageContents(IList<AIContent> contents)
    {
        var mapped = new List<MultimodalMessageContent>();
        foreach (var aiContent in contents)
        {
            var content = aiContent switch
            {
                TextContent text => MultimodalMessageContent.TextContent(text.Text),
                ImageContent { Data.Length: > 0 } image => MultimodalMessageContent.ImageContent(
                    image.Data.Value.Span,
                    image.MediaType ?? throw new InvalidOperationException("image media type should not be null")),
                ImageContent { Uri: { } uri } => MultimodalMessageContent.ImageContent(uri),
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

    private IEnumerable<Cnblogs.DashScope.Core.ChatMessage> ToTextChatMessages(
        ChatMessage from,
        List<ToolDefinition>? tools)
    {
        if (from.Role == ChatRole.System || from.Role == ChatRole.User)
        {
            yield return new Cnblogs.DashScope.Core.ChatMessage(
                from.Role.Value,
                from.Text ?? string.Empty,
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

                    yield return new Cnblogs.DashScope.Core.ChatMessage(from.Role.Value, result ?? string.Empty);
                }
            }
        }
        else if (from.Role == ChatRole.Assistant)
        {
            var functionCall = from.Contents
                .OfType<FunctionCallContent>()
                .Select(
                    c => new ToolCall(
                        c.CallId,
                        "function",
                        tools?.FindIndex(f => f.Function?.Name == c.Name) ?? -1,
                        new FunctionCall(c.Name, JsonSerializer.Serialize(c.Arguments, ToolCallJsonSerializerOptions))))
                .ToList();
            yield return new Cnblogs.DashScope.Core.ChatMessage(
                from.Role.Value,
                from.Text ?? string.Empty,
                from.AuthorName,
                null,
                functionCall);
        }
    }

    private static TextGenerationParameters? ToTextGenerationParameters(ChatOptions? options)
    {
        if (options is null)
        {
            return null;
        }

        var format = "message";
        if (options.ResponseFormat is ChatResponseFormatJson)
        {
            format = "json_object";
        }

        return new TextGenerationParameters()
        {
            ResultFormat = format,
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
                _ => ToolChoice.AutoChoice
            }
        };
    }

    private static IEnumerable<ToolDefinition>? ToToolDefinitions(IList<AITool>? tools)
    {
        return tools?.OfType<AIFunction>().Select(
            f => new ToolDefinition(
                "function",
                new FunctionDefinition(
                    f.Metadata.Name,
                    f.Metadata.Description,
                    GetParameterSchema(f.Metadata.Parameters))));
    }

    private static JsonSchema GetParameterSchema(IEnumerable<AIFunctionParameterMetadata> metadata)
    {
        return new JsonSchemaBuilder()
            .Properties(metadata.Select(c => (c.Name, Schema: c.Schema as JsonSchema ?? EmptyObjectSchema)).ToArray())
            .Build();
    }
}
