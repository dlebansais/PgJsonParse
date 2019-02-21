namespace PgJsonObjects
{
    public interface IPgQuestObjectiveUseAbilityOnTargets
    {
        IPgAbilityCollection AbilityList { get; }
        AbilityKeyword Keyword { get; }
    }
}
