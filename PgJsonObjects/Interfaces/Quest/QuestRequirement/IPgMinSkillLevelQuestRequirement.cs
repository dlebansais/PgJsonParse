namespace PgJsonObjects
{
    public interface IPgMinSkillLevelQuestRequirement
    {
        Skill ConnectedSkill { get; }
        int SkillLevel { get; }
        int? RawSkillLevel { get; }
    }
}
