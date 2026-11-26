namespace Cnblogs.DashScope.Core.Internals;

/// <summary>
/// The wire shape of the model list response. The success flag and the error fields are handled by
/// <see cref="DashScopeClientCore.ListModelsAsync"/> and are not part of the public type.
/// </summary>
internal record ModelListEnvelope(
    string? RequestId,
    bool? Success,
    string? Code,
    string? Message,
    DashScopeModelPage? Output);
