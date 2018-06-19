namespace PgJsonObjects
{
    public interface IPgPowerAttributeLink
    {
        float AttributeEffect { get; }
        IPgAttribute AttributeLink { get; }
        IPgSkill SkillLink { get; }
    }
}
