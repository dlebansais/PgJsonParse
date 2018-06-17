namespace PgJsonObjects
{
    public interface IPgItemSkillLink
    {
        string SkillName { get; }
        int SkillLevel { get; }
        int? RawSkillLevel { get; }
        Skill Link { get; }
    }
}
