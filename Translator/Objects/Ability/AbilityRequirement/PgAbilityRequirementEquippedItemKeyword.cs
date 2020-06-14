namespace PgObjects
{
    public class PgAbilityRequirementEquippedItemKeyword : PgAbilityRequirement
    {
        public int MinCount { get { return RawMinCount.HasValue ? RawMinCount.Value : 0; } }
        public int? RawMinCount { get; set; }
        public int MaxCount { get { return RawMaxCount.HasValue ? RawMaxCount.Value : 0; } }
        public int? RawMaxCount { get; set; }
        public AbilityKeyword Keyword { get; set; }
    }
}
