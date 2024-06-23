namespace PgObjects
{
    public class PgQuestRewardRacingXp : PgQuestReward
    {
        public string? Skill_Key { get; set; }
        public int Xp { get { return RawXp.HasValue ? RawXp.Value : 0; } }
        public int? RawXp { get; set; }
    }
}
