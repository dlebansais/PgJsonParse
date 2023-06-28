namespace PgObjects
{
    public class PgQuestObjectiveScripted : PgQuestObjective
    {
        public PgQuestObjectiveRequirementCollection QuestObjectiveRequirementList { get; set; } = new PgQuestObjectiveRequirementCollection();
    }
}
