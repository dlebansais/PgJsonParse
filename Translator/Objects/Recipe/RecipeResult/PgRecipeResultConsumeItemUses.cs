namespace PgObjects
{
    public class PgRecipeResultConsumeItemUses : PgRecipeResultEffect
    {
        public ItemKeyword Keyword { get; set; }
        public int ConsumedUse { get { return RawConsumedUse.HasValue ? RawConsumedUse.Value : 0; } }
        public int? RawConsumedUse { get; set; }
    }
}
