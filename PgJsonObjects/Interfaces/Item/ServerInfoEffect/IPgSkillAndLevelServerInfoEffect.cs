namespace PgJsonObjects
{
    public interface IPgSkillAndLevelServerInfoEffect
    {
        Skill Skill { get; }
        int SkillLevel { get; }
        int? RawSkillLevel { get; }
    }
}
