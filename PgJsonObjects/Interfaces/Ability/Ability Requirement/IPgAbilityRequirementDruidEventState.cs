namespace PgJsonObjects
{
    public interface IPgAbilityRequirementDruidEventState : IPgAbilityRequirement
    {
        DisallowedState DisallowedState { get; }
    }
}
