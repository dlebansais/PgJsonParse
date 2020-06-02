namespace PgJsonObjects
{
    public class PgQuestRequirementMinSkillLevel : PgQuestRequirement
    {
        public PgSkill Skill { get; set; }
        public int SkillLevel { get { return RawSkillLevel.HasValue ? RawSkillLevel.Value : 0; } }
        public int? RawSkillLevel { get; set; }
    }
}
