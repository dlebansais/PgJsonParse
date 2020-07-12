namespace PgObjects
{
    public class PgQuestObjectiveKill : PgQuestObjective
    {
        public QuestObjectiveKillTarget Target { get; set; }
        public PgQuestObjectiveRequirementCollection QuestObjectiveRequirementList { get; set; } = new PgQuestObjectiveRequirementCollection();
    }
}
