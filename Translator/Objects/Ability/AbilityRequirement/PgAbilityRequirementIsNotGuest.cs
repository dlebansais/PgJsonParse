namespace PgJsonObjects
{
    public class PgAbilityRequirementIsNotGuest : PgAbilityRequirement
    {
        public override OtherRequirementType Type { get { return OtherRequirementType.IsNotGuest; } }
    }
}
