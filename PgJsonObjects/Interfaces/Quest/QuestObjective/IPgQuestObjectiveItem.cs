namespace PgJsonObjects
{
    public interface IPgQuestObjectiveItem
    {
        Item QuestItem { get; }
        ItemCollection TargetItemList { get; }
        ItemKeyword Target { get; }
    }
}
