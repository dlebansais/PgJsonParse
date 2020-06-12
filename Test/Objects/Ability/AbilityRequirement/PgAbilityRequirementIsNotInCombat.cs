namespace PgObjects
{
    public class PgAbilityRequirementIsNotInCombat : PgAbilityRequirement
    {
        public override OtherRequirementType Type { get { return OtherRequirementType.IsNotInCombat; } }
    }
}
