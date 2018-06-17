namespace PgJsonObjects
{
    public interface IPgQuestRewardXp
    {
        Skill Skill { get; }
        int Xp { get; }
        int? RawXp { get; }
    }
}
