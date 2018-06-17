namespace PgJsonObjects
{
    public interface IPgQuestObjectiveGuildGiveItem
    {
        Item QuestItem { get; }
        GameNpc DeliverNpc { get; }
        ItemCollection ItemList { get; }
        ItemKeyword ItemKeyword { get; }
    }
}
