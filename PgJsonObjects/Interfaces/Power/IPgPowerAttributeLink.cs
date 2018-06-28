namespace PgJsonObjects
{
    public interface IPgPowerAttributeLink : IPgPowerEffect
    {
        string AttributeName { get; }
        float AttributeEffect { get; }
        IPgAttribute AttributeLink { get; }
        IPgSkill SkillLink { get; }
        FloatFormat AttributeEffectFormat { get; }
    }
}
