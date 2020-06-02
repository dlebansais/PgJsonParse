namespace PgJsonObjects
{
    public class PgAbilityRequirementNotInHotspot : PgAbilityRequirement
    {
        public override OtherRequirementType Type { get { return OtherRequirementType.IsNotInHotspot; } }
        public string Name { get; set; } = string.Empty;
    }
}
