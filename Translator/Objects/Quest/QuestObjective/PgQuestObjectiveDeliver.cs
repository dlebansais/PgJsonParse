namespace PgObjects
{
    public class PgQuestObjectiveDeliver : PgQuestObjective
    {
        public PgNpcLocation DeliverNpc { get; set; } = null!;
        public PgItem Item { get; set; } = null!;
        public int NumToDeliver { get { return RawNumToDeliver.HasValue ? RawNumToDeliver.Value : 0; } }
        public int? RawNumToDeliver { get; set; }
    }
}
