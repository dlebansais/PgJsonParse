namespace PgObjects
{
    public class PgSelfPreEffectConfigGalvanize : PgSelfPreEffect
    {
        public float Value { get { return RawValue.HasValue ? RawValue.Value : 0; } }
        public float? RawValue { get; set; }
    }
}
