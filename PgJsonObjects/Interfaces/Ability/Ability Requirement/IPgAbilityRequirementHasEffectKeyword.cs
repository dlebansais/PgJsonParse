namespace PgJsonObjects
{
    public interface IPgAbilityRequirementHasEffectKeyword : IPgAbilityRequirement
    {
        AbilityKeyword Keyword { get; }
    }
}
