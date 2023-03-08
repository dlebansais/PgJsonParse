namespace PgObjects
{
    public class PgRecipeResultRaiseMaxTempestRaiseEnergy : PgRecipeResultEffect
    {
        public int RaiseEnergy { get { return RawRaiseEnergy.HasValue ? RawRaiseEnergy.Value : 0; } }
        public int? RawRaiseEnergy { get; set; }
    }
}
