namespace PgObjects
{
    public class PgQuestObjectiveGuildGiveItem : PgQuestObjective
    {
        public PgNpcLocation DeliverNpc { get; set; } = null!;
        public string? Item_Key { get; set; }
    }
}
