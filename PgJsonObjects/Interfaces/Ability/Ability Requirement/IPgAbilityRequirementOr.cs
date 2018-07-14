namespace PgJsonObjects
{
    public interface IPgAbilityRequirementOr : IPgAbilityRequirement
    {
        AbilityRequirementCollection OrList { get; }
        string ErrorMsg { get; }
    }
}
