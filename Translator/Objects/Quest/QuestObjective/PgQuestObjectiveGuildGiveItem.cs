namespace PgObjects
{
    public class PgQuestObjectiveGuildGiveItem : PgQuestObjective
    {
        public PgNpcLocation DeliverNpc { get; set; }
        public PgItem Item { get; set; }
        public ItemKeyword ItemKeyword { get; set; }
    }
}
