namespace PgObjects
{
    public class PgQuestObjectiveGuildGiveItem : PgQuestObjective
    {
        public PgNpcLocation DeliverNpc { get; set; }
        public PgItem QuestItem { get; set; }
        public ItemKeyword ItemKeyword { get; set; }
    }
}
