namespace PgJsonObjects
{
    public class PgQuestObjectiveUseAbility : PgQuestObjective
    {
        public PgAbilityCollection AbilityTargetList { get; } = new PgAbilityCollection();
        public AbilityKeyword AbilityTarget { get; set; }
    }
}
