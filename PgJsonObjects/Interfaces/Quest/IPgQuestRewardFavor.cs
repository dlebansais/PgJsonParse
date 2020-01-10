namespace PgJsonObjects
{
    public interface IPgQuestRewardFavor
    {
        string RawNpcName { get; }
        int Favor { get; }
        int? RawFavor { get; }
    }
}
