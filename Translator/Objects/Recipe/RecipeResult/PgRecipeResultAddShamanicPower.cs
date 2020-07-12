namespace PgObjects
{
    public class PgRecipeResultAddShamanicPower : PgRecipeResultEffect
    {
        public ShamanicSlotPower Slot { get; set; }
        public int Tier { get { return RawTier.HasValue ? RawTier.Value : 0; } }
        public int? RawTier { get; set; }
    }
}
