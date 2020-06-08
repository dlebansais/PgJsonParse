namespace PgJsonObjects
{
    public class PgQuestObjectiveKill : PgQuestObjective
    {
        public AbilityKeyword Keyword { get; set; }
        public QuestObjectiveKillTarget Target { get; set; }
        public EffectKeyword EffectRequirement { get; set; }
        public PgQuestObjectiveRequirement QuestObjectiveRequirement { get; set; }
    }
}
