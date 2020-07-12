namespace PgObjects
{
    public class PgRecipeResultDeltaFairyEnergy : PgRecipeResultEffect
    {
        public int Delta { get { return RawDelta.HasValue ? RawDelta.Value : 0; } }
        public int? RawDelta { get; set; }
    }
}
