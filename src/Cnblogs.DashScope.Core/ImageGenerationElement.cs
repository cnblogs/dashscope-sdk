namespace Cnblogs.DashScope.Core;

/// <summary>
/// One subject (element) referenced by a Kling image generation task.
/// </summary>
/// <param name="ElementId">The id of the subject, obtained from the Kling subject list page of the Bailian documentation.</param>
public record ImageGenerationElement(int ElementId);
