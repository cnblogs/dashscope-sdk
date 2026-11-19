namespace Cnblogs.DashScope.Core;

/// <summary>
/// One shot segment of a customized multi-shot Kling video synthesis task.
/// </summary>
/// <param name="Index">The index of the segment, starting from 1. Up to 6 segments.</param>
/// <param name="Prompt">The prompt of the segment, Chinese or English, up to 512 characters (truncated).</param>
/// <param name="Duration">The duration of the segment in seconds, range 1 to the requested total duration.</param>
public record VideoSynthesisMultiPrompt(int Index, string Prompt, int Duration);
