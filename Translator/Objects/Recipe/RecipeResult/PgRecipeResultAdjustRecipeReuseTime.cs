namespace PgJsonObjects
{
    public class PgRecipeResultAdjustRecipeReuseTime : PgRecipeResultEffect
    {
        public int AdjustedReuseTime { get { return RawAdjustedReuseTime.HasValue ? RawAdjustedReuseTime.Value : 0; } }
        public int? RawAdjustedReuseTime { get; set; }
        public MoonPhases MoonPhase { get; set; }
    }
}
