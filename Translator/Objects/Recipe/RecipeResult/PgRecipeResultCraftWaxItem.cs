namespace PgObjects
{
    public class PgRecipeResultCraftWaxItem : PgRecipeResultEffect
    {
        public string? Item_Key { get; set; }
        public PowerWaxType PowerWaxType { get; set; }
        public int BoostLevel { get { return RawBoostLevel.HasValue ? RawBoostLevel.Value : 0; } }
        public int? RawBoostLevel { get; set; }
        public int MaxHitCount { get { return RawMaxHitCount.HasValue ? RawMaxHitCount.Value : 0; } }
        public int? RawMaxHitCount { get; set; }
    }
}
