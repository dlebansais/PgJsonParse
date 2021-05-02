namespace PgObjects
{
    public class PgQuestObjectiveScriptedReceiveItem : PgQuestObjective
    {
        public PgNpcLocation DeliverNpc { get; set; } = null!;
        public PgItem Item { get; set; } = null!;
    }
}
