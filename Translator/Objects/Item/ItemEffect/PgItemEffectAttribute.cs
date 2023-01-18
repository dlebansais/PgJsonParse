namespace PgObjects
{
    public class PgItemEffectAttribute : PgItemEffect
    {
        public string? Attribute_Key { get; set; }
        public float AttributeEffect { get; set; }
        public FloatFormat AttributeEffectFormat { get; set; }
        public string? Label { get; set; }
    }
}
