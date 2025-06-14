namespace PgObjects
{
    public class PgQuestRequirementMinCombatSkillLevel : PgQuestRequirement
    {
        public int SkillLevel { get { return RawSkillLevel.HasValue ? RawSkillLevel.Value : 0; } }
        public int? RawSkillLevel { get; set; }
    }
}
