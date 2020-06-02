namespace PgJsonObjects
{
    public class PgQuestObjectiveDeliver : PgQuestObjective
    {
        public PgNpc DeliverNpc { get; private set; }
        public PgItem QuestItem { get; private set; }
        public int NumToDeliver { get { return RawNumToDeliver.HasValue ? RawNumToDeliver.Value : 0; } }
        public int? RawNumToDeliver { get; private set; }
        public string DeliverNpcId { get; private set; } = string.Empty;
        public string DeliverNpcName { get; private set; } = string.Empty;
        public MapAreaName DeliverNpcArea { get; private set; }
    }
}
