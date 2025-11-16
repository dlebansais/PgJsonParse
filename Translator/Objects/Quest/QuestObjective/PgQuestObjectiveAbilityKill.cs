namespace PgObjects
{
    public class PgQuestObjectiveAbilityKill : PgQuestObjective
    {
        public AbilityKeyword AbilityKeyword { get; set; }
        public PgQuestObjectiveRequirementCollection QuestObjectiveRequirementList { get; set; } = new PgQuestObjectiveRequirementCollection();
        public QuestObjectiveTarget Target { get; set; }
    }
}
