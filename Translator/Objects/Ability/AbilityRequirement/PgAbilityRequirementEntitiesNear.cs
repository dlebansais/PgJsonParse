namespace PgObjects
{
    public class PgAbilityRequirementEntitiesNear : PgAbilityRequirement
    {
        public int Distance { get { return RawDistance.HasValue ? RawDistance.Value : 0; } }
        public int? RawDistance { get; set; }
        public ItemKeyword EntityTypeTag { get; set; }
        public string ErrorMsg { get; set; } = string.Empty;
        public int MinCount { get { return RawMinCount.HasValue ? RawMinCount.Value : 0; } }
        public int? RawMinCount { get; set; }
    }
}
