namespace PgObjects
{
    using Translator;

    public class PgPowerEffectAttribute : PgPowerEffect
    {
        public string? Attribute_Key { get; set; }
        public float AttributeEffect { get; set; }
        public string? Skill_Key { get; set; }
        public FloatFormat AttributeEffectFormat { get; set; }

        public PgAttribute? AttributeRef;

        public void SetAttribute(PgAttribute attribute)
        {
            Attribute_Key = PgObject.GetItemKey(attribute);
            AttributeRef = attribute;
        }
    }
}
