namespace PgJsonObjects
{
    public interface IPgQuestObjectiveGuildGiveItem
    {
        IPgItem QuestItem { get; }
        IPgGameNpc DeliverNpc { get; }
        IPgItemCollection ItemList { get; }
        string DeliverNpcId { get; }
        string DeliverNpcName { get; }
        ItemKeyword ItemKeyword { get; }
    }
}
