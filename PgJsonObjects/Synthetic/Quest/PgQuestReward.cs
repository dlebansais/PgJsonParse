namespace PgJsonObjects
{
    public class PgQuestReward : GenericPgObject, IPgQuestReward
    {
        public PgQuestReward(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public string Type { get { return GetString(0); } }
        public int RewardXp { get { return RawRewardXp.HasValue ? RawRewardXp.Value : 0; } }
        public int? RawRewardXp { get { return GetInt(4); } }
        public string RewardRecipe { get { return GetString(8); } }
        public int RewardGuildCredits { get { return RawRewardGuildCredits.HasValue ? RawRewardGuildCredits.Value : 0; } }
        public int? RawRewardGuildCredits { get { return GetInt(12); } }
        public PowerSkill RewardSkill { get { return GetEnum<PowerSkill>(16); } }
    }
}
