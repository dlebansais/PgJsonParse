namespace PgObjects
{
    public class PgQuestRequirementMinSkillLevel : PgQuestRequirement
    {
        public int SkillLevel { get { return RawSkillLevel.HasValue ? RawSkillLevel.Value : 0; } }
        public int? RawSkillLevel { get; set; }
        public string? Skill_Key { get; set; }
    }
}
