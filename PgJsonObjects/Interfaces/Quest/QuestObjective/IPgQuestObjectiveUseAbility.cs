namespace PgJsonObjects
{
    public interface IPgQuestObjectiveUseAbility
    {
        IPgAbilityCollection AbilityTargetList { get; }
        AbilityKeyword AbilityTarget { get; }
    }
}
