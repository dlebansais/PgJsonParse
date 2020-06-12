namespace PgObjects
{
    public class PgQuestObjectiveCollect : PgQuestObjective
    {
        public ItemKeyword ItemTarget { get; set; }
        public PgItem QuestItem { get; set; }
    }
}
