namespace PgObjects
{
    public class PgQuestObjectiveKillElite : PgQuestObjective
    {
        public QuestObjectiveKillTarget Target { get; set; }
        public PgQuestObjectiveRequirement QuestRequirement { get; set; } = null!;
    }
}
