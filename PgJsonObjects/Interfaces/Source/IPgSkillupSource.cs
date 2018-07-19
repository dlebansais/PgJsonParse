namespace PgJsonObjects
{
    public interface IPgSkillupSource : IPgGenericSource
    {
        IPgSkill Skill { get; }
    }
}
