namespace PgObjects
{
    public class PgQuestObjectiveScriptedReceiveItem : PgQuestObjective
    {
        public PgNpcLocation DeliverNpc { get; set; } = null!;
        public string? Item_Key { get; set; }
    }
}
