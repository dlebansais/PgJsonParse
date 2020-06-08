namespace PgJsonObjects
{
    public class PgQuestObjectiveUseAbilityOnTargets : PgQuestObjective
    {
        public string InteractionTarget { get; set; } = string.Empty;
        public AbilityKeyword Keyword { get; set; }
    }
}
