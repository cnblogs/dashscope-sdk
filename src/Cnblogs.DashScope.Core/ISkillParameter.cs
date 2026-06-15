namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents skill parameters for supported models.
/// </summary>
public interface ISkillParameter
{
    /// <summary>
    /// The skill to use.
    /// </summary>
    List<DashScopeModelSkill>? Skill { get; set; }
}
