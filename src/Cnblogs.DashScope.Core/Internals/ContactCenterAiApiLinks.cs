namespace Cnblogs.DashScope.Core.Internals;

internal static class ContactCenterAiApiLinks
{
    public static string AnalyzeConversation(string workspaceId, string appId)
        => $"/{workspaceId}/ccai/app/{appId}/analyze_conversation";

    public static string RunCompletion(string workspaceId, string appId)
        => $"/{workspaceId}/ccai/app/{appId}/completion";

    public static string RunCompletionMessage(string workspaceId, string appId)
        => $"/{workspaceId}/ccai/app/{appId}/completion_message";

    public static string AnalyzeImage(string workspaceId, string appId)
        => $"/{workspaceId}/ccai/app/{appId}/analyzeImage";

    public static string GeneralAnalyzeImage(string workspaceId, string appId)
        => $"/{workspaceId}/ccai/app/{appId}/generalanalyzeImage";

    public static string CreateTask(string workspaceId, string appId)
        => $"/{workspaceId}/ccai/app/{appId}/createTask";

    public const string GetTaskResult = "/ccai/app/getTaskResult";

    public const string CreateVocab = "/vocab/createVocab";
    public const string UpdateVocab = "/vocab/updateVocab";
    public const string ListVocab = "/vocab/listVocab";
    public const string DeleteVocab = "/vocab/deleteVocab";
    public const string GetVocab = "/vocab/getVocab";
}
