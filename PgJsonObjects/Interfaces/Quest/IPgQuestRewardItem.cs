namespace PgJsonObjects
{
    public interface IPgQuestRewardItem
    {
        Item QuestItem { get; }
        int StackSize { get; }
        int? RawStackSize { get; }
    }
}
