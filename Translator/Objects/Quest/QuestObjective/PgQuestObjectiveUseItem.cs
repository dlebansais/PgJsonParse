namespace PgObjects
{
    public class PgQuestObjectiveUseItem : PgQuestObjective
    {
        public ItemKeyword Target { get; set; }
        public PgItem Item { get; set; }
        public PgQuestObjectiveRequirement QuestObjectiveRequirement { get; set; }
    }
}
