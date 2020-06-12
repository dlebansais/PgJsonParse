namespace PgObjects
{
    public class PgQuestObjectiveUseItem : PgQuestObjective
    {
        public ItemKeyword ItemTarget { get; set; }
        public PgItem QuestItem { get; set; }
        public PgQuestObjectiveRequirement QuestObjectiveRequirement { get; set; }
    }
}
