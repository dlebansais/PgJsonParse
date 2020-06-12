namespace PgObjects
{
    public class PgAbilityRequirementIsInGraveyard : PgAbilityRequirement
    {
        public override OtherRequirementType Type { get { return OtherRequirementType.InGraveyard; } }
    }
}
