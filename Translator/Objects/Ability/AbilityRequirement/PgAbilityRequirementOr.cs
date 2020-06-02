namespace PgJsonObjects
{
    public class PgAbilityRequirementOr : PgAbilityRequirement
    {
        public override OtherRequirementType Type { get { return OtherRequirementType.Or; } }
        public PgAbilityRequirementCollection OrList { get; set; }
        public string ErrorMsg { get; set; } = string.Empty;
    }
}
