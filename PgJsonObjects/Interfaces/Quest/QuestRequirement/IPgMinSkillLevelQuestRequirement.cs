namespace PgJsonObjects
{
    public interface IPgMinSkillLevelQuestRequirement
    {
        IPgSkill Skill { get; }
        int SkillLevel { get; }
        int? RawSkillLevel { get; }
    }
}
