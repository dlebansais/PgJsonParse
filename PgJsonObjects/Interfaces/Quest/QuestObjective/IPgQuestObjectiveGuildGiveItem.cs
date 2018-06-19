namespace PgJsonObjects
{
    public interface IPgQuestObjectiveGuildGiveItem
    {
        IPgItem QuestItem { get; }
        IPgGameNpc DeliverNpc { get; }
        ItemCollection ItemList { get; }
        ItemKeyword ItemKeyword { get; }
    }
}
