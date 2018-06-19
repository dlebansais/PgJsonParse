namespace PgJsonObjects
{
    public interface IPgSkillAndLevelServerInfoEffect
    {
        IPgSkill Skill { get; }
        int SkillLevel { get; }
        int? RawSkillLevel { get; }
    }
}
