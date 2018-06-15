namespace PgJsonObjects
{
    public interface IPgQuestObjectiveScriptedReceiveItem
    {
        GameNpc DeliverNpc { get; }
        Item QuestItem { get; }
    }
}
