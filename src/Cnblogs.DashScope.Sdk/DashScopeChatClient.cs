using System.Text.Json;
using Cnblogs.DashScope.Core;
using Json.Schema;
using Json.Schema.Generation;
using Microsoft.Extensions.AI;
using ChatMessage = Microsoft.Extensions.AI.ChatMessage;

namespace Cnblogs.DashScope.Sdk;

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
        var useVl = options?.AdditionalProperties?.GetValueOrDefault("useVl") ?? false;
        if (useVl)
        {
            var response = await _dashScopeClient.GetMultimodalGenerationAsync(
                new ModelRequest<MultimodalInput, IMultimodalParameters>()
                {
                    Input = new MultimodalInput() { Messages =  }
                })
        }
        else
        {
            var parameters = ToTextGenerationParameters(options) ?? DefaultTextGenerationParameter;
            var response = await _dashScopeClient.GetTextCompletionAsync(
                new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
                {
                    Input = new TextGenerationInput()
                    {
                        Messages = chatMessages.SelectMany(ToTextChatMessages),
                        Tools = ToToolDefinitions(options?.Tools)
                    },
                    Model = _modelId,
                    Parameters = parameters
                },
                cancellationToken);

        }
    }

    /// <inheritdoc />
    public IAsyncEnumerable<StreamingChatCompletionUpdate> CompleteStreamingAsync(
        IList<ChatMessage> chatMessages,
        ChatOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public object? GetService(Type serviceType, object? serviceKey = null)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        // nothing to dispose.
    }

    /// <inheritdoc />
    public ChatClientMetadata Metadata { get; }

    private IEnumerable<Cnblogs.DashScope.Core.ChatMessage> ToTextChatMessages(ChatMessage from)
    {
        if (from.Role == ChatRole.System || from.Role == ChatRole.User)
        {
            yield return new Core.ChatMessage(from.Role.Value, from.Text ?? string.Empty, from.AuthorName);
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

                    yield return new Core.ChatMessage(from.Role.Value, result ?? string.Empty);
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
                        new FunctionCall(c.Name, JsonSerializer.Serialize(c.Arguments, ToolCallJsonSerializerOptions))))
                .ToList();
            yield return new Core.ChatMessage(
                from.Role.Value,
                from.Text ?? string.Empty,
                from.AuthorName,
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

    private static List<ChatMessageContentPart> GetContentParts(IList<AIContent> contents)
    {
        List<ChatMessageContentPart> parts = [];
        foreach (var content in contents)
        {
            switch (content)
            {
                case TextContent textContent:
                    parts.Add(ChatMessageContentPart.CreateTextPart(textContent.Text));
                    break;

                case ImageContent imageContent when imageContent.Data is { IsEmpty: false } data:
                    parts.Add(
                        ChatMessageContentPart.CreateImagePart(BinaryData.FromBytes(data), imageContent.MediaType));
                    break;

                case ImageContent imageContent when imageContent.Uri is string uri:
                    parts.Add(ChatMessageContentPart.CreateImagePart(new Uri(uri)));
                    break;
            }
        }

        if (parts.Count == 0)
        {
            parts.Add(ChatMessageContentPart.CreateTextPart(string.Empty));
        }

        return parts;
    }
}
