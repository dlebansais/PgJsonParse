namespace PgObjects
{
    public class PgQuestObjectiveKillElite : PgQuestObjective
    {
        public QuestObjectiveTarget Target { get; set; }
        public PgQuestObjectiveRequirement? QuestRequirement { get; set; }
    }
}
