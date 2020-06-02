namespace PgJsonObjects
{
    public class PgQuestReward
    {
        public string Type { get; set; } = string.Empty;
        public int RewardXp { get { return RawRewardXp.HasValue ? RawRewardXp.Value : 0; } }
        public int? RawRewardXp { get; set; }
        public string RewardRecipe { get; set; } = string.Empty;
        public int RewardGuildCredits { get { return RawRewardGuildCredits.HasValue ? RawRewardGuildCredits.Value : 0; } }
        public int? RawRewardGuildCredits { get; set; }
        public PowerSkill RewardSkill { get; set; }
    }
}
