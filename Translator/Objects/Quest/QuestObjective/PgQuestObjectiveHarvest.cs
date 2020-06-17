namespace PgObjects
{
    public class PgQuestObjectiveHarvest : PgQuestObjective
    {
        public ItemKeyword Target { get; set; }
        public PgItem Item { get; set; }
        public PgQuestObjectiveRequirementCollection QuestObjectiveRequirementList { get; set; } = new PgQuestObjectiveRequirementCollection();
    }
}
