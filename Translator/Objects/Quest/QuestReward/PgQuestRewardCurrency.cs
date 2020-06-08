namespace PgJsonObjects
{
    public class PgQuestRewardCurrency : PgQuestReward
    {
        public Currency Currency { get; set; }
        public int Amount { get { return RawAmount.HasValue ? RawAmount.Value : 0; } }
        public int? RawAmount { get; set; }
    }
}
