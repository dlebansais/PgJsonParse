namespace PgObjects
{
    public class PgRecipeResultEnhancedItem : PgRecipeResultEffect
    {
        public EnhancementEffect Enhancement { get; set; }
        public float AddedQuantity { get { return RawAddedQuantity.HasValue ? RawAddedQuantity.Value : 0; } }
        public float? RawAddedQuantity { get; set; }
        public int ConsumedEnhancementPoints { get { return RawConsumedEnhancementPoints.HasValue ? RawConsumedEnhancementPoints.Value : 0; } }
        public int? RawConsumedEnhancementPoints { get; set; }
    }
}
