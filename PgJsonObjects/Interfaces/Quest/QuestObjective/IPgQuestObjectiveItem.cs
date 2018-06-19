namespace PgJsonObjects
{
    public interface IPgQuestObjectiveItem
    {
        IPgItem QuestItem { get; }
        ItemCollection TargetItemList { get; }
        ItemKeyword Target { get; }
    }
}
