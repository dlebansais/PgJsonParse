namespace PgJsonObjects
{
    public interface IPgQuestObjectiveScriptedReceiveItem
    {
        IPgGameNpc DeliverNpc { get; }
        IPgItem QuestItem { get; }
        string DeliverNpcId { get; }
        string DeliverNpcName { get; }
        MapAreaName DeliverNpcArea { get; }
    }
}
