namespace PgObjects
{
    using System;

    public class PgRecipeResultAdjustRecipeReuseTime : PgRecipeResultEffect
    {
        public TimeSpan AdjustedReuseTime { get { return RawAdjustedReuseTime.HasValue ? RawAdjustedReuseTime.Value : TimeSpan.Zero; } }
        public TimeSpan? RawAdjustedReuseTime { get; set; }
        public MoonPhases MoonPhase { get; set; }
    }
}
