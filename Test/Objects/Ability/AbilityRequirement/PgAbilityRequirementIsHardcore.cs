namespace PgObjects
{
    public class PgAbilityRequirementIsHardcore : PgAbilityRequirement
    {
        public override OtherRequirementType Type { get { return OtherRequirementType.IsHardcore; } }
    }
}
