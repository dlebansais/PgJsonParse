namespace PgJsonObjects
{
    public class PgPowerEffectAttribute : PgPowerEffect
    {
        public string AttributeName { get; set; } = string.Empty;
        public float AttributeEffect { get { return RawAttributeEffect.HasValue ? RawAttributeEffect.Value : 0; } }
        public float? RawAttributeEffect { get; set; }
        public PgAttribute Attribute { get; set; }
        public PgSkill Skill { get; set; }
        public FloatFormat AttributeEffectFormat { get; set; }
    }
}
