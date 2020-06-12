namespace PgObjects
{
    public class PgRecipeResultDeltaFairyEnergy : PgRecipeResultEffect
    {
        public int BoostLevel { get { return RawBoostLevel.HasValue ? RawBoostLevel.Value : 0; } }
        public int? RawBoostLevel { get; set; }
    }
}
