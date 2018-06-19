namespace PgJsonObjects
{
    public interface IPgQuestRewardItem
    {
        IPgItem QuestItem { get; }
        int StackSize { get; }
        int? RawStackSize { get; }
    }
}
