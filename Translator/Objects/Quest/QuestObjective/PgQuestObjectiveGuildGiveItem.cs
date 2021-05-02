namespace PgObjects
{
    public class PgQuestObjectiveGuildGiveItem : PgQuestObjective
    {
        public PgNpcLocation DeliverNpc { get; set; } = null!;
        public PgItem Item { get; set; } = null!;
    }
}
