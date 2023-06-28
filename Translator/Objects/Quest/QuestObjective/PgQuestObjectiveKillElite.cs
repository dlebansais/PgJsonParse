namespace PgObjects
{
    public class PgQuestObjectiveKillElite : PgQuestObjective
    {
        public QuestObjectiveTarget Target { get; set; }
        public PgQuestObjectiveRequirementCollection QuestObjectiveRequirementList { get; set; } = new PgQuestObjectiveRequirementCollection();
    }
}
