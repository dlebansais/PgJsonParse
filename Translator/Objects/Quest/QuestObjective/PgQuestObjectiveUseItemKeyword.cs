namespace PgObjects
{
    public class PgQuestObjectiveUseItemKeyword : PgQuestObjective
    {
        public ItemKeyword Keyword { get; set; }
        public PgQuestObjectiveRequirementCollection QuestObjectiveRequirementList { get; set; } = new PgQuestObjectiveRequirementCollection();
    }
}
