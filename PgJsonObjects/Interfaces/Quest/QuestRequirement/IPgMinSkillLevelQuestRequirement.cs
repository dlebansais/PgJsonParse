namespace PgJsonObjects
{
    public interface IPgMinSkillLevelQuestRequirement
    {
        IPgSkill ConnectedSkill { get; }
        int SkillLevel { get; }
        int? RawSkillLevel { get; }
    }
}
