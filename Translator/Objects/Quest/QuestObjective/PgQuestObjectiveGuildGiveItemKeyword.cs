namespace PgObjects
{
    public class PgQuestObjectiveGuildGiveItemKeyword : PgQuestObjective
    {
        public PgNpcLocation DeliverNpc { get; set; }
        public ItemKeyword Keyword { get; set; }
    }
}
