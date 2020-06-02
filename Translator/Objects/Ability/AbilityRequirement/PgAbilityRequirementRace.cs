namespace PgJsonObjects
{
    public class PgAbilityRequirementRace : PgAbilityRequirement
    {
        public override OtherRequirementType Type { get { return OtherRequirementType.Race; } }
        public string Name { get; set; } = string.Empty;
    }
}
