namespace PgObjects
{
    public class PgQuestObjectiveHarvestItemKeyword : PgQuestObjective
    {
        public ItemKeyword Keyword { get; set; }
        public PgQuestObjectiveRequirementCollection QuestObjectiveRequirementList { get; set; } = new PgQuestObjectiveRequirementCollection();
    }
}
