namespace PgObjects
{
    public class PgQuestObjectiveUseItem : PgQuestObjective
    {
        public string? Item_Key { get; set; }
        public PgQuestObjectiveRequirementCollection QuestObjectiveRequirementList { get; set; } = new PgQuestObjectiveRequirementCollection();
        public string? BehaviorId { get; set; }
    }
}
