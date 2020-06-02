namespace PgJsonObjects
{
    public class PgQuestObjectiveUseAbilityOnTargets : PgQuestObjective
    {
        public PgAbilityCollection AbilityList { get; private set; } = new PgAbilityCollection();
        public AbilityKeyword Keyword { get; private set; }
    }
}
