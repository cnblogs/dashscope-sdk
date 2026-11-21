namespace Cnblogs.DashScope.Core;

/// <summary>
/// The output of a Tripo 3D task, shared by all Tripo models on the 3d-generation endpoint.
/// </summary>
public record Tripo3DOutput : DashScopeTaskOutput
{
    /// <summary>
    /// The results of the task, present only when the task succeeded. All urls are signed
    /// temporary links valid for 2 hours.
    /// </summary>
    public List<Tripo3DResult>? Results { get; set; }

    /// <summary>
    /// The original prompt of the generation, returned by Tripo-H3.1 / Tripo-P1.0.
    /// </summary>
    public string? OrigPrompt { get; set; }
}
