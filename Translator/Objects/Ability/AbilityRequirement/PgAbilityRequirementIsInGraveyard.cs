namespace PgJsonObjects
{
    public class PgAbilityRequirementIsInGraveyard : PgAbilityRequirement
    {
        public override OtherRequirementType Type { get { return OtherRequirementType.InGraveyard; } }
    }
}
