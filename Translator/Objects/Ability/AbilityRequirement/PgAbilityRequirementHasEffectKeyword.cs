namespace PgObjects
{
    public class PgAbilityRequirementHasEffectKeyword : PgAbilityRequirement
    {
        public AbilityKeyword Keyword { get; set; }
        public int MinCount { get { return RawMinCount.HasValue ? RawMinCount.Value : 0; } }
        public int? RawMinCount { get; set; }
    }
}
