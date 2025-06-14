namespace PgObjects
{
    public class PgQuestObjectiveRequirementInCombatWithElite : PgQuestObjectiveRequirement
    {
        public int MinLevel { get { return RawMinLevel.HasValue ? RawMinLevel.Value : 0; } }
        public int? RawMinLevel { get; set; }
    }
}
