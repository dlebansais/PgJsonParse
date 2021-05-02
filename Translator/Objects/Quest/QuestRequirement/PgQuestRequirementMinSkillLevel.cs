namespace PgObjects
{
    public class PgQuestRequirementMinSkillLevel : PgQuestRequirement
    {
        public int SkillLevel { get { return RawSkillLevel.HasValue ? RawSkillLevel.Value : 0; } }
        public int? RawSkillLevel { get; set; }
        public PgSkill Skill { get; set; } = null!;
    }
}
