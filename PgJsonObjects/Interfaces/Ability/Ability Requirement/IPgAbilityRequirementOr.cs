namespace PgJsonObjects
{
    public interface IPgAbilityRequirementOr
    {
        AbilityRequirementCollection OrList { get; }
        string ErrorMsg { get; }
    }
}
