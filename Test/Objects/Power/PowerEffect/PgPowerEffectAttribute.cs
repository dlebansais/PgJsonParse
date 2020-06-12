namespace PgObjects
{
    public class PgPowerEffectAttribute : PgPowerEffect
    {
        public string AttributeName { get; set; } = string.Empty;
        public float AttributeEffect { get; set; }
        public PgAttribute Attribute { get; set; }
        public PgSkill Skill { get; set; }
        public FloatFormat AttributeEffectFormat { get; set; }
    }
}
