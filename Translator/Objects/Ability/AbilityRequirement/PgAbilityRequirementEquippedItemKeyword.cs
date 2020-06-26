namespace PgObjects
{
    public class PgAbilityRequirementEquippedItemKeyword : PgAbilityRequirement
    {
        public int MinCount { get { return RawMinCount.HasValue ? RawMinCount.Value : 0; } }
        public int? RawMinCount { get; set; }
        public ItemKeyword Keyword { get; set; }
    }
}
