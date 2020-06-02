namespace PgJsonObjects
{
    public class PgAbilityRequirementHasInventorySpaceFor : PgAbilityRequirement
    {
        public override OtherRequirementType Type { get { return OtherRequirementType.HasInventorySpaceFor; } }
        public PgItem Item { get; }
    }
}
