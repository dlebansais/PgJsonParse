namespace PgObjects
{
    public class PgRecipeResultAddShamanicPower : PgRecipeResultEffect
    {
        public ShamanicSlotPower Slot { get; set; }
        public int PowerLevel { get { return RawPowerLevel.HasValue ? RawPowerLevel.Value : 0; } }
        public int? RawPowerLevel { get; set; }
    }
}
