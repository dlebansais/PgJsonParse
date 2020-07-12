namespace PgObjects
{
    public class PgQuestObjectiveUseItem : PgQuestObjective
    {
        public PgItem Item { get; set; }
        public PgQuestObjectiveRequirement QuestObjectiveRequirement { get; set; }
    }
}
