namespace PgJsonObjects
{
    public interface IPgQuestRewardCurrency
    {
        int Amount { get; }
        int? RawAmount { get; }
        Currency Currency { get; }
    }
}
