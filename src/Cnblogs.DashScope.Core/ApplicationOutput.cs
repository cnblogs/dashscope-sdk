namespace Cnblogs.DashScope.Core;

/// <summary>
/// The output of application call.
/// </summary>
/// <param name="Text">Output text from application.</param>
/// <param name="FinishReason">Finish reason from application.</param>
/// <param name="SessionId">Unique id of current session.</param>
/// <param name="Thoughts">Thoughts from application.</param>
/// <param name="DocReferences">Doc references from application output.</param>
public record ApplicationOutput(
    string Text,
    string FinishReason,
    string SessionId,
    List<ApplicationOutputThought>? Thoughts,
    List<ApplicationDocReference>? DocReferences);
