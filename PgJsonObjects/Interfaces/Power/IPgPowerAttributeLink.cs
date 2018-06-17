namespace PgJsonObjects
{
    public interface IPgPowerAttributeLink
    {
        float AttributeEffect { get; }
        Attribute AttributeLink { get; }
        Skill SkillLink { get; }
    }
}
