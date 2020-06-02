namespace PgJsonObjects
{
    public class PgAbilityRequirementIsVegetarian : PgAbilityRequirement
    {
        public override OtherRequirementType Type { get { return OtherRequirementType.IsVegetarian; } }
    }
}
