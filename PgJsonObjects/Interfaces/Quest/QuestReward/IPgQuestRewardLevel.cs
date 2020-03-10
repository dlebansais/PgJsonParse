namespace PgJsonObjects
{
    public interface IPgQuestRewardLevel
    {
        IPgSkill Skill { get; }
        int Level { get; }
        int? RawLevel { get; }
    }
}
