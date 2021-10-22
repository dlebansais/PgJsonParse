namespace PgObjects
{
    public class PgQuestObjectiveUseAbilityOnTargets : PgQuestObjective
    {
        public string Target { get; set; } = string.Empty;
        public AbilityKeyword Keyword { get; set; }
        public PgQuestObjectiveRequirement? QuestObjectiveRequirement { get; set; }
    }
}
