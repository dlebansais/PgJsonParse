namespace PgJsonObjects
{
    public class PgQuestObjectiveUseAbilityOnTargets : PgQuestObjective
    {
        public string InteractionTarget { get; set; } = string.Empty;
        public PgAbilityCollection AbilityList { get; } = new PgAbilityCollection();
        public AbilityKeyword Keyword { get; set; }
    }
}
