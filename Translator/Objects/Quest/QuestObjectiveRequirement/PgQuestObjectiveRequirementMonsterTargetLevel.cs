namespace PgObjects
{
    public class PgQuestObjectiveRequirementMonsterTargetLevel : PgQuestObjectiveRequirement
    {
        public int MinLevel { get { return RawMinLevel.HasValue ? RawMinLevel.Value : 0; } }
        public int? RawMinLevel { get; set; }
    }
}
