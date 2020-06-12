namespace PgObjects
{
    public class PgAbilityRequirementHasEffectKeyword : PgAbilityRequirement
    {
        public override OtherRequirementType Type { get { return OtherRequirementType.HasEffectKeyword; } }
        public AbilityKeyword Keyword { get; set; }
    }
}
