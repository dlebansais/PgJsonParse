namespace PgJsonObjects
{
    public interface IPgAbilityRequirementEquippedItemKeyword
    {
        int MinCount { get; }
        int? RawMinCount { get; }
        int MaxCount { get; }
        int? RawMaxCount { get; }
        AbilityKeyword Keyword { get; }
    }
}
