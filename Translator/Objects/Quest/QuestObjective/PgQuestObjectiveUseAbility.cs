namespace PgJsonObjects
{
    public class PgQuestObjectiveUseAbility : PgQuestObjective
    {
        public PgAbilityCollection AbilityTargetList { get; set; } = new PgAbilityCollection();
        public AbilityKeyword AbilityTarget { get; set; }
    }
}
