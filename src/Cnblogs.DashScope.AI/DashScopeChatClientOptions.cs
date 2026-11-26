using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.AI;

/// <summary>
/// The options of <see cref="DashScopeChatClient"/>.
/// </summary>
/// <param name="UseVl">Whether to call the multimodal generation endpoint. Leave it <c>null</c> to
/// decide by the model of each request: the multimodal models go there, the other models go to the
/// text generation endpoint. When <paramref name="AutoSyncModelList"/> is on, the decision follows
/// the model list of the service, otherwise the model prefixes known to this SDK.</param>
/// <param name="AutoSyncModelList">Whether to look a model up in the model list of the service
/// (<see cref="IDashScopeClient.ListModelsAsync"/>) the first time it is used, so that the endpoint
/// is picked by what the service reports about the model instead of the model prefixes known to
/// this SDK. The looked up models are cached for the lifetime of the process and shared by every
/// chat client of it. When the service does not list the model, or the lookup fails, the chat
/// requests go on with the model prefixes known to this SDK and the model is not looked up again
/// for a minute.</param>
public record DashScopeChatClientOptions(bool? UseVl = null, bool AutoSyncModelList = false)
{
    /// <summary>
    /// The options of the chat clients created without options.
    /// </summary>
    internal static DashScopeChatClientOptions Default { get; } = new();
}
