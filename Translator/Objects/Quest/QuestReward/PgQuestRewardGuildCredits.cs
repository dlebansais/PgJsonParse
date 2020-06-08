namespace PgJsonObjects
{
    public class PgQuestRewardGuildCredits : PgQuestReward
    {
        public int Credits { get { return RawCredits.HasValue ? RawCredits.Value : 0; } }
        public int? RawCredits { get; set; }
    }
}
