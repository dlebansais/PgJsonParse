namespace PgJsonObjects
{
    public interface IPgQuestObjectiveScriptedReceiveItem
    {
        IPgGameNpc DeliverNpc { get; }
        IPgItem QuestItem { get; }
    }
}
