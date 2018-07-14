namespace PgJsonObjects
{
    public interface IPgAbilityRequirementHasInventorySpaceFor : IPgAbilityRequirement
    {
        IPgItem Item { get; }
    }
}
