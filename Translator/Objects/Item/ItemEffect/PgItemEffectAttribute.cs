namespace PgJsonObjects
{
    public class PgItemEffectAttribute : PgItemEffect
    {
        public string AttributeName { get; set; } = string.Empty;
        public float AttributeEffect { get; set; }
        public FloatFormat AttributeEffectFormat { get; set; }
        public PgAttribute Attribute { get; set; }
    }
}
