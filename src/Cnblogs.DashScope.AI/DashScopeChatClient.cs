using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Cnblogs.DashScope.AI.Internals;
using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Sdk;
using Microsoft.Extensions.AI;
using ChatMessage = Microsoft.Extensions.AI.ChatMessage;

namespace Cnblogs.DashScope.AI;

/// <summary>
/// <see cref="IChatClient" /> implemented with DashScope.
/// </summary>
public sealed partial class DashScopeChatClient : IChatClient
{
    private readonly IDashScopeClient _dashScopeClient;
    private readonly string _modelId;
    private readonly DashScopeChatClientOptions _options;
    private readonly DashScopeModelCache _modelCache;

    /// <summary>
    /// Initialize a new instance of the <see cref="DashScopeChatClient"/>.
    /// </summary>
    /// <param name="dashScopeClient">The DashScope client that sends the requests.</param>
    /// <param name="modelId">The model to use, <see cref="ChatOptions.ModelId"/> overrides it when set.</param>
    /// <param name="useVl">Whether to call the multimodal generation endpoint: <c>true</c> to call
    /// it for every request, <c>false</c> to call the text generation endpoint for every request.
    /// Leave the argument out to decide by the model of each request: the multimodal models (e.g.
    /// qwen3.5 / qwen3.6 series, qwen3.7-plus, qwen3.8-max, qwen3-vl-plus, gui-plus) go there, the
    /// other models go to the text generation endpoint.</param>
    public DashScopeChatClient(IDashScopeClient dashScopeClient, string modelId, bool useVl)
        : this(dashScopeClient, modelId, new DashScopeChatClientOptions(useVl), DashScopeModelCache.Instance)
    {
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="DashScopeChatClient"/>.
    /// </summary>
    /// <param name="dashScopeClient">The DashScope client that sends the requests.</param>
    /// <param name="modelId">The model to use, <see cref="ChatOptions.ModelId"/> overrides it when set.</param>
    /// <param name="options">The endpoint to call and whether to look the models up in the model
    /// list of the service, <c>null</c> (or leave it out) to decide the endpoint by the model of
    /// each request without looking models up.</param>
    public DashScopeChatClient(
        IDashScopeClient dashScopeClient,
        string modelId,
        DashScopeChatClientOptions? options = null)
        : this(dashScopeClient, modelId, options, DashScopeModelCache.Instance)
    {
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="DashScopeChatClient"/> with the cache of the
    /// models looked up in the model list.
    /// </summary>
    /// <param name="dashScopeClient">The DashScope client that sends the requests.</param>
    /// <param name="modelId">The model to use.</param>
    /// <param name="options">The endpoint to call and whether to look the models up in the model
    /// list of the service, <c>null</c> for the defaults.</param>
    /// <param name="modelCache">The cache of the looked up models, <see cref="DashScopeModelCache.Instance"/>
    /// in production.</param>
    internal DashScopeChatClient(
        IDashScopeClient dashScopeClient,
        string modelId,
        DashScopeChatClientOptions? options,
        DashScopeModelCache modelCache)
    {
        ArgumentNullException.ThrowIfNull(dashScopeClient);
        ArgumentNullException.ThrowIfNull(modelId);
        ArgumentNullException.ThrowIfNull(modelCache);

        _dashScopeClient = dashScopeClient;
        _modelId = modelId;
        _options = options ?? DashScopeChatClientOptions.Default;
        _modelCache = modelCache;
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
        if (await UseMultimodalAsync(options, modelId))
        {
            var response = await _dashScopeClient.GetMultimodalGenerationAsync(
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Input = new MultimodalInput
                    {
                        Messages = WithInstructions(chatMessages, options).SelectMany(ToMultimodalMessages)
                    },
                    Parameters = ToMultimodalParameters(options),
                    Model = modelId
                },
                cancellationToken);

            var returnMessage = ToChatMessage(response.Output.Choices[0].Message);
            var completion = new ChatResponse(returnMessage)
            {
                RawRepresentation = response,
                ResponseId = response.RequestId,
                CreatedAt = DateTimeOffset.UtcNow,
                ModelId = modelId,
                FinishReason = ToFinishReason(response.Output.Choices[0].FinishReason),
                Usage = ToUsageDetails(response.Usage),
            };

            return completion;
        }
        else
        {
            var parameters = ToTextGenerationParameters(options);
            var input = new TextGenerationInput
            {
                Messages = WithInstructions(chatMessages, options).SelectMany(ToTextChatMessages),
            };
            var response = await _dashScopeClient.GetTextCompletionAsync(
                new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                {
                    Input = input, Model = modelId, Parameters = parameters
                },
                cancellationToken);
            var returnMessage = ToChatMessage(response.Output.Choices![0].Message);
            var completion = new ChatResponse(returnMessage)
            {
                RawRepresentation = response,
                ResponseId = response.RequestId,
                CreatedAt = DateTimeOffset.UtcNow,
                ModelId = modelId,
                FinishReason = ToFinishReason(response.Output.Choices[0].FinishReason),
                Usage = ToUsageDetails(response.Usage),
            };

            return completion;
        }
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<ChatResponseUpdate> GetStreamingResponseAsync(
        IEnumerable<ChatMessage> chatMessages,
        ChatOptions? options = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var modelId = options?.ModelId ?? _modelId;
        var useVl = await UseMultimodalAsync(options, modelId);

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
                Input = new MultimodalInput
                {
                    Messages = WithInstructions(chatMessages, options).SelectMany(ToMultimodalMessages)
                },
                Parameters = parameter,
                Model = modelId
            };
            var stream = _dashScopeClient.GetMultimodalGenerationStreamAsync(modelRequest, cancellationToken);
            await foreach (var response in stream)
            {
                // the web search of qwen3.8-omni-flash opens the stream with a frame that carries
                // no output, it only announces the assistant
                if (response.Output.Choices is not { Count: > 0 })
                {
                    continue;
                }

                streamedRole ??= ToChatRole(response.Output.Choices.FirstOrDefault()?.Message.Role);
                finishReason ??= ToFinishReason(response.Output.Choices[0].FinishReason);
                completionId ??= response.RequestId;
                createdAt ??= DateTimeOffset.UtcNow;

                var update = new ChatResponseUpdate
                {
                    ResponseId = completionId,
                    MessageId = response.RequestId,
                    CreatedAt = createdAt,
                    FinishReason = finishReason,
                    ModelId = modelId,
                    RawRepresentation = response,
                    Role = streamedRole
                };

                var firstMessage = response.Output.Choices.FirstOrDefault()?.Message;
                AddReasoningIfAny(update, firstMessage?.ReasoningContent);
                AddTextIfAny(update, firstMessage?.Content.FirstOrDefault()?.Text);
                RecordToolCalls(firstMessage?.ToolCalls);

                // DashScope reports cumulative usage on every chunk, surface it only on the
                // final chunk so downstream aggregators (UsageDetails.Add) don't double count.
                if (IsFinalChunk(response.Output.Choices[0].FinishReason))
                {
                    AddUsageIfAny(update, response.Usage);
                }

                yield return update;
            }
        }
        else
        {
            var parameters = ToTextGenerationParameters(options);
            parameters.IncrementalOutput = true;
            var input = new TextGenerationInput
            {
                Messages = WithInstructions(chatMessages, options).SelectMany(ToTextChatMessages),
            };
            var stream = _dashScopeClient.GetTextCompletionStreamAsync(
                new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                {
                    Input = input, Model = modelId, Parameters = parameters
                },
                cancellationToken);
            await foreach (var response in stream)
            {
                streamedRole ??= ToChatRole(response.Output.Choices?.FirstOrDefault()?.Message.Role);
                finishReason ??= ToFinishReason(response.Output.Choices?.FirstOrDefault()?.FinishReason);
                completionId ??= response.RequestId;
                createdAt ??= DateTimeOffset.UtcNow;

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

                // DashScope reports cumulative usage on every chunk, surface it only on the
                // final chunk so downstream aggregators (UsageDetails.Add) don't double count.
                if (IsFinalChunk(response.Output.Choices?.FirstOrDefault()?.FinishReason))
                {
                    AddUsageIfAny(update, response.Usage);
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

    /// <summary>
    /// Whether the chunk is the final one of a streaming response. DashScope sets <c>finish_reason</c>
    /// of intermediate chunks to the literal string "null" and reports cumulative usage on every chunk.
    /// </summary>
    private static bool IsFinalChunk(string? finishReason) => finishReason is not null && finishReason != "null";

    private static void AddUsageIfAny(ChatResponseUpdate update, TextGenerationTokenUsage? usage)
        => AddUsageContent(update, ToUsageDetails(usage), usage);

    private static void AddUsageIfAny(ChatResponseUpdate update, MultimodalTokenUsage? usage)
        => AddUsageContent(update, ToUsageDetails(usage), usage);

    private static void AddUsageContent(ChatResponseUpdate update, UsageDetails? details, object? rawUsage)
    {
        if (details is null)
        {
            return;
        }

        update.Contents.Add(new UsageContent(details) { RawRepresentation = rawUsage });
    }

    private static UsageDetails? ToUsageDetails(MultimodalTokenUsage? usage)
    {
        if (usage is null)
        {
            return null;
        }

        var details = new UsageDetails
        {
            InputTokenCount = usage.InputTokens,
            OutputTokenCount = usage.OutputTokens,
            TotalTokenCount = usage.TotalTokens,
            // some responses (e.g. qwen-doc-turbo with doc urls) still report cached_tokens
            // flat on usage instead of inside prompt_tokens_details
            CachedInputTokenCount = usage.PromptTokensDetails?.CachedTokens ?? usage.CachedTokens,
            ReasoningTokenCount = usage.OutputTokensDetails?.ReasoningTokens,
            AdditionalCounts = []
        };

        var counts = details.AdditionalCounts;
        if (usage.InputTokensDetails is { } inputDetails)
        {
            const string inputTokensDetails = nameof(MultimodalTokenUsage.InputTokensDetails);
            AddCount(counts, $"{inputTokensDetails}.{nameof(MultimodalInputTokenDetails.ImageTokens)}", inputDetails.ImageTokens);
            AddCount(counts, $"{inputTokensDetails}.{nameof(MultimodalInputTokenDetails.VideoTokens)}", inputDetails.VideoTokens);
            AddCount(counts, $"{inputTokensDetails}.{nameof(MultimodalInputTokenDetails.TextTokens)}", inputDetails.TextTokens);
            AddCount(counts, $"{inputTokensDetails}.{nameof(MultimodalInputTokenDetails.AudioTokens)}", inputDetails.AudioTokens);
        }

        if (usage.OutputTokensDetails is { } outputDetails)
        {
            const string outputTokensDetails = nameof(MultimodalTokenUsage.OutputTokensDetails);
            AddCount(counts, $"{outputTokensDetails}.{nameof(MultimodalOutputTokenDetails.TextTokens)}", outputDetails.TextTokens);
        }

        AddCount(counts, nameof(MultimodalTokenUsage.ImageTokens), usage.ImageTokens);
        AddCount(counts, nameof(MultimodalTokenUsage.AudioTokens), usage.AudioTokens);
        AddCount(counts, nameof(MultimodalTokenUsage.VideoTokens), usage.VideoTokens);
        AddPluginUsages(counts, usage.Plugins);

        return details;
    }

    private static UsageDetails? ToUsageDetails(TextGenerationTokenUsage? usage)
    {
        if (usage is null)
        {
            return null;
        }

        var details = new UsageDetails
        {
            InputTokenCount = usage.InputTokens,
            OutputTokenCount = usage.OutputTokens,
            TotalTokenCount = usage.TotalTokens,
            // some responses (e.g. qwen-doc-turbo with doc urls) still report cached_tokens
            // flat on usage instead of inside prompt_tokens_details
            CachedInputTokenCount = usage.PromptTokensDetails?.CachedTokens ?? usage.CachedTokens,
            ReasoningTokenCount = usage.OutputTokensDetails?.ReasoningTokens,
            AdditionalCounts = []
        };

        var counts = details.AdditionalCounts;
        if (usage.OutputTokensDetails is { } outputDetails)
        {
            const string outputTokensDetails = nameof(TextGenerationTokenUsage.OutputTokensDetails);
            AddCount(counts, $"{outputTokensDetails}.{nameof(TextGenerationOutputTokenDetails.TextTokens)}", outputDetails.TextTokens);
        }

        AddPluginUsages(counts, usage.Plugins);
        return details;
    }

    /// <summary>
    /// Plugin usages are provider specific, the numeric counts go into <see cref="UsageDetails.AdditionalCounts"/>
    /// with "Plugins." prefixed keys; non-numeric fields stay in <c>RawRepresentation</c> only.
    /// </summary>
    private static void AddPluginUsages(AdditionalPropertiesDictionary<long> counts, DashScopePluginUsages? plugins)
    {
        if (plugins?.Search is { } search)
        {
            counts.Add("Plugins.Search.Count", search.Count);
        }

        if (plugins?.ImageSearch is { } imageSearch)
        {
            counts.Add("Plugins.ImageSearch.Count", imageSearch.Count);
        }

        if (plugins?.CodeInterpreter is { } codeInterpreter)
        {
            counts.Add("Plugins.CodeInterpreter.Count", codeInterpreter.Count);
        }
    }

    private static void AddCount(AdditionalPropertiesDictionary<long> counts, string key, int? value)
    {
        if (value is { } count)
        {
            counts.Add(key, count);
        }
    }

    private static ChatMessage ToChatMessage(TextChatMessage message)
    {
        var returnMessage = new ChatMessage { RawRepresentation = message, Role = ToChatRole(message.Role).Value, };

        if (message.ReasoningContent != null)
        {
            returnMessage.Contents.Add(new TextReasoningContent(message.ReasoningContent));
        }

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
        var returnMessage = new ChatMessage { RawRepresentation = message, Role = ToChatRole(message.Role).Value, };

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

    [return: NotNullIfNotNull(nameof(role))]
    private static ChatRole? ToChatRole(string? role)
        => role switch
        {
            DashScopeRoleNames.System => ChatRole.System,
            DashScopeRoleNames.User => ChatRole.User,
            DashScopeRoleNames.Assistant => ChatRole.Assistant,
            DashScopeRoleNames.Tool => ChatRole.Tool,
            null => null,
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

        var parameters = new MultimodalParameters();
        MapChatOptions(parameters, options);
        return parameters;
    }

    /// <summary>
    /// Prepends <see cref="ChatOptions.Instructions"/> as a system message, matching how other
    /// M.E.AI providers (e.g. OpenAI) turn instructions into a leading system chat message.
    /// Blank instructions are skipped.
    /// </summary>
    private static IEnumerable<ChatMessage> WithInstructions(
        IEnumerable<ChatMessage> chatMessages,
        ChatOptions? options)
    {
        if (options?.Instructions is { } instructions && string.IsNullOrWhiteSpace(instructions) == false)
        {
            yield return new ChatMessage(ChatRole.System, instructions);
        }

        foreach (var chatMessage in chatMessages)
        {
            yield return chatMessage;
        }
    }

    private IEnumerable<MultimodalMessage> ToMultimodalMessages(ChatMessage from)
    {
        if (from.Role == ChatRole.System || from.Role == ChatRole.User)
        {
            if (from.RawRepresentation is MultimodalMessage raw)
            {
                yield return raw;
            }
            else
            {
                var contents = ToMultimodalMessageContents(from.Contents);
                yield return from.Role == ChatRole.System
                    ? MultimodalMessage.System(contents)
                    : MultimodalMessage.User(contents);
            }
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
                DataContent { Data.Length: > 0 } data when data.HasTopLevelMediaType("audio") =>
                    MultimodalMessageContent.AudioContent(
                        data.Data.Span,
                        data.MediaType ?? throw new InvalidOperationException("audio media type should not be null")),
                DataContent { Uri: { } uri } data when data.HasTopLevelMediaType("audio") =>
                    MultimodalMessageContent.AudioContent(uri),
                UriContent uri when uri.HasTopLevelMediaType("audio") => MultimodalMessageContent.AudioContent(
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
            else
            {
                yield return new TextChatMessage(
                    from.Role.Value,
                    from.Text,
                    from.AuthorName);
            }
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

    private TextGenerationParameters ToTextGenerationParameters(ChatOptions? options)
    {
        if (options is null)
        {
            return DefaultTextGenerationParameter;
        }

        if (options.AdditionalProperties?.GetValueOrDefault("raw") is TextGenerationParameters parameters)
        {
            return parameters;
        }

        parameters = new TextGenerationParameters { ResultFormat = "message" };
        MapChatOptions(parameters, options);
        return parameters;
    }

    /// <summary>
    /// Maps shared <see cref="ChatOptions"/> onto the parameter interfaces implemented by both
    /// <see cref="TextGenerationParameters"/> and <see cref="MultimodalParameters"/>.
    /// </summary>
    private void MapChatOptions<TParameters>(TParameters parameters, ChatOptions options)
        where TParameters : IProbabilityParameter,
        IPenaltyParameter,
        IMaxTokenParameter,
        IStopTokenParameter,
        ISeedParameter,
        IStructuredOutputParameter,
        IThinkingParameter,
        IFunctionCallParameter
    {
        MapFromChatOptions((IProbabilityParameter)parameters, options);
        MapFromChatOptions((IPenaltyParameter)parameters, options);
        MapMaxOutputTokens((IMaxTokenParameter)parameters, options);
        MapFromChatOptions((IStopTokenParameter)parameters, options);
        MapFromChatOptions((ISeedParameter)parameters, options);
        MapFromChatOptions((IStructuredOutputParameter)parameters, options);
        MapFromChatOptions((IThinkingParameter)parameters, options);
        MapFromChatOptions((IFunctionCallParameter)parameters, options);
    }

    /// <summary>
    /// Resolves whether <see cref="ChatOptions.MaxOutputTokens"/> is sent as <c>max_completion_tokens</c>.
    /// Defaults to models on the whitelist and can be overridden via the "useMaxCompletionTokens"
    /// additional property.
    /// </summary>
    private static bool UseMaxCompletionTokens(ChatOptions? options, string modelId)
    {
        var raw = options?.AdditionalProperties?.GetValueOrDefault("useMaxCompletionTokens")?.ToString();
        if (string.IsNullOrEmpty(raw) == false)
        {
            return string.Equals(raw, "true", StringComparison.OrdinalIgnoreCase);
        }

        return MaxCompletionTokenModelPrefixes.Any(prefix => modelId.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Resolves whether the request goes to the multimodal generation endpoint. The "useVl"
    /// additional property and <see cref="DashScopeChatClientOptions.UseVl"/> win over everything
    /// else, and no model is looked up then. Without them the model looked up in the model list of
    /// the service decides when <see cref="DashScopeChatClientOptions.AutoSyncModelList"/> is on,
    /// and the model prefixes known to this SDK decide for the rest: the option is off, the model
    /// was not looked up, or the service does not list it.
    /// </summary>
    private async Task<bool> UseMultimodalAsync(ChatOptions? options, string modelId)
    {
        var raw = options?.AdditionalProperties?.GetValueOrDefault("useVl")?.ToString();
        if (string.IsNullOrEmpty(raw) == false)
        {
            return string.Equals(raw, "true", StringComparison.OrdinalIgnoreCase);
        }

        if (_options.UseVl.HasValue)
        {
            return _options.UseVl.Value;
        }

        if (_options.AutoSyncModelList)
        {
            var model = await _modelCache.GetModelAsync(_dashScopeClient, modelId);
            if (model != null)
            {
                return IsMultimodal(model);
            }
        }

        return MultimodalModelPrefixes.Any(prefix => modelId.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Whether a model of the model list is served by the multimodal generation endpoint: the
    /// models hosted by Bailian that understand images (the "VU" and "Multimodal-Omni"
    /// capabilities). The models supplied directly by third parties are served by the text
    /// generation endpoint even when they understand images.
    /// </summary>
    private static bool IsMultimodal(DashScopeModel model)
        => model.InferenceProvider == "aliyun-bailian"
           && (model.Capabilities.Contains("VU") || model.Capabilities.Contains("Multimodal-Omni"));

    /// <summary>
    /// Resolves whether <see cref="ChatResponseFormatJson.Schema"/> should be sent as <c>json_schema</c>.
    /// Defaults to models on the whitelist and can be overridden via the "useJsonSchema" additional property.
    /// </summary>
    private static bool UseJsonSchema(ChatOptions? options, string modelId)
    {
        var raw = options?.AdditionalProperties?.GetValueOrDefault("useJsonSchema")?.ToString();
        if (string.IsNullOrEmpty(raw) == false)
        {
            return string.Equals(raw, "true", StringComparison.OrdinalIgnoreCase);
        }

        return JsonSchemaModelPrefixes.Any(prefix => modelId.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
    }

    private static void MapFromChatOptions(IProbabilityParameter parameters, ChatOptions options)
    {
        parameters.Temperature = options.Temperature;
        parameters.TopP = options.TopP;
        parameters.TopK = options.TopK;
    }

    private static void MapFromChatOptions(IPenaltyParameter parameters, ChatOptions options)
    {
        parameters.RepetitionPenalty = options.FrequencyPenalty;
        parameters.PresencePenalty = options.PresencePenalty;
    }

    private void MapMaxOutputTokens(IMaxTokenParameter parameters, ChatOptions options)
    {
        // max_completion_tokens limits the reasoning output and the answer together, but only the
        // models of the whitelist read it, the others silently ignore it and read max_tokens only
        if (UseMaxCompletionTokens(options, options.ModelId ?? _modelId))
        {
            parameters.MaxCompletionTokens = options.MaxOutputTokens;
        }
        else
        {
            parameters.MaxTokens = options.MaxOutputTokens;
        }
    }

    private static void MapFromChatOptions(IStopTokenParameter parameters, ChatOptions options)
    {
        parameters.Stop = options.StopSequences == null ? null : new TextGenerationStop(options.StopSequences);
    }

    private static void MapFromChatOptions(ISeedParameter parameters, ChatOptions options)
    {
        parameters.Seed = (ulong?)options.Seed;
    }

    private void MapFromChatOptions(IStructuredOutputParameter parameters, ChatOptions options)
    {
        var useJsonSchema = UseJsonSchema(options, options.ModelId ?? _modelId);
        parameters.ResponseFormat = ToDashScopeResponseFormat(options.ResponseFormat, useJsonSchema);
    }

    private static void MapFromChatOptions(IThinkingParameter parameters, ChatOptions options)
    {
        parameters.EnableThinking = options.Reasoning?.Effort switch
        {
            null => null,
            ReasoningEffort.None => false,
            _ => true
        };
        parameters.ReasoningEffort = options.Reasoning?.Effort switch
        {
            ReasoningEffort.Low => "low",
            ReasoningEffort.Medium => "medium",
            ReasoningEffort.High => "high",
            ReasoningEffort.ExtraHigh => "xhigh",
            _ => null
        };
    }

    private static void MapFromChatOptions(IFunctionCallParameter parameters, ChatOptions options)
    {
        parameters.Tools = options.Tools == null ? null : ToToolDefinitions(options.Tools);
        parameters.ToolChoice = options.ToolMode switch
        {
            AutoChatToolMode => ToolChoice.AutoChoice,
            RequiredChatToolMode required when string.IsNullOrEmpty(required.RequiredFunctionName) == false =>
                ToolChoice.FunctionChoice(required.RequiredFunctionName),
            NoneChatToolMode => ToolChoice.NoneChoice,
            null => null,
            _ => ToolChoice.AutoChoice
        };
        parameters.ParallelToolCalls = options.AllowMultipleToolCalls;
    }

    private static DashScopeResponseFormat? ToDashScopeResponseFormat(ChatResponseFormat? format, bool useJsonSchema)
    {
        // SchemaName defaults to "response" since DashScope requires a name while MSEAI allows null;
        // SchemaDescription is dropped because the DashScope json_schema object has no such field.
        return format switch
        {
            null => null,
            ChatResponseFormatJson { Schema: not null } json when useJsonSchema => DashScopeResponseFormat.JsonSchema(
                new DashScopeJsonSchema(json.SchemaName ?? "response", json.Schema.Value)),
            ChatResponseFormatJson => DashScopeResponseFormat.JsonObject,
            ChatResponseFormatText => DashScopeResponseFormat.Text,
            _ => throw new ArgumentOutOfRangeException("Unknown response format: " + format)
        };
    }

    private static IEnumerable<ToolDefinition>? ToToolDefinitions(IList<AITool>? tools)
    {
        return tools?.OfType<AIFunctionDeclaration>().Select(f => new ToolDefinition(
            "function",
            new FunctionDefinition(
                f.Name,
                f.Description,
                f.JsonSchema)));
    }
}
