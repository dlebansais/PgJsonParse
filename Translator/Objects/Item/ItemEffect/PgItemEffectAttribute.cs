namespace PgObjects
{
    public class PgItemEffectAttribute : PgItemEffect
    {
        public PgAttribute Attribute { get; set; } = null!;
        public float AttributeEffect { get; set; }
        public FloatFormat AttributeEffectFormat { get; set; }
    }
}
