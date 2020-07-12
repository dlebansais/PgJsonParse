namespace PgObjects
{
    public class PgRecipeResultWaxItem : PgRecipeResultEffect
    {
        public PowerWaxType PowerWaxType { get; set; }
        public int PowerLevel { get { return RawPowerLevel.HasValue ? RawPowerLevel.Value : 0; } }
        public int? RawPowerLevel { get; set; }
        public int MaxHitCount { get { return RawMaxHitCount.HasValue ? RawMaxHitCount.Value : 0; } }
        public int? RawMaxHitCount { get; set; }
    }
}
