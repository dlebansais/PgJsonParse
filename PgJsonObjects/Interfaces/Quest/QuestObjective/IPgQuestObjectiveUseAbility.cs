namespace PgJsonObjects
{
    public interface IPgQuestObjectiveUseAbility
    {
        AbilityCollection AbilityTargetList { get; }
        AbilityKeyword AbilityTarget { get; }
    }
}
