namespace PgObjects
{
    public class PgRecipeResultEffectWithTier : PgRecipeResultEffect
    {
        public EffectKeyword Keyword { get; set; }
        public int Tier { get { return RawTier.HasValue ? RawTier.Value : 0; } }
        public int? RawTier { get; set; }
    }
}
