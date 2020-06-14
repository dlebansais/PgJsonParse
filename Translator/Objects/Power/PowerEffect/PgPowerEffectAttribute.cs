namespace PgObjects
{
    public class PgPowerEffectAttribute : PgPowerEffect
    {
        public PgAttribute Attribute { get; set; }
        public float AttributeEffect { get; set; }
        public PgSkill Skill { get; set; }
        public FloatFormat AttributeEffectFormat { get; set; }
    }
}
