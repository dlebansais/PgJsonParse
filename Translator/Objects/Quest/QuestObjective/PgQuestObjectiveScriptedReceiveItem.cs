namespace PgJsonObjects
{
    public class PgQuestObjectiveScriptedReceiveItem : PgQuestObjective
    {
        public PgNpc DeliverNpc { get; set; }
        public PgItem QuestItem { get; set; }
        public string DeliverNpcId { get; set; } = string.Empty;
        public string DeliverNpcName { get; set; } = string.Empty;
        public MapAreaName DeliverNpcArea { get; set; }
    }
}
