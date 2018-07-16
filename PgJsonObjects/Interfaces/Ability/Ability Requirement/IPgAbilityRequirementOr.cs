namespace PgJsonObjects
{
    public interface IPgAbilityRequirementOr : IPgAbilityRequirement
    {
        IPgAbilityRequirementCollection OrList { get; }
        string ErrorMsg { get; }
    }
}
