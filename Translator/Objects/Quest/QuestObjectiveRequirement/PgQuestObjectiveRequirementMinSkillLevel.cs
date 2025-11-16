namespace PgObjects
{
    public class PgQuestObjectiveRequirementMinSkillLevel : PgQuestObjectiveRequirement
    {
        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get; set; }
        public string? Skill_Key { get; set; }
    }
}
