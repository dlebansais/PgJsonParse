namespace PgJsonObjects
{
    public interface IPgQuestReward
    {
        string Type { get; }
        int RewardXp { get; }
        int? RawRewardXp { get; }
        string RewardRecipe { get; }
        int RewardGuildCredits { get; }
        int? RawRewardGuildCredits { get; }
        PowerSkill RewardSkill { get; }
    }
}
