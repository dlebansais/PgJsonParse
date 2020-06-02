namespace PgJsonObjects
{
    public class PgQuestRewardCurrency
    {
        public int Amount { get { return RawAmount.HasValue ? RawAmount.Value : 0; } }
        public int? RawAmount { get; set; }
        public Currency Currency { get; set; }
    }
}
