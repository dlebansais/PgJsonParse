namespace PgObjects
{
    public class PgQuestObjectiveScriptedReceiveItem : PgQuestObjective
    {
        public PgNpcLocation DeliverNpc { get; set; }
        public PgItem QuestItem { get; set; }
    }
}
