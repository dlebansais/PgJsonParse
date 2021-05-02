namespace PgObjects
{
    public class PgQuestObjectiveGuildGiveItemKeyword : PgQuestObjective
    {
        public PgNpcLocation DeliverNpc { get; set; } = null!;
        public ItemKeyword Keyword { get; set; }
    }
}
