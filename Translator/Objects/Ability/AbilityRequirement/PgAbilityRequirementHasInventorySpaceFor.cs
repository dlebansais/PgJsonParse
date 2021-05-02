namespace PgObjects
{
    public class PgAbilityRequirementHasInventorySpaceFor : PgAbilityRequirement
    {
        public PgItem Item { get; set; } = null!;
    }
}
