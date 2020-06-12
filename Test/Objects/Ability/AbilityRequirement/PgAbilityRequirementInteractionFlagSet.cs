namespace PgObjects
{
    public class PgAbilityRequirementInteractionFlagSet : PgAbilityRequirement
    {
        public override OtherRequirementType Type { get { return OtherRequirementType.InteractionFlagSet; } }
        public string InteractionFlag { get; set; } = string.Empty;
    }
}
