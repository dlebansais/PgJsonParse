namespace PgJsonObjects
{
    public class PgAbilityRequirementInHotspot : PgAbilityRequirement
    {
        public override OtherRequirementType Type { get { return OtherRequirementType.InHotspot; } }
        public string Name { get; set; } = string.Empty;
    }
}
