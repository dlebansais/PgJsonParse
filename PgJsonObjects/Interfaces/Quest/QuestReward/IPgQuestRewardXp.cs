namespace PgJsonObjects
{
    public interface IPgQuestRewardXp
    {
        IPgSkill Skill { get; }
        int Xp { get; }
        int? RawXp { get; }
    }
}
