namespace PgObjects
{
    public class PgQuestObjectiveHarvestItem : PgQuestObjective
    {
        public PgItem Item { get; set; }
        public PgQuestObjectiveRequirementCollection QuestObjectiveRequirementList { get; set; } = new PgQuestObjectiveRequirementCollection();
    }
}
