namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents a skill that model can use.
/// </summary>
public record DashScopeModelSkill(string Type, string? Mode, string? TemplateId)
{
    /// <summary>
    /// PPT skill in general mode.
    /// </summary>
    /// <param name="templateId">The template to use.</param>
    /// <returns></returns>
    public static DashScopeModelSkill PptGeneral(string templateId = "internet_01") => Ppt("general", templateId);

    /// <summary>
    /// PPT skill in creative mode.
    /// </summary>
    public static DashScopeModelSkill PptCreative => Ppt("creative");

    /// <summary>
    /// PPT skill.
    /// </summary>
    /// <param name="mode">The generation mode.</param>
    /// <param name="templateId">Optional template id.</param>
    /// <returns></returns>
    public static DashScopeModelSkill Ppt(string? mode, string? templateId = null) => new("ppt", mode, templateId);
}
