namespace PgObjects
{
    public class PgQuestObjectiveDeliver : PgQuestObjective
    {
        public PgNpcLocation DeliverNpc { get; set; }
        public PgItem Item { get; set; }
        public int NumToDeliver { get { return RawNumToDeliver.HasValue ? RawNumToDeliver.Value : 0; } }
        public int? RawNumToDeliver { get; set; }
    }
}
