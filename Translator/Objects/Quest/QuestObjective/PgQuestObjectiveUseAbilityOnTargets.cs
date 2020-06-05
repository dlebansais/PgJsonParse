namespace PgJsonObjects
{
    public class PgQuestObjectiveUseAbilityOnTargets : PgQuestObjective
    {
        public PgAbilityCollection AbilityList { get; } = new PgAbilityCollection();
        public AbilityKeyword Keyword { get; set; }
    }
}
