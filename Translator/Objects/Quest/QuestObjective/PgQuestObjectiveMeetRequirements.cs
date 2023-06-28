namespace PgObjects
{
    public class PgQuestObjectiveMeetRequirements : PgQuestObjective
    {
        public PgQuestObjectiveRequirementCollection QuestObjectiveRequirementList { get; set; } = new PgQuestObjectiveRequirementCollection();
    }
}
