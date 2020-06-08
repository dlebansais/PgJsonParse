namespace PgJsonObjects
{
    public class PgQuestObjectiveDeliver : PgQuestObjective
    {
        public PgNpcLocation DeliverNpc { get; set; }
        public PgItem QuestItem { get; set; }
        public int NumToDeliver { get { return RawNumToDeliver.HasValue ? RawNumToDeliver.Value : 0; } }
        public int? RawNumToDeliver { get; set; }
        public string DeliverNpcId { get; set; } = string.Empty;
        public string DeliverNpcName { get; set; } = string.Empty;
        public MapAreaName DeliverNpcArea { get; set; }
    }
}
