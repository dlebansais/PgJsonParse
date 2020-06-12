namespace PgObjects
{
    public class PgAbilityRequirementDruidEventState : PgAbilityRequirement
    {
        public override OtherRequirementType Type { get { return OtherRequirementType.DruidEventState; } }
        public DisallowedState DisallowedState { get; set; }
    }
}
