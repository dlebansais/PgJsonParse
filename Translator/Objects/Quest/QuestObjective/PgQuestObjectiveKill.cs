namespace PgObjects
{
    public class PgQuestObjectiveKill : PgQuestObjective
    {
        public QuestObjectiveTarget Target { get; set; }
        public PgQuestObjectiveRequirementCollection QuestObjectiveRequirementList { get; set; } = new PgQuestObjectiveRequirementCollection();
    }
}
