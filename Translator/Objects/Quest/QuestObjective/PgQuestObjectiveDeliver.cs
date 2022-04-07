namespace PgObjects
{
    public class PgQuestObjectiveDeliver : PgQuestObjective
    {
        public PgNpcLocation DeliverNpc { get; set; } = null!;
        public string? Item_Key { get; set; }
        public int NumToDeliver { get { return RawNumToDeliver.HasValue ? RawNumToDeliver.Value : 0; } }
        public int? RawNumToDeliver { get; set; }
    }
}
