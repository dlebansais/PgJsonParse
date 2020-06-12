namespace PgObjects
{
    public class PgRecipeResultWaxItem : PgRecipeResultEffect
    {
        public PowerWaxType PowerWaxType { get; set; }
        public int PowerLevel { get { return RawPowerLevel.HasValue ? RawPowerLevel.Value : 0; } }
        public int? RawPowerLevel { get; set; }
        public int AdjustedReuseTime { get { return RawAdjustedReuseTime.HasValue ? RawAdjustedReuseTime.Value : 0; } }
        public int? RawAdjustedReuseTime { get; set; }
    }
}
