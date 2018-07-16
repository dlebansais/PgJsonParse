namespace PgJsonObjects
{
    public interface IPgQuestObjectiveItem
    {
        IPgItem QuestItem { get; }
        IPgItemCollection TargetItemList { get; }
        ItemKeyword Target { get; }
    }
}
