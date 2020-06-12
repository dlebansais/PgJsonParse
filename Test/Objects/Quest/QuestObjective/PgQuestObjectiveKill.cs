namespace PgObjects
{
    public class PgQuestObjectiveKill : PgQuestObjective
    {
        public QuestObjectiveKillTarget Target { get; set; }
        public AbilityKeyword Keyword { get; set; }
        public PgQuestObjectiveRequirement QuestObjectiveRequirement { get; set; }
    }
}
