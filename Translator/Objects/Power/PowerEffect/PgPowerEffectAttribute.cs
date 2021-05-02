namespace PgObjects
{
    public class PgPowerEffectAttribute : PgPowerEffect
    {
        public PgAttribute Attribute { get; set; } = null!;
        public float AttributeEffect { get; set; }
        public PgSkill Skill { get; set; } = null!;
        public FloatFormat AttributeEffectFormat { get; set; }
    }
}
